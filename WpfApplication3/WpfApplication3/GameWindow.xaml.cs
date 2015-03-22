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

        private int cpuScore;

        private bool playerOneTurn;     //Change true if it is player 1 turn or false if player 2 turn

        //Empty game
        public GameWindow()
        {
            InitializeComponent();

        }

        //Single player game
        public GameWindow(User player, int difficultyLevel)
        {
            //Create object for player 
            player1 = new User(player);
            
            //Set difficulty
            difficulty = difficultyLevel;
            
            //Initilize components in the window
            InitializeComponent();
            
            //change the text of player 1 label and player 2 to 'CPU'
            player1Label.Content = player1.getUsername()+":";
            player2Label.Content = "CPU:";

            //change game mode label to 'Single Player:' + difficulty
            if (difficulty == 1)
            {
                gameModeLabel.Content = "Single Player: Easy";
            }
            else if (difficulty == 2)
            {
                gameModeLabel.Content = "Single Player: Medium";
            }
            else
            {
                gameModeLabel.Content = "Single Player: Hard";
            }

            //change score label to 0
            score1Label.Content = player1.getScore();
            cpuScore = 0;
            score2Label.Content = cpuScore;
        }

        //Multiplayer game
        public GameWindow(User playerOne, User playerTwo, int difficultyLevel)
        {
            //create user objects for each player
            player1 = new User(playerOne);
            player2 = new User(playerTwo);

            //set difficulty
            difficulty = difficultyLevel;

            InitializeComponent();

            //change the text of player 1 label and player 2 label
            player1Label.Content = player1.getUsername() + ":";
            player2Label.Content = player2.getUsername() + ":";

            //change game mode label to 'Single Player:' + difficulty
            if (difficulty == 1)
            {
                gameModeLabel.Content = "Multi Player: Easy";
            }
            else if (difficulty == 2)
            {
                gameModeLabel.Content = "Multi Player: Medium";
            }
            else
            {
                gameModeLabel.Content = "Multi Player: Hard";
            }

            //change score label to 0
            score1Label.Content = player1.getScore();
            score2Label.Content = player2.getScore();
            
        }

       
    }
}
