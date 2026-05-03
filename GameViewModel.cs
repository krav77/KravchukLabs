using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Labs
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private string[] _board = new string[9];
        private bool _xTurn = true;
        private string _statusText = "";
        private bool _gameOver = false;
        private int _scoreX, _scoreO, _draws;

        public string StatusText { get { return _statusText; } private set { _statusText = value; OnPropertyChanged(); } }
        public int ScoreX { get { return _scoreX; } private set { _scoreX = value; OnPropertyChanged(); } }
        public int ScoreO { get { return _scoreO; } private set { _scoreO = value; OnPropertyChanged(); } }
        public int Draws { get { return _draws; } private set { _draws = value; OnPropertyChanged(); } }

        public string C0 { get { return _board[0]; } }
        public string C1 { get { return _board[1]; } }
        public string C2 { get { return _board[2]; } }
        public string C3 { get { return _board[3]; } }
        public string C4 { get { return _board[4]; } }
        public string C5 { get { return _board[5]; } }
        public string C6 { get { return _board[6]; } }
        public string C7 { get { return _board[7]; } }
        public string C8 { get { return _board[8]; } }

        public ICommand MakeMoveCommand { get; }
        public ICommand RestartCommand { get; }

        public GameViewModel()
        {
            MakeMoveCommand = new RelayCommand(
                p => MakeMove(int.Parse(p.ToString())),
                p => p != null && !_gameOver && _board[int.Parse(p.ToString())] == ""
            );
            RestartCommand = new RelayCommand(p => Restart());
            Restart();
        }

        private void MakeMove(int index)
        {
            _board[index] = _xTurn ? "X" : "O";
            NotifyCells();

            string winner;
            if (CheckWinner(out winner))
            {
                StatusText = "Переміг гравець " + winner + "!";
                _gameOver = true;
                if (winner == "X") ScoreX++; else ScoreO++;
            }
            else if (IsDraw())
            {
                StatusText = "Нічия!";
                _gameOver = true;
                Draws++;
            }
            else
            {
                _xTurn = !_xTurn;
                StatusText = "Хід гравця " + (_xTurn ? "X" : "O");
            }
        }

        private bool CheckWinner(out string winner)
        {
            int[][] lines = {
                new[]{0,1,2}, new[]{3,4,5}, new[]{6,7,8},
                new[]{0,3,6}, new[]{1,4,7}, new[]{2,5,8},
                new[]{0,4,8}, new[]{2,4,6}
            };
            foreach (var line in lines)
            {
                string a = _board[line[0]], b = _board[line[1]], c = _board[line[2]];
                if (a != "" && a == b && b == c) { winner = a; return true; }
            }
            winner = ""; return false;
        }

        private bool IsDraw()
        {
            foreach (var cell in _board) if (cell == "") return false;
            return true;
        }

        private void Restart()
        {
            for (int i = 0; i < 9; i++) _board[i] = "";
            _xTurn = true; _gameOver = false;
            StatusText = "Хід гравця X";
            NotifyCells();
        }

        private void NotifyCells()
        {
            for (int i = 0; i <= 8; i++) OnPropertyChanged("C" + i);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}