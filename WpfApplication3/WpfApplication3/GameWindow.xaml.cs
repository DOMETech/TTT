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
        private const string sqlconnection = "Data Source=TTTdatabase.sqlite;Version=3;";

        private User player1;
        private User player2;
        private int difficulty;
        private string difficultyStr;

        private bool playerOneTurn;     //Change true if it is player 1 turn or false if player 2 turn
        //Player1 will be 'X' and player2 will be 'O'

        private int moveCounter;        //The move counter when set to 36 the game will end

        //Set all the options in the board

        //list of buttons
        Button[] buttons;

        //Properties user for checking score and AI
        private bool[] ROWA;
        private bool[] ROWB;
        private bool[] ROWC;
        private bool[] ROWD;
        private bool[] ROWE;
        private bool[] ROWF;

        private bool[] COL1;
        private bool[] COL2;
        private bool[] COL3;
        private bool[] COL4;
        private bool[] COL5;
        private bool[] COL6;

        private bool[] DI1;
        private bool[] DI2;
        private bool[] DI3;
        private bool[] DI4;
        private bool[] DI5;
        private bool[] DI6;


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
            moveCounter = 0;

            //Set properties
            setProperties();
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
            moveCounter = 0;

            //Set properties
            setProperties();
        }

        //Method to set properties for the game
        private void setProperties()
        {

            //Array for checking scores
            ROWA = new bool[3];
            ROWB = new bool[3];
            ROWC = new bool[3];
            ROWD = new bool[3];
            ROWE = new bool[3];
            ROWF = new bool[3];

            COL1 = new bool[3];
            COL2 = new bool[3];
            COL3 = new bool[3];
            COL4 = new bool[3];
            COL5 = new bool[3];
            COL6 = new bool[3];

            DI1 = new bool[3];
            DI2 = new bool[3];
            DI3 = new bool[3];
            DI4 = new bool[3];
            DI5 = new bool[3];
            DI6 = new bool[3];

            //SET arrays
            for (int i = 0; i < 3; i++)
            {
                //set row wins comb
                ROWA[i] = false;
                ROWB[i] = false;
                ROWC[i] = false;
                ROWD[i] = false;
                ROWE[i] = false;
                ROWF[i] = false;

                //set col wins comb
                COL1[i] = false;
                COL2[i] = false;
                COL3[i] = false;
                COL4[i] = false;
                COL5[i] = false;
                COL6[i] = false;

                //set diagonal win comb
                DI1[i] = false;
                DI2[i] = false;
                DI3[i] = false;
                DI4[i] = false;
                DI5[i] = false;
                DI6[i] = false;

            }

            buttons = new Button[36];

            buttons[0] = A1;
            buttons[1] = A2;
            buttons[2] = A3;
            buttons[3] = A4;
            buttons[4] = A5;
            buttons[5] = A6;
            buttons[6] = B1;
            buttons[7] = B2;
            buttons[8] = B3;
            buttons[9] = B4;
            buttons[10] = B5;
            buttons[11] = B6;
            buttons[12] = C1;
            buttons[13] = C2;
            buttons[14] = C3;
            buttons[15] = C4;
            buttons[16] = C5;
            buttons[17] = C6;
            buttons[18] = D1;
            buttons[19] = D2;
            buttons[20] = D3;
            buttons[21] = D4;
            buttons[22] = D5;
            buttons[23] = D6;
            buttons[24] = E1;
            buttons[25] = E2;
            buttons[26] = E3;
            buttons[27] = E4;
            buttons[28] = E5;
            buttons[29] = E6;
            buttons[30] = F1;
            buttons[31] = F2;
            buttons[32] = F3;
            buttons[33] = F4;
            buttons[34] = F5;
            buttons[35] = F6;

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

        private void selectPlayer1Action(object sender, MouseButtonEventArgs e)
        {
            playerOneTurn = true;
            displayGameBoard();

        }

        private void selectPlayer2Action(object sender, MouseButtonEventArgs e)
        {
            playerOneTurn = false;
            displayGameBoard();
        }
        
        //Function to display gameBoard
        private void displayGameBoard(){
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
                        guestPlayerCpuStats();
                        setWinner();

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;

                    }
                    //If player 2 is a guest or CPU 
                    else
                    {
                        guestPlayerCpuStats();
                        setWinner();

                        //exit back to main menu since no information is display
                        displayPlayerStats.Visibility = Visibility.Visible;


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
                        setWinner();

                        //display panel and stats
                        displayPlayerStats.Visibility = Visibility.Visible;


                    }
                    //If player 2 is a guest or the CPU it will add a losing point to player 1
                    else
                    {
                        //A losing point will be added to player1
                        addLosingPoint(player1);

                        //set labels
                        setStats(player1, player1StatsLabel, player1StatsWinsLabel, player1StatsLosesLabel, player1StatsHighestScoreLabel);
                        guestPlayerCpuStats();
                        setWinner();

                        //display panel and stats
                        displayPlayerStats.Visibility = Visibility.Visible;

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
                        guestPlayerCpuStats();
                        setWinner();

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;


                    }
                    else
                    {
                        guestPlayerCpuStats();
                        setWinner();

                        //exit the gamewindow and open the main Meny window
                        displayPlayerStats.Visibility = Visibility.Visible;

                    }
                }
                //if player 2 is guest
                else if (player2.isGuest())
                {
                    //if player 1 is a guest
                    if (player1.isGuest())
                    {
                        guestPlayerCpuStats();
                        setWinner();

                        //exit the gamewindow and open the main Meny window
                        displayPlayerStats.Visibility = Visibility.Visible;
                    }
                    //if player 1 is not a guest
                    else
                    {
                        //A winning point will be added to player1
                        addWinningPoint(player1);

                        //set labels
                        setStats(player1, player1StatsLabel, player1StatsWinsLabel, player1StatsLosesLabel, player1StatsHighestScoreLabel);
                        guestPlayerCpuStats();
                        setWinner();

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;

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
                        setWinner();

                        //display panel and stats
                        displayPlayerStats.Visibility = Visibility.Visible;


                    }
                    //If player 1 is guest
                    else
                    {
                        //A losing point will be added to player2
                        addLosingPoint(player2);

                        //set stats
                        setStats(player2, player2StatsLabel, player2StatsWinsLabel, player2StatsLosesLabel, player2StatsHighestScoreLabel);
                        guestPlayerCpuStats();
                        setWinner();

                        //display everything
                        displayPlayerStats.Visibility = Visibility.Visible;

                    }
                }

            }

        }

        //Set winner in stats label
        private void setWinner()
        {
            if (moveCounter == 36)
            {
                if (player1.getScore() > player2.getScore())
                {
                    player1StatsLabel.Content += " Wins!";
                    player2StatsLabel.Content += " Loses!";
                }
                else if (player2.getScore() > player1.getScore())
                {
                    player1StatsLabel.Content += " Losess!";
                    player2StatsLabel.Content += " Wins!";
                }
                else if (player1.getScore() == player2.getScore())
                {
                    player1StatsLabel.Content += " Tie";
                    player2StatsLabel.Content += " Tie";
                }
            }
            else
            {
                if (playerOneTurn)
                {
                    player1StatsLabel.Content += " Losess!";
                    player2StatsLabel.Content += " Wins!";
                }
                else
                {
                    if (player2.getUsername() != "CPU")
                    {
                        player1StatsLabel.Content += " Wins!";
                        player2StatsLabel.Content += " Loses!";
                    }
                    else
                    {
                        player1StatsLabel.Content += " Losess!";
                        player2StatsLabel.Content += " Wins!";
                    }
                }
            }

        }

        //Set Guest player stats
        private void guestPlayerCpuStats()
        {
            if (player1.isGuest())
                player1StatsLabel.Content = player1.getUsername();


            if (player2.isGuest() || player2.getUsername() == "CPU")
                player2StatsLabel.Content = player2.getUsername();

            if (player1.isGuest())
                player1StatsWinsLabel.Content = "Score: " + player1.getScore();

            if (player2.isGuest() || player2.getUsername() == "CPU")
                player2StatsWinsLabel.Content = "Score: " + player2.getScore();

            if (player1.isGuest())
                player1StatsLosesLabel.Visibility = Visibility.Hidden;

            if (player2.isGuest() || player2.getUsername() == "CPU")
                player2StatsLosesLabel.Visibility = Visibility.Hidden;

            if (player1.isGuest())
                player1StatsHighestScoreLabel.Visibility = Visibility.Hidden;

            if (player2.isGuest() || player2.getUsername() == "CPU")
                player2StatsHighestScoreLabel.Visibility = Visibility.Hidden;

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

            bool found = reader.Read();//true if found false if not

            conn.Close();

            if (found)
            {
                //open connection
                conn.Open();

                //Update the winning column for the user
                sql = "UPDATE " + difficultyStr + "Score SET wins = wins + 1 WHERE username='" + player.getUsername() + "'";

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
                conn.Open();

                //insert the user into table
                sql = "INSERT INTO " + difficultyStr + "Score (username, wins, loses, highestScore) VALUES('" +
                    player.getUsername() + "', 1, 0, " + player.getScore() + ")";

                //create command
                cmd = new SQLiteCommand(sql, conn);

                //execute command
                cmd.ExecuteNonQuery();

                conn.Close();
            }


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

        //Place mark on board "Button click action"
        private void placeMark(object sender, RoutedEventArgs e)
        {
            Button selectedButton = sender as Button;

            if (playerOneTurn && selectedButton.Content.ToString() == "")
            {
                selectedButton.Content = "X";
                selectedButton.FontSize = 60;
                selectedButton.FontWeight = FontWeights.SemiBold;
                selectedButton.Foreground = new SolidColorBrush(Colors.DarkGray);

                //Increment move counter
                moveCounter++;

                //Check if the the user score
                checkScore();

                //Change the player indicator
                playerTwoIndicatior.Visibility = Visibility.Visible;
                playerOneIndicator.Visibility = Visibility.Hidden;


                //Switch players
                playerOneTurn = false;

                if (player2.getUsername() == "CPU")
                {
                    AI();
                }
            }
            else if (!playerOneTurn && selectedButton.Content.ToString() == "")
            {
                //If player2 is not the CPU
                if (player2.getUsername() != "CPU")
                {
                    selectedButton.Content = "O";
                    selectedButton.FontSize = 60;
                    selectedButton.Foreground = new SolidColorBrush(Colors.LightGray);
                    selectedButton.FontWeight = FontWeights.SemiBold;

                    //Increment move counter
                    moveCounter++;

                    //Check if the user score
                    checkScore();

                    //Change the player indicator
                    playerTwoIndicatior.Visibility = Visibility.Hidden;
                    playerOneIndicator.Visibility = Visibility.Visible;

                    //Switch players
                    playerOneTurn = true;
                }

            }


            //Check if the game is over
            if (moveCounter == 36)
            {
                MouseButtonEventArgs ee = e as MouseButtonEventArgs;
                exitGame(sender, ee);
            }

        }
        //Check score of game
        private void checkScore()
        {
            for (int i = 0; i < 18; i++)
            {
                checkIfUserScore(checkHorizontal());
                checkIfUserScore(checkVerticalScore());
                checkIfUserScore(checkDiagonal());
            }
        }

        //Check if user score
        private void checkIfUserScore(bool score)
        {
            //If somebody score then add a point to the player
            if (score)
            {
                if (playerOneTurn)
                {
                    player1.addScore();
                    score1Label.Content = player1.getScore();
                }
                else
                {
                    player2.addScore();
                    score2Label.Content = player2.getScore();
                }
            }
        }

        //Check for vertical score
        private bool checkVerticalScore()
        {
            //VERTICAL CHECK
            // COL1
            if (A1.Content != "" && B1.Content != "" && C1.Content != "" && D1.Content != "")
            {
                if ((A1.Content == B1.Content) && (B1.Content == C1.Content) && (C1.Content == D1.Content) && !COL1[0])
                    return COL1[0] = true;
            }
            if (E1.Content != "" && B1.Content != "" && C1.Content != "" && D1.Content != "")
            {
                if ((B1.Content == C1.Content) && (C1.Content == D1.Content) && (D1.Content == E1.Content) && !COL1[1])
                    return COL1[1] = true;
            }
            if (E1.Content != "" && F1.Content != "" && C1.Content != "" && D1.Content != "")
            {
                if ((C1.Content == D1.Content) && (D1.Content == E1.Content) && (E1.Content == F1.Content) && !COL1[2])
                    return COL1[2] = true;
            }

            // COL2
            if (A2.Content != "" && B2.Content != "" && C2.Content != "" && D2.Content != "")
            {
                if ((A2.Content == B2.Content) && (B2.Content == C2.Content) && (C2.Content == D2.Content) && !COL2[0])
                    return COL2[0] = true;
            }
            if (E2.Content != "" && B2.Content != "" && C2.Content != "" && D2.Content != "")
            {
                if ((B2.Content == C2.Content) && (C2.Content == D2.Content) && (D2.Content == E2.Content) && !COL2[1])
                    return COL2[1] = true;
            }
            if (E2.Content != "" && F2.Content != "" && C2.Content != "" && D2.Content != "")
            {
                if ((C2.Content == D2.Content) && (D2.Content == E2.Content) && (E2.Content == F2.Content) && !COL2[2])
                    return COL2[2] = true;
            }

            // COL3
            if (A3.Content != "" && B3.Content != "" && C3.Content != "" && D3.Content != "")
            {
                if ((A3.Content == B3.Content) && (B3.Content == C3.Content) && (C3.Content == D3.Content) && !COL3[0])
                    return COL3[0] = true;
            }
            if (E3.Content != "" && B3.Content != "" && C3.Content != "" && D3.Content != "")
            {
                if ((B3.Content == C3.Content) && (C3.Content == D3.Content) && (D3.Content == E3.Content) && !COL3[1])
                    return COL3[1] = true;
            }
            if (E3.Content != "" && F3.Content != "" && C3.Content != "" && D3.Content != "")
            {
                if ((C3.Content == D3.Content) && (D3.Content == E3.Content) && (E3.Content == F3.Content) && !COL3[2])
                    return COL3[2] = true;
            }

            //COL4
            if (A4.Content != "" && B4.Content != "" && C4.Content != "" && D4.Content != "")
            {
                if ((A4.Content == B4.Content) && (B4.Content == C4.Content) && (C4.Content == D4.Content) && !COL4[0])
                    return COL4[0] = true;
            }
            if (E4.Content != "" && B4.Content != "" && C4.Content != "" && D4.Content != "")
            {
                if ((B4.Content == C4.Content) && (C4.Content == D4.Content) && (D4.Content == E4.Content) && !COL4[1])
                    return COL4[1] = true;
            }
            if (E4.Content != "" && F4.Content != "" && C4.Content != "" && D4.Content != "")
            {
                if ((C4.Content == D4.Content) && (D4.Content == E4.Content) && (E4.Content == F4.Content) && !COL4[2])
                    return COL4[2] = true;
            }

            //COL5
            if (A5.Content != "" && B5.Content != "" && C5.Content != "" && D5.Content != "")
            {
                if ((A5.Content == B5.Content) && (B5.Content == C5.Content) && (C5.Content == D5.Content) && !COL5[0])
                    return COL5[0] = true;
            }
            if (E5.Content != "" && B5.Content != "" && C5.Content != "" && D5.Content != "")
            {
                if ((B5.Content == C5.Content) && (C5.Content == D5.Content) && (D5.Content == E5.Content) && !COL5[1])
                    return COL5[1] = true;
            }
            if (E5.Content != "" && F5.Content != "" && C5.Content != "" && D5.Content != "")
            {
                if ((C5.Content == D5.Content) && (D5.Content == E5.Content) && (E5.Content == F5.Content) && !COL5[2])
                    return COL5[2] = true;
            }

            //COL6
            if (A6.Content != "" && B6.Content != "" && C6.Content != "" && D6.Content != "")
            {
                if ((A6.Content == B6.Content) && (B6.Content == C6.Content) && (C6.Content == D6.Content) && !COL6[0])
                    return COL6[0] = true;
            }
            if (E6.Content != "" && B6.Content != "" && C6.Content != "" && D6.Content != "")
            {
                if ((B6.Content == C6.Content) && (C6.Content == D6.Content) && (D6.Content == E6.Content) && !COL6[1])
                    return COL6[1] = true;
            }
            if (E6.Content != "" && F6.Content != "" && C6.Content != "" && D6.Content != "")
            {
                if ((C6.Content == D6.Content) && (D6.Content == E6.Content) && (E6.Content == F6.Content) && !COL6[2])
                    return COL6[2] = true;
            }

            return false;
        }

        //Check for horizontal score
        private bool checkHorizontal()
        {
            //HORIZONTAL CHECK

            //ROW A
            if (A1.Content != "" && A2.Content != "" && A3.Content != "" && A4.Content != "")
            {
                if ((A1.Content == A2.Content) && (A2.Content == A3.Content) && (A3.Content == A4.Content) && !ROWA[0])
                    return ROWA[0] = true;
            }

            if (A2.Content != "" && A3.Content != "" && A4.Content != "" && A5.Content != "")
            {
                if ((A2.Content == A3.Content) && (A3.Content == A4.Content) && (A4.Content == A5.Content) && !ROWA[1])
                    return ROWA[1] = true;
            }

            if (A3.Content != "" && A4.Content != "" && A5.Content != "" && A6.Content != "")
            {
                if ((A3.Content == A4.Content) && (A4.Content == A5.Content) && (A5.Content == A6.Content) && !ROWA[2])
                    return ROWA[2] = true;
            }

            // ROW B
            if (B1.Content != "" && B2.Content != "" && B3.Content != "" && B4.Content != "")
            {
                if ((B1.Content == B2.Content) && (B2.Content == B3.Content) && (B3.Content == B4.Content) && !ROWB[0])
                    return ROWB[0] = true;
            }

            if (B5.Content != "" && B2.Content != "" && B3.Content != "" && B4.Content != "")
            {
                if ((B2.Content == B3.Content) && (B3.Content == B4.Content) && (B4.Content == B5.Content) && !ROWB[1])
                    return ROWB[1] = true;
            }

            if (B6.Content != "" && B5.Content != "" && B3.Content != "" && B4.Content != "")
            {
                if ((B3.Content == B4.Content) && (B4.Content == B5.Content) && (B5.Content == B6.Content) && !ROWB[2])
                    return ROWB[2] = true;
            }

            // ROW C
            if (C1.Content != "" && C2.Content != "" && C3.Content != "" && C4.Content != "")
            {
                if ((C1.Content == C2.Content) && (C2.Content == C3.Content) && (C3.Content == C4.Content) && !ROWC[0])
                    return ROWC[0] = true;
            }

            if (C5.Content != "" && C2.Content != "" && C3.Content != "" && C4.Content != "")
            {
                if ((C2.Content == C3.Content) && (C3.Content == C4.Content) && (C4.Content == C5.Content) && !ROWC[1])
                    return ROWC[1] = true;
            }

            if (C5.Content != "" && C6.Content != "" && C3.Content != "" && C4.Content != "")
            {
                if ((C3.Content == C4.Content) && (C4.Content == C5.Content) && (C5.Content == C6.Content) && !ROWC[2])
                    return ROWC[2] = true;
            }

            // ROW D
            if (D1.Content != "" && D2.Content != "" && D3.Content != "" && D4.Content != "")
            {
                if ((D1.Content == D2.Content) && (D2.Content == D3.Content) && (D3.Content == D4.Content) && !ROWD[0])
                    return ROWD[0] = true;
            }

            if (D5.Content != "" && D2.Content != "" && D3.Content != "" && D4.Content != "")
            {
                if ((D2.Content == D3.Content) && (D3.Content == D4.Content) && (D4.Content == D5.Content) && !ROWD[1])
                    return ROWD[1] = true;
            }

            if (D5.Content != "" && D6.Content != "" && D3.Content != "" && D4.Content != "")
            {
                if ((D3.Content == D4.Content) && (D4.Content == D5.Content) && (D5.Content == D6.Content) && !ROWD[2])
                    return ROWD[2] = true;
            }

            // ROW E
            if (E1.Content != "" && E2.Content != "" && E3.Content != "" && E4.Content != "")
            {
                if ((E1.Content == E2.Content) && (E2.Content == E3.Content) && (E3.Content == E4.Content) && !ROWE[0])
                    return ROWE[0] = true;
            }

            if (E5.Content != "" && E2.Content != "" && E3.Content != "" && E4.Content != "")
            {
                if ((E2.Content == E3.Content) && (E3.Content == E4.Content) && (E4.Content == E5.Content) && !ROWE[1])
                    return ROWE[1] = true;
            }

            if (E5.Content != "" && E6.Content != "" && E3.Content != "" && E4.Content != "")
            {
                if ((E3.Content == E4.Content) && (E4.Content == E5.Content) && (E5.Content == E6.Content) && !ROWE[2])
                    return ROWE[2] = true;
            }

            // ROW F
            if (F1.Content != "" && F2.Content != "" && F3.Content != "" && F4.Content != "")
            {
                if ((F1.Content == F2.Content) && (F2.Content == F3.Content) && (F3.Content == F4.Content) && !ROWF[0])
                    return ROWF[0] = true;
            }

            if (F5.Content != "" && F2.Content != "" && F3.Content != "" && F4.Content != "")
            {
                if ((F2.Content == F3.Content) && (F3.Content == F4.Content) && (F4.Content == F5.Content) && !ROWF[1])
                    return ROWF[1] = true;
            }

            if (F5.Content != "" && F6.Content != "" && F3.Content != "" && F4.Content != "")
            {
                if ((F3.Content == F4.Content) && (F4.Content == F5.Content) && (F5.Content == F6.Content) && !ROWF[2])
                    return ROWF[2] = true;
            }

            return false;

        }

        //Check for diagonal score
        private bool checkDiagonal()
        {
            //Diagonal
            //A
            if (A1.Content != "" && B2.Content != "" && C3.Content != "" && D4.Content != "")
            {
                if ((A1.Content == B2.Content) && (B2.Content == C3.Content) && (C3.Content == D4.Content) && !DI1[0])
                    return DI1[0] = true;
            }
            if (A2.Content != "" && B3.Content != "" && C4.Content != "" && D5.Content != "")
            {
                if ((A2.Content == B3.Content) && (B3.Content == C4.Content) && (C4.Content == D5.Content) && !DI1[1])
                    return DI1[1] = true;
            }
            if (A3.Content != "" && B4.Content != "" && C5.Content != "" && D6.Content != "")
            {
                if ((A3.Content == B4.Content) && (B4.Content == C3.Content) && (C3.Content == D6.Content) && !DI1[2])
                    return DI1[2] = true;
            }

            // B
            if (B1.Content != "" && C2.Content != "" && D3.Content != "" && E4.Content != "")
            {
                if ((B1.Content == C2.Content) && (C2.Content == D3.Content) && (D3.Content == E4.Content) && !DI2[0])
                    return DI2[0] = true;
            }
            if (B2.Content != "" && C3.Content != "" && D4.Content != "" && E5.Content != "")
            {
                if ((B2.Content == C3.Content) && (C3.Content == D4.Content) && (D4.Content == E5.Content) && !DI2[1])
                    return DI2[1] = true;
            }
            if (B3.Content != "" && C4.Content != "" && D5.Content != "" && E6.Content != "")
            {
                if ((B3.Content == C4.Content) && (C4.Content == D5.Content) && (D5.Content == E6.Content) && !DI2[2])
                    return DI2[2] = true;
            }

            // C
            if (C1.Content != "" && D2.Content != "" && E3.Content != "" && F4.Content != "")
            {
                if ((C1.Content == D2.Content) && (D2.Content == E3.Content) && (E3.Content == F4.Content) && !DI3[0])
                    return DI3[0] = true;
            }
            if (C2.Content != "" && D3.Content != "" && E4.Content != "" && F5.Content != "")
            {
                if ((C2.Content == D3.Content) && (D3.Content == E4.Content) && (E4.Content == F5.Content) && !DI3[1])
                    return DI3[1] = true;
            }
            if (C3.Content != "" && D4.Content != "" && E5.Content != "" && F6.Content != "")
            {
                if ((C3.Content == D4.Content) && (D4.Content == E5.Content) && (E5.Content == F6.Content) && !DI3[2])
                    return DI3[2] = true;
            }

            //D
            if (D1.Content != "" && C2.Content != "" && B3.Content != "" && A4.Content != "")
            {
                if ((D1.Content == C2.Content) && (C2.Content == B3.Content) && (B3.Content == A4.Content) && !DI4[0])
                    return DI4[0] = true;
            }
            if (D2.Content != "" && C3.Content != "" && B4.Content != "" && A5.Content != "")
            {
                if ((D2.Content == C3.Content) && (C3.Content == B4.Content) && (B4.Content == A5.Content) && !DI4[1])
                    return DI4[1] = true;
            }
            if (D3.Content != "" && C4.Content != "" && B5.Content != "" && A6.Content != "")
            {
                if ((D3.Content == C4.Content) && (C4.Content == B5.Content) && (B5.Content == A6.Content) && !DI4[2])
                    return DI4[2] = true;
            }

            // E
            if (E1.Content != "" && D2.Content != "" && C3.Content != "" && B4.Content != "")
            {
                if ((E1.Content == D2.Content) && (D2.Content == C3.Content) && (C3.Content == B4.Content) && !DI5[0])
                    return DI5[0] = true;
            }
            if (E2.Content != "" && D3.Content != "" && C4.Content != "" && B5.Content != "")
            {
                if ((E2.Content == D3.Content) && (D3.Content == C4.Content) && (C4.Content == B5.Content) && !DI5[1])
                    return DI5[1] = true;
            }
            if (E3.Content != "" && D4.Content != "" && C5.Content != "" && B6.Content != "")
            {
                if ((E3.Content == D4.Content) && (D4.Content == C5.Content) && (C5.Content == B6.Content) && !DI5[2])
                    return DI5[2] = true;
            }

            // F
            if (F1.Content != "" && E2.Content != "" && D3.Content != "" && C4.Content != "")
            {
                if ((F1.Content == E2.Content) && (E2.Content == D3.Content) && (D3.Content == C4.Content) && !DI6[0])
                    return DI6[0] = true;
            }
            if (F2.Content != "" && E3.Content != "" && D4.Content != "" && C5.Content != "")
            {
                if ((F2.Content == E3.Content) && (E3.Content == D4.Content) && (D4.Content == C5.Content) && !DI6[1])
                    return DI6[1] = true;
            }
            if (F3.Content != "" && E4.Content != "" && D5.Content != "" && C6.Content != "")
            {
                if ((F3.Content == E4.Content) && (E4.Content == D5.Content) && (D5.Content == C6.Content) && !DI6[2])
                    return DI6[2] = true;
            }

            return false;
        }


        private void AI()
        {
            if (difficulty == 1)
            {
                easyAI();

                //Increment move counter
                moveCounter++;

                //Check if the user score
                checkScore();

                //Change the player indicator
                playerTwoIndicatior.Visibility = Visibility.Hidden;
                playerOneIndicator.Visibility = Visibility.Visible;

                //Switch players
                playerOneTurn = true;
            }
            else if (difficulty == 2)
            {
                int num;

                Random rand = new Random();

                num = rand.Next(1);

                if (num == 0)
                {
                    easyAI();
                }
                else
                {
                    hardAI();
                }
            }
            else if (difficulty == 3)
            {

            }

        }

        private void easyAI()
        {
            RoutedEventArgs e = new RoutedEventArgs();

            int buttonNum;
            bool empty = false;

            int size = buttons.Length;

            MessageBox.Show(size.ToString());
            do
            {

                Random rand = new Random();

                buttonNum = rand.Next(size);

                if (buttons[buttonNum].Content == "")
                {
                    buttons[buttonNum].Content = "O";
                    buttons[buttonNum].FontSize = 60;
                    buttons[buttonNum].Foreground = new SolidColorBrush(Colors.LightGray);
                    buttons[buttonNum].FontWeight = FontWeights.SemiBold;

                    empty = true;


                }
                //Store in temporary array buttons left
                Button[] temp = new Button[size - 1];
                int j = 0;
                for (int i = 0; i < size; i++)
                {
                    if (i != buttonNum)
                    {
                        temp[j] = buttons[i];
                        j++;
                    }
                }

                //Set new buttons array with new length and buttons from temporary buttons array

                buttons = new Button[size - 1];

                for (int i = 0; i < size - 1; i++)
                {
                    buttons[i] = temp[i];
                }

                //Reduce size by one
                size--;


            } while (!empty);
        }

        private void hardAI()
        {

        }

        //button test
        private void buttonCheck(object sender, RoutedEventArgs e)
        {
            buttons[4].Content = "WORKS";
        }
        /*
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */





    }


}
