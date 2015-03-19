using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private User player1;
        private User player2;
        private int difficulty;

        //Empty game
        public GameWindow()
        {
            InitializeComponent();

        }

        //Single player game
        public GameWindow(User player, int difficultyLevel)
        {

            player1 = new User(player);
            difficulty = difficultyLevel;

            InitializeComponent();

            MessageBox.Show(player1.getUsername());
        }

        //Multiplayer game
        public GameWindow(User playerOne, User playerTwo, int difficultyLevel)
        {
            player1 = new User(playerOne);
            player2 = new User(playerTwo);

            difficulty = difficultyLevel;

            InitializeComponent();

            MessageBox.Show(player1.getUsername());
            MessageBox.Show(player2.getUsername());
           
        }
    }
}
