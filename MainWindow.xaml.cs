using System.Windows;
using System.Windows.Controls;

namespace Labs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[,] map;
        public string current_player;
        int turn_number;
        public MainWindow()
        {
            InitializeComponent();

            current_player = "X";
            turn_number = 1;
            map = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    map[i, j] = " ";
                }
            }
        }

        private void make_move(object sender, RoutedEventArgs e)
        {
            Button pressed_button = (Button)sender;
            string pressed_button_name = pressed_button.Name;

            bool correct = false;
            while (correct != true)
            {
                int player_row = int.Parse(pressed_button_name[1].ToString());
                int player_col = int.Parse(pressed_button_name[2].ToString());
                if (player_row < 0 || player_row > 2 || player_col < 0 || player_col > 2)
                {
                    MessageBox.Show("Некоректне введення! Введiть повторно.");
                }
                else if (map[player_row, player_col] != " ")
                {
                    MessageBox.Show("Некоректне введення! Тут вже заповнено. Введiть повторно.");
                }
                else
                {
                    correct = true;
                    map[player_row, player_col] = current_player;
                    pressed_button.Content = current_player;
                }
            }

            check_win();

            if (current_player == "X")
            {
                current_player = "O";
            }
            else if (current_player == "O")
            {
                current_player = "X";
            }

            if (turn_number == 9)
            {
                MessageBox.Show("Нічия!");
                try
                {
                    Application.Current.Shutdown();
                }
                catch { }
            }
            turn_number++;
        }

        private void check_win()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((map[i, 0] == map[i, 1] && map[i, 1] == map[i, 2] && map[i, 2] == current_player) || // Вертикаль
                    (map[0, i] == map[1, i] && map[1, i] == map[2, i] && map[2, i] == current_player) || // Горизонталь
                    (map[0, 0] == map[1, 1] && map[1, 1] == map[2, 2] && map[2, 2] == current_player) || // Головна діагональ
                    (map[0, 2] == map[1, 1] && map[1, 1] == map[2, 0] && map[2, 0] == current_player))   // Побічна діагональ
                {

                    MessageBox.Show(current_player + " виграли!");
                    try
                    {
                        Application.Current.Shutdown();
                    }
                    catch { }
                }
            }
        }
    }
}
