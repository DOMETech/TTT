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
using System.Data.SQLite;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        //String for database connection
        private string sqlconnection = "Data Source=TTTdatabase.sqlite;Version=3;";


        private User player1;
        private User player2;
        private int difficulty;
        private string difficultyStr;

        private bool playerOneTurn;     //Change true if it is player 1 turn or false if player 2 turn

        private int moveCounter;        //The move counter when set to 36 the game will end

        //Set all the options in the board


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
            player2 = new User("CPU");

            //Set difficulty
            difficulty = difficultyLevel;

            //Initilize components in the window
            InitializeComponent();

            //change the text of player 1 label and player 2 to 'CPU'
            player1Label.Content = player1.getUsername() + ":";
            player2Label.Content = player2.getUsername() + ":";

            selectPlayer1Label.Content = player1.getUsername();
            selectPlayer2Label.Content = player2.getUsername();

            //set difficultyStr
            if (difficulty == 1)
            {
                difficultyStr = "Easy";
            }
            else if (difficulty == 2)
            {
                difficultyStr = "Medium";
            }
            else
            {
                difficultyStr = "Hard";
            }

            //Change label to 'Single Player: difficyltyStr
            gameModeLabel.Content = "Single Player: " + difficultyStr;

            //change score label to 0
            score1Label.Content = player1.getScore();
            score2Label.Content = player2.getScore();

            //set move counter to 1
            moveCounter = 1;
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

            selectPlayer1Label.Content = player1.getUsername();
            selectPlayer2Label.Content = player2.getUsername();

            //set difficultyStr
            if (difficulty == 1)
            {
                difficultyStr = "Easy";
            }
            else if (difficulty == 2)
            {
                difficultyStr = "Medium";
            }
            else
            {
                difficultyStr = "Hard";
            }

            //Change label to 'Single Player: difficyltyStr
            gameModeLabel.Content = "Multi Player: " + difficultyStr;

            //change score label to 0
            score1Label.Content = player1.getScore();
            score2Label.Content = player2.getScore();

            //set move counter to 1
            moveCounter = 1;

        }


        //function to highlight labels with mouse hovering over it
        private void highlightLabel(object sender, MouseEventArgs e)
        {
            Label selectedLabel = sender as Label;
            selectedLabel.Foreground = new SolidColorBrush(Colors.Yellow);
            Mouse.OverrideCursor = Cursors.Hand;
            selectedLabel.FontSize += 10;
            selectedLabel.FontWeight = FontWeights.SemiBold;

        }

        //function to unhighlth labels when mouse leaves them
        private void unhighlightLabel(object sender, MouseEventArgs e)
        {
            Label selectedLabel = sender as Label;
            selectedLabel.Foreground = new SolidColorBrush(Colors.White);
            Mouse.OverrideCursor = Cursors.Arrow;
            selectedLabel.FontSize -= 10;
            selectedLabel.FontWeight = FontWeights.Regular;

        }

        private void selectPlayer1Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            playerOneTurn = true;
            selectWhoStartsPanel.Visibility = Visibility.Hidden;
            playerOneIndicator.Visibility = Visibility.Visible;

        }

        private void selectPlayer2Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            playerOneTurn = false;
            selectWhoStartsPanel.Visibility = Visibility.Hidden;
            playerTwoIndicatior.Visibility = Visibility.Visible;
        }

        //Exit game button prompts message and options
        private void exitGameMessage(object sender, MouseButtonEventArgs e)
        {
            //If it is press when player 1 turn is going on
            if (playerOneTurn)
            {
                //If player 1 is a guest
                if (player1.isGuest())
                {
                    //If player 2 is a not guest and it is not CPU display that it will add a win point to player 2
                    if (!player2.isGuest() && player2.getUsername() != "CPU")
                    {
                        exitMessLabel2.Content = "A winning point will be added to " + player2.getUsername() + ".";
                    }
                    //If player 2 is a guest or CPU no message needed
                    else
                    {
                        exitMessLabel2.Visibility = Visibility.Hidden;
                    }
                }
                //if player 1 is logged in
                else
                {
                    //If player 2 is not a guest and it is not the cpu display it will add a lose point to player 1 and that it will add a win point to player 2
                    if (!player2.isGuest() && player2.getUsername() != "CPU")
                    {
                        exitMessLabel2.Content = "A losing point will be added to " + player1.getUsername() + ".\nA winning point will be added to " + player2.getUsername() + ".";
                    }
                    //If player 2 is a guest or the CPU it will add a losing point to player 1
                    else
                    {
                        exitMessLabel2.Content = "A losing point will be added to " + player1.getUsername() + ".";
                    }

                }
            }
            //If it press when player 2 turn is going on
            else
            {
                //if player 2 is cpu
                if (player2.getUsername() == "CPU")
                {

                    if (!player1.isGuest())
                    {
                        exitMessLabel2.Content = "A losing point will be added to " + player1.getUsername() + ".";
                    }
                    else
                    {
                        exitMessLabel2.Visibility = Visibility.Hidden;
                    }
                }
                //if player 2 is guest
                else if (player2.isGuest())
                {
                    if (player1.isGuest())
                    {
                        exitMessLabel2.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        exitMessLabel2.Content = "A winning point will be added to " + player1.getUsername() + ".";
                    }
                }
                //if player 2 is logged in
                else
                {
                    //If player 1 is not a guest it will add a lose point to player 2 and add a win point to player 1
                    if (!player1.isGuest())
                    {
                        exitMessLabel2.Content = "A losing point will be added to " + player2.getUsername() + ".\nA winning point will be added to " + player1.getUsername() + ".";
                    }
                    else
                    {
                        exitMessLabel2.Content = "A losing point will be added to " + player2.getUsername() + ".";
                    }
                }

            }

            exitGamePrompt.Visibility = Visibility.Visible;
            exitButton.Visibility = Visibility.Hidden;

        }

        //Function to exit the game window
        private void exitGame(object sender, MouseButtonEventArgs e)
        {
            //If it is press when player 1 turn is going on
            if (playerOneTurn)
            {
                //If player 1 is a guest
                if (player1.isGuest())
                {
                    //If player 2 is a not guest and it is not CPU it will add a win point to player 2
                    if (!player2.isGuest() && player2.getUsername() != "CPU")
                    {
                        //add winning point to player 2
                        addWinningPoint(player2);

                        //set stats for player 2
                        setStats(player2, player2StatsLabel, player2StatsWinsLabel, player2StatsLosesLabel, player2StatsHighestScoreLabel);

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;

                        player1StatsPanel.Visibility = Visibility.Hidden;
                        player2StatsPanel.Visibility = Visibility.Visible;


                    }
                    //If player 2 is a guest or CPU 
                    else
                    {
                        //exit back to main menu since no information is display
                        backToMainMenu(sender, e);
                    }

                }
                //if player 1 is logged in
                else
                {
                    //If player 2 is not a guest and it is not the cpu display it will add a lose point to player 1 and that it will add a win point to player 2
                    if (!player2.isGuest() && player2.getUsername() != "CPU")
                    {
                        //A losing point will be added to player1
                        addLosingPoint(player1);

                        //A winning point will be added to player2
                        addWinningPoint(player2);

                        //set labels
                        setStats(player1, player1StatsLabel, player1StatsWinsLabel, player1StatsLosesLabel, player1StatsHighestScoreLabel);
                        setStats(player2, player2StatsLabel, player2StatsWinsLabel, player2StatsLosesLabel, player2StatsHighestScoreLabel);

                        //display panel and stats
                        displayPlayerStats.Visibility = Visibility.Visible;

                        player1StatsPanel.Visibility = Visibility.Visible;
                        player2StatsPanel.Visibility = Visibility.Visible;

                    }
                    //If player 2 is a guest or the CPU it will add a losing point to player 1
                    else
                    {
                        //A losing point will be added to player1
                        addLosingPoint(player1);

                        //set labels
                        setStats(player1, player1StatsLabel, player1StatsWinsLabel, player1StatsLosesLabel, player1StatsHighestScoreLabel);

                        //display panel and stats
                        displayPlayerStats.Visibility = Visibility.Visible;
                        player1StatsPanel.Visibility = Visibility.Visible;
                        player2StatsPanel.Visibility = Visibility.Hidden;
                    }

                }
            }
            //If it press when player 2 turn is going on
            else
            {
                //if player 2 is cpu
                if (player2.getUsername() == "CPU")
                {
                    //if player 1 is not a guest
                    if (!player1.isGuest())
                    {
                        //A losing point will be added to player1
                        addLosingPoint(player1);

                        //sets the stats labels 
                        setStats(player1, player1StatsLabel, player1StatsWinsLabel, player1StatsLosesLabel, player1StatsHighestScoreLabel);

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;
                        player1StatsPanel.Visibility = Visibility.Visible;
                        player2StatsPanel.Visibility = Visibility.Hidden;

                    }
                    else
                    {
                        //exit the gamewindow and open the main Meny window
                        backToMainMenu(sender, e);
                    }
                }
                //if player 2 is guest
                else if (player2.isGuest())
                {
                    //if player 1 is a guest
                    if (player1.isGuest())
                    {
                        //exit the gamewindow and open the main Meny window
                        backToMainMenu(sender, e);
                    }
                    //if player 1 is not a guest
                    else
                    {
                        //A winning point will be added to player1
                        addWinningPoint(player1);

                        //set labels
                        setStats(player1, player1StatsLabel, player1StatsWinsLabel, player1StatsLosesLabel, player1StatsHighestScoreLabel);

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;
                        player1StatsPanel.Visibility = Visibility.Visible;
                        player2StatsPanel.Visibility = Visibility.Hidden;
                    }
                }
                //if player 2 is logged in
                else
                {
                    //If player 1 is not a guest it will add a lose point to player 2 and add a win point to player 1
                    if (!player1.isGuest())
                    {
                        //A losing point will be added to player2
                        addLosingPoint(player2);

                        //A winning point will be added to player1
                        addWinningPoint(player1);

                        //set stats label
                        setStats(player1, player1StatsLabel, player1StatsWinsLabel, player1StatsLosesLabel, player1StatsHighestScoreLabel);
                        setStats(player2, player2StatsLabel, player2StatsWinsLabel, player2StatsLosesLabel, player2StatsHighestScoreLabel);

                        //display panel and stats
                        displayPlayerStats.Visibility = Visibility.Visible;
                        player1StatsPanel.Visibility = Visibility.Visible;
                        player2StatsPanel.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        //A losing point will be added to player2
                        addLosingPoint(player2);

                        //set stats for player 2
                        setStats(player2, player2StatsLabel, player2StatsWinsLabel, player2StatsLosesLabel, player2StatsHighestScoreLabel);

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;

                        player1StatsPanel.Visibility = Visibility.Hidden;
                        player2StatsPanel.Visibility = Visibility.Visible;

                    }
                }

            }

        }

        //Function to add a winning point to the database
        private void addWinningPoint(User player)
        {
            //Connect to the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);

            //open connection
            conn.Open();

            //sql query to be perform
            string sql = "SELECT * FROM " + difficultyStr + "Score WHERE username='" + player.getUsername() + "'";

            //create a command
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //execute command
            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //Update the winning column for the user
                sql = "UPDATE " + difficultyStr + "Score SET wins = wins + 1 WHERE username='" + player.getUsername() + "'";

                //create command
                cmd = new SQLiteCommand(sql, conn);

                //execute command
                cmd.ExecuteNonQuery();

                //Now lets check if the highest score needs to be updated for that table
                sql = "UPDATE " + difficultyStr + "Score SET highestScore = " + player.getScore() + "WHERE username='" +
                    player.getUsername() + "' AND highestScore<" + player.getScore();

                //create command
                cmd = new SQLiteCommand(sql, conn);

                //execute command
                cmd.ExecuteNonQuery();
            }
            else
            {
                //insert the user into table
                sql = "INSERT INTO " + difficultyStr + "Score (username, wins, loses, highestScore) VALUES('" +
                    player.getUsername() + "', 1, 0, " + player.getScore() + ")";

                //create command
                cmd = new SQLiteCommand(sql, conn);

                //execute command
                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        //Function to add a losing point to the database
        private void addLosingPoint(User player)
        {
            //Connect to the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);

            //open connection
            conn.Open();

            //sql query to be perform
            string sql = "SELECT * FROM " + difficultyStr + "Score WHERE username='" + player.getUsername() + "'";

            //create a command
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //execute command           
            SQLiteDataReader reader = cmd.ExecuteReader();

            bool found = reader.Read();

            conn.Close();

            if (found)
            {
                //Open connection
                conn.Open();

                //Update the winning column for the user
                sql = "UPDATE " + difficultyStr + "Score SET loses = loses + 1 WHERE username='" + player.getUsername() + "'";

                //create command
                cmd = new SQLiteCommand(sql, conn);

                //execute command
                cmd.ExecuteNonQuery();

                //Now lets check if the highest score needs to be updated for that table
                sql = "UPDATE " + difficultyStr + "Score SET highestScore = " + player.getScore() + " WHERE username='" +
                    player.getUsername() + "' AND highestScore<" + player.getScore();

                //create command
                cmd = new SQLiteCommand(sql, conn);

                //execute command
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            else
            {
                //insert the user into table
                conn.Open();

                //create sql
                string addsql = "INSERT INTO " + difficultyStr + "Score (username, wins, loses, highestScore) VALUES('" +
                    player.getUsername() + "', 0, 1, " + player.getScore().ToString() + ")";

                //create command
                cmd = new SQLiteCommand(addsql, conn);

                //execute command
                cmd.ExecuteNonQuery();

                conn.Close();

            }

        }

        //Function for button to go back to Main Menu
        private void backToMainMenu(object sender, MouseButtonEventArgs e)
        {
            MainWindow menuWindow = new MainWindow(true);
            menuWindow.Show();
            this.Close();
        }

        //If no option is selected when exit game menu is prompted
        private void backToGame(object sender, MouseButtonEventArgs e)
        {
            exitGamePrompt.Visibility = Visibility.Hidden;
            exitButton.Visibility = Visibility.Visible;
        }

        //Set the stats for player
        private void setStats(User player, object playerSender, object winsSender, object loseSender, object highestScoreSender)
        {
            Label playerLabel = playerSender as Label;
            Label winsLabel = winsSender as Label;
            Label loseLabel = loseSender as Label;
            Label highScoreLabel = highestScoreSender as Label;

            //Connect to the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);

            //open connection
            conn.Open();

            //sql query to be perform
            string sql = "SELECT * FROM " + difficultyStr + "Score WHERE username='" + player.getUsername() + "'";

            //create a command
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //execute command
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                playerLabel.Content = reader["username"] + ":";
                winsLabel.Content = "Wins: " + reader["wins"];
                loseLabel.Content = "Loses: " + reader["loses"];
                highScoreLabel.Content = "Highest Score: " + reader["highestScore"];
            }

            conn.Close();
        }

        //Database test
        private void databseTest(User player)
        {
            //Connect to the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);
            //open connection
            conn.Open();

            //create sql query
            string sql = "SELECT * FROM HardScore WHERE username='" + player.getUsername() + "'";

            //create command and add query
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //create reader to read data from table            
            SQLiteDataReader reader = cmd.ExecuteReader();

            string test = "empty";

            while (reader.Read())
            {
                test = "username: " + reader["username"] + " wins:" + reader["wins"] + "loses: " + reader["loses"];
            }

            MessageBox.Show(test);


            conn.Close();

        }


    }


}
