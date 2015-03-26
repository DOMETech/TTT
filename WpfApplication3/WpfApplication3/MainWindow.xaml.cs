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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SQL Connection string
        private string sqlconnection = "Data Source=TTTdatabase.sqlite;Version=3;";
    
        private bool mainMenuScreenActive;
        private bool singlePlayerScreenActive;
        private bool multiPlayer1ScreenActive;
        private bool multiPlayer2ScreenActive;
        private User player1;
        private User player2;
        private bool singlePlayerGame;
        private bool sound;

        public MainWindow()
        {
            InitializeComponent();
            
            mainMenuPanel.Visibility = Visibility.Hidden;
            mainMenuScreenActive = false;
            singlePlayerPanel.Visibility = Visibility.Hidden;
            singlePlayerScreenActive = false;
            registerPanel.Visibility = Visibility.Hidden;
            multiPlayer1Panel.Visibility = Visibility.Hidden;
            multiPlayer1ScreenActive = false;
            multiPlayer2Panel.Visibility = Visibility.Hidden;
            multiPlayer2ScreenActive = false;
            viewScoresPanel.Visibility = Visibility.Hidden;
            difficultyPanel.Visibility = Visibility.Hidden;

            
            sound = false;//erase this if sound wanted in the program and uncomment the 2 lines bellow
            //sound = true;
            //soundElement.Play();

        }

        public MainWindow(bool alreadyOpened)
        {
            if (alreadyOpened)
            {
                InitializeComponent();

                startScreenMenu.Visibility = Visibility.Hidden;
                mainMenuPanel.Visibility = Visibility.Visible;
                mainMenuScreenActive = true;
                singlePlayerPanel.Visibility = Visibility.Hidden;
                singlePlayerScreenActive = false;
                registerPanel.Visibility = Visibility.Hidden;
                multiPlayer1Panel.Visibility = Visibility.Hidden;
                multiPlayer1ScreenActive = false;
                multiPlayer2Panel.Visibility = Visibility.Hidden;
                multiPlayer2ScreenActive = false;
                viewScoresPanel.Visibility = Visibility.Hidden;
                difficultyPanel.Visibility = Visibility.Hidden;


                sound = false;//erase this if sound wanted in the program and uncomment the 2 lines bellow
                //sound = true;
                //soundElement.Play();
            }
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

        //start game button in the first screen
        private void clickStartaGame(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Visible;
            startScreenMenu.Visibility = Visibility.Hidden;
            mainMenuScreenActive = true;

        }

        //Function to quit the program from the main menu
        private void quitProgram(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //sound button action
        private void soundOnOff(object sender, MouseButtonEventArgs e)
        {


            if (sound)
            {
                soundButton.Content = "Sound: Off";
                sound = false;

                try
                {
                    soundElement.Pause();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Error pausing sound:\n" + err);
                }

            }
            else
            {
                soundButton.Content = "Sound: On";
                sound = true;
                soundElement.Play();

            }
        }

        //looping function for sound
        private void soundElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            soundElement.Position = TimeSpan.Zero;
            soundElement.Play();
        }

        //single player button selection
        private void singlePayerButton(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Hidden;
            mainMenuScreenActive = false;
            singlePlayerPanel.Visibility = Visibility.Visible;
            singlePlayerScreenActive = true;

        }

       
        //Multiplayer button action
        private void multiplayerButton(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Hidden;
            mainMenuScreenActive = false;
            multiPlayer1Panel.Visibility = Visibility.Visible;
            multiPlayer1ScreenActive = true;
        }
               
        //Back to main menu button
        private void backToMainMenu(object sender, MouseButtonEventArgs e)
        {
            //Hide all the panels
            singlePlayerPanel.Visibility = Visibility.Hidden;
            multiPlayer1Panel.Visibility = Visibility.Hidden;
            multiPlayer2Panel.Visibility = Visibility.Hidden;
            difficultyPanel.Visibility = Visibility.Hidden;
            viewScoresPanel.Visibility = Visibility.Hidden;

            //Display Main Menu
            mainMenuPanel.Visibility = Visibility.Visible;            

            //Set main menu bool true all the others false
            singlePlayerScreenActive = false;
            multiPlayer1ScreenActive = false;
            multiPlayer2ScreenActive = false;
            mainMenuScreenActive = true;
        }

        //Back to player 1 button multiplayer 2
        private void backToPlayer1(object sender, MouseButtonEventArgs e)
        {
            multiPlayer2Panel.Visibility = Visibility.Hidden;
            multiPlayer1Panel.Visibility = Visibility.Visible;
            multiPlayer2ScreenActive = false;
            multiPlayer1ScreenActive = true;

        }

        //Registration button in Main Menu
        private void registration(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Hidden;
            singlePlayerPanel.Visibility = Visibility.Hidden;
            multiPlayer2Panel.Visibility = Visibility.Hidden;
            multiPlayer1Panel.Visibility = Visibility.Hidden;
            registerPanel.Visibility = Visibility.Visible;
        }

        //Enter button action in the registration menu
        private void enterButtonRegistration(object sender, MouseButtonEventArgs e)
        {
            if (userRegTextBox.Text.Length >= 1)
            {
                if (emailTextBox.Text.Length >= 1 && emailTextBox.Text.Contains('@') && emailTextBox.Text.Contains('.'))
                {
                    if (passRegPassBox.Password == confirmPassRegPassBox.Password && passRegPassBox.Password.Length >=1 && confirmPassRegPassBox.Password.Length >=1)
                    {
                        if (checkUsername(userRegTextBox.Text))
                        {
                            MessageBox.Show("The Username already exists please try another one");

                        }
                        else
                        {
                            registerUser(userRegTextBox.Text, passRegPassBox.Password, emailTextBox.Text);
                            backRegistration();
                        }
                    }
                    else
                    {
                        passRegPassBox.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        passRegPassBox.Password = "";
                        confirmPassRegPassBox.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        confirmPassRegPassBox.Password = "";
                        MessageBox.Show("The passwords do not match.\nPlease try again.");
                    }
                }
                else
                {
                    emailTextBox.Text = "";
                    emailTextBox.Background = new SolidColorBrush(Colors.LightSkyBlue);
                    MessageBox.Show("The email field is empty or it is not an email address.\nPLease try again.");
                }
            }
            else
            {
                userRegTextBox.Background = new SolidColorBrush(Colors.LightSkyBlue);
                userRegTextBox.Text = "";
                MessageBox.Show("The username field cannot be empty.\nPlease try again.");
            }
            
        }

        //This is the login button in the single player screen
        private void loginToSinglePlayerGame(object sender, MouseButtonEventArgs e)
        {
            if (usernameTextBoxSinglePlayer.Text.Length >= 1)
            {
                if (passwordBoxSinglePlayer.Password.Length >= 1)
                {
                    if (checkUsernameAndPassword(usernameTextBoxSinglePlayer.Text, passwordBoxSinglePlayer.Password))
                    {

                        //Create User object for single player
                        player1 = new User(usernameTextBoxSinglePlayer.Text);

                        //set single player game and display difficulty panel
                        singlePlayerGame = true;
                        difficultyPanel.Visibility = Visibility.Visible;
                        singlePlayerPanel.Visibility = Visibility.Hidden;


                    }
                    else
                    {
                        passwordBoxSinglePlayer.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        passwordBoxSinglePlayer.Password = "";
                        usernameTextBoxSinglePlayer.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        usernameTextBoxSinglePlayer.Text = "";
                        MessageBox.Show("The username and/or password you input do not match our records.\nPlease try again.");
                    }
                }
                else
                {
                    passwordBoxSinglePlayer.Background = new SolidColorBrush(Colors.LightSkyBlue);
                    passwordBoxSinglePlayer.Password = "";
                    MessageBox.Show("Please enter your password.");
                    
                }
            }
            else
            {
                usernameTextBoxSinglePlayer.Background = new SolidColorBrush(Colors.LightSkyBlue);
                usernameTextBoxSinglePlayer.Text = "";
                MessageBox.Show("Please enter a username or register.\nYou can also select to play as Guest but no score record will be kept.");
                
            }

        }

        //Login player 1 multiplayer
        private void loginMultiPlayer1(object sender, MouseButtonEventArgs e)
        {
            if (usernameBoxMultiPlayer1.Text.Length >= 1)
            {
                if (passwordBoxMultiPlayer1.Password.Length >= 1)
                {
                    if (checkUsernameAndPassword(usernameBoxMultiPlayer1.Text, passwordBoxMultiPlayer1.Password))
                    {

                        //Create user object for player 1
                        player1 = new User(usernameBoxMultiPlayer1.Text);

                        multiPlayer1Panel.Visibility = Visibility.Hidden;
                        multiPlayer1ScreenActive = false;
                        multiPlayer2Panel.Visibility = Visibility.Visible;
                        multiPlayer2ScreenActive = true;


                    }
                    else
                    {
                        passwordBoxMultiPlayer1.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        passwordBoxMultiPlayer1.Password = "";
                        usernameBoxMultiPlayer1.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        usernameBoxMultiPlayer1.Text = "";
                        MessageBox.Show("The username and/or password you input do not match our records./nPlease try again.");
                    }
                }
                else
                {
                    passwordBoxMultiPlayer1.Background = new SolidColorBrush(Colors.LightSkyBlue);
                    passwordBoxMultiPlayer1.Password = "";
                    MessageBox.Show("Please enter your password.");
                }
            }
            else
            {
                usernameBoxMultiPlayer1.Background = new SolidColorBrush(Colors.LightSkyBlue);
                usernameBoxMultiPlayer1.Text = "";
                MessageBox.Show("Please enter a username or register.\nYou can also select to play as Guest but no score record will be kept.");
            }
        }

        //Login PLayer 2 multiplayer
        private void loginMulti2(object sender, MouseButtonEventArgs e)
        {
            if (usernameBoxMultiPlayer2.Text.Length >= 1)
            {
                if (passwordBoxMultiPlayer2.Password.Length >= 1)
                {
                    if (checkUsernameAndPassword(usernameBoxMultiPlayer2.Text, passwordBoxMultiPlayer2.Password))
                    {
                        //Create user object for player 2
                        player2 = new User(usernameBoxMultiPlayer2.Text);

                        //Launch the difficulty panel and set singlePlayerGame false
                        singlePlayerGame = false;
                        difficultyPanel.Visibility = Visibility.Visible;
                        multiPlayer2Panel.Visibility = Visibility.Hidden;


                    }
                    else
                    {
                        passwordBoxMultiPlayer2.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        passwordBoxMultiPlayer2.Password = "";
                        usernameBoxMultiPlayer2.Background = new SolidColorBrush(Colors.LightSkyBlue);
                        usernameBoxMultiPlayer2.Text = "";
                        MessageBox.Show("The username and/or password you input do not match our records./nPlease try again.");
                    }
                }
                else
                {
                    passwordBoxMultiPlayer2.Background = new SolidColorBrush(Colors.LightSkyBlue);
                    passwordBoxMultiPlayer2.Password = "";
                    MessageBox.Show("Please enter your password.");
                }
            }
            else
            {
                usernameBoxMultiPlayer2.Background = new SolidColorBrush(Colors.LightSkyBlue);
                usernameBoxMultiPlayer2.Text = "";
                MessageBox.Show("Please enter a username or register.\nYou can also select to play as Guest but no score record will be kept.");
            }

        }

        //Player 1 play as guest multiplayer Game
        private void playAsGuestMulti1(object sender, MouseButtonEventArgs e)
        {
            //Create an empty user object since it is a guest
            player1 = new User();

            multiPlayer1Panel.Visibility = Visibility.Hidden;
            multiPlayer1ScreenActive = false;
            multiPlayer2Panel.Visibility = Visibility.Visible;
            multiPlayer2ScreenActive = true;

        }

        //PLayer 2 in multiplayer plays as guest
        private void playAsGuestMulti2(object sender, MouseButtonEventArgs e)
        {
            
            //Create an empty user object for player 2
            player2 = new User();

            //Launch the difficulty panel and set singlePlayerGame false
            singlePlayerGame = false;
            difficultyPanel.Visibility = Visibility.Visible;
            multiPlayer2Panel.Visibility = Visibility.Hidden;


        }
              

        //Back button registration
        private void backButtonReg(object sender, MouseButtonEventArgs e)
        {
            backRegistration();
        }

        //ViewScores button action
        private void viewScoresButtonAction(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Hidden;
            mainMenuScreenActive = false;
            setScoresLabels("Easy");
            viewScoresPanel.Visibility = Visibility.Visible;
        }

        //Play as guest single player
        private void playAsGuestSinglePlayer(object sender, MouseButtonEventArgs e)
        {
            player1 = new User();
            singlePlayerGame = true;

            //TO-DO dislpay difficulty levels
            difficultyPanel.Visibility = Visibility.Visible;
            singlePlayerPanel.Visibility = Visibility.Hidden;

        }

        //easy difficulty button
        private void easyButtonAction(object sender, MouseButtonEventArgs e)
        {
            
            createGame(singlePlayerGame, 1);

        }

        //Medium difficulty button
        private void mediumButtonAction(object sender, MouseButtonEventArgs e)
        {
            createGame(singlePlayerGame, 2);
        }

        //Hard difficulty button
        private void hardButtonAction(object sender, MouseButtonEventArgs e)
        {

            createGame(singlePlayerGame, 3);
        }

        //Change difficulty scores in the view scores panel
        private void changeDifficultyScores(object sender, MouseButtonEventArgs e)
        {
            Label difficultyButton = sender as Label;

            if (difficultyButton.Content.ToString() == "Easy Scores")
            {
                setScoresLabels("Easy");
            }
            else if (difficultyButton.Content.ToString() == "Medium Scores")
            {
                setScoresLabels("Medium");
            }
            else if (difficultyButton.Content.ToString() == "Hard Scores")
            {
                setScoresLabels("Hard");
            }
        }
        

        /*
         * 
         * 
         * Functions to be use int he program by the buttons actions
         * 
         * 
         * 
         * */
               
        //Creates the game difficulty 1=Easy, 2=Medium, 3=Hard
        private void createGame(bool playingMode, int difficulty)
        {
                     
            if (playingMode)//If single player
            {
                GameWindow game = new GameWindow(player1, difficulty);
                
                game.Show();
                this.Close();
            }
            else//if multiplayer
            {
                GameWindow game = new GameWindow(player1, player2, difficulty);

                game.Show();
                this.Close();

            }
            
        }

        //Function to set the labels for the view score panel
        private void setScoresLabels(string difficultySelected)
        {
            //create connection with the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);

            //open connection
            conn.Open();

            //create sql query to display 
            string sql = "SELECT * FROM " + difficultySelected + "Score ORDER BY wins DESC, highestScore DESC, loses ASC";

            //create command
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //execute command
            SQLiteDataReader reader;


            //arrays to store the information
            string[] usernames = new string[6];
            string[] wins = new string[6];
            string[] loses = new string[6];
            string[] highestScore = new string[6];


            try
            {
                reader = cmd.ExecuteReader();

                //counter
                int i = 0;

                //loop to get all the information stored
                while (reader.Read() && i < 6)//while reader has stuff and i is less than 6
                {
                    usernames[i] = reader["username"].ToString();
                    wins[i] = reader["wins"].ToString();
                    loses[i] = reader["loses"].ToString();
                    highestScore[i] = reader["highestScore"].ToString();

                    //increase counter
                    i++;

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            

            //close connection
            conn.Close();

            firstPlaceNameLabel.Content = "1. " + usernames[0];
            firstPlaceWinsLabel.Content = wins[0];
            firstPlaceLosesLabel.Content = loses[0];
            firstPlaceHighestLabel.Content = highestScore[0];

            secondPlaceNameLabel.Content = "2. " + usernames[1];
            secondPlaceWinsLabel.Content = wins[1];
            secondPlaceLosesLabel.Content = loses[1];
            secondPlaceHighestLabel.Content = highestScore[1];

            thirdPlaceNameLabel.Content = "3. " + usernames[2];
            thirdPlaceWinsLabel.Content = wins[2];
            thirdPlaceLosesLabel.Content = loses[2];
            thirdPlaceHighestLabel.Content = highestScore[2];

            fourthPlaceNameLabel.Content = "4. " + usernames[3];
            fourthPlaceWinsLabel.Content = wins[3];
            fourthPlaceLosesLabel.Content = loses[3];
            fourthPlaceHighestLabel.Content = highestScore[3];

            fifthPlaceNameLabel.Content = "5. " + usernames[4];
            fifthPlaceWinsLabel.Content = wins[4];
            fifthPlaceLosesLabel.Content = loses[4];
            fifthPlaceHighestLabel.Content = highestScore[4];

            sixthPlaceNameLabel.Content = "6. " + usernames[5];
            sixthPlaceWinsLabel.Content = wins[5];
            sixthPlaceLosesLabel.Content = loses[5];
            sixthPlaceHighestLabel.Content = highestScore[5];

            viewScoreTitleLabel.Content = "Top " + difficultySelected + " Scores";

            if (difficultySelected == "Easy")
            {                
                changeDifficultyScoresButton.Content = "Medium Scores";
                changeDifficultyScoresButton2.Content = "Hard Scores";
            }
            else if (difficultySelected == "Medium")
            {                
                changeDifficultyScoresButton.Content = "Easy Scores";
                changeDifficultyScoresButton2.Content = "Hard Scores";
            }
            else if (difficultySelected == "Hard")
            {
                changeDifficultyScoresButton.Content = "Easy Scores";
                changeDifficultyScoresButton2.Content = "Medium Scores";
            }

        }

        //Back action from registration screen
        private void backRegistration()
        {
            userRegTextBox.Clear();
            passRegPassBox.Clear();
            emailTextBox.Clear();
            confirmPassRegPassBox.Clear();

            if (singlePlayerScreenActive)//the previouse screen was the login single player screen
            {
                registerPanel.Visibility = Visibility.Hidden;
                singlePlayerPanel.Visibility = Visibility.Visible;

            }
            else if (mainMenuScreenActive)//the previouse screen was the main menu
            {
                registerPanel.Visibility = Visibility.Hidden;
                mainMenuPanel.Visibility = Visibility.Visible;

            }
            else if (multiPlayer1ScreenActive)
            {
                registerPanel.Visibility = Visibility.Hidden;
                multiPlayer1Panel.Visibility = Visibility.Visible;
            }
            else if (multiPlayer2ScreenActive)
            {
                registerPanel.Visibility = Visibility.Hidden;
                multiPlayer2Panel.Visibility = Visibility.Visible;
            }
            

        }
        
        //Check username and password function
        private bool checkUsernameAndPassword(string username, string password){

            //create connection with the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);

            //open connection
            conn.Open();
       
            //Create sql to check for username and password
            string sql = "SELECT username, password FROM Users WHERE username='"+username+"' AND password='" +password+"'";

            //create command
            SQLiteCommand cmd = new SQLiteCommand(sql,conn);
            //execute command
            SQLiteDataReader reader = cmd.ExecuteReader();

            bool found = reader.Read();//true if the username and password are found otherwise it returns false

            //close connection
            conn.Close();

            return found;//Returns found

        }

        //Function to check if username exists in the database
        private bool checkUsername(string username)
        {
            //Connect to the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);

            //open connection
            conn.Open();

            //sql query to be perform
            string sql = "SELECT * FROM Users WHERE username='" + username + "'";

            //create a command
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //execute command
            SQLiteDataReader reader = cmd.ExecuteReader();

            bool found = reader.Read();//true if username is found in the database false otherwise  

            //close connection
            conn.Close();

            return found;//Returns found        
           
        }

        //Function to register users to the database
        private void registerUser(string username, string password, string email)
        {
            //Create connection with the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);

            //open connection
            conn.Open();

            //create sql query to insert information into the table
            string sql = "INSERT INTO Users (username, password, email) VALUES ('" +
                    username + "', '" + password + "', '" + email + "')";

            //create command
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //execute command
            cmd.ExecuteNonQuery();

            /*
            //create in other tables 'EasyScore', 'MediumScore', and 'HardScore'
            sql = "INSERT INTO EasyScore (username, wins, loses, highestScore) Values ('"+
                    username + "', 0, 0, 0)";

            //create command
            cmd = new SQLiteCommand(sql, conn);

            //execute command
            cmd.ExecuteNonQuery();

            //medium
            sql = "INSERT INTO MediumScore (username, wins, loses, highestScore) Values ('" +
                    username + "', 0, 0, 0)";

            //create command
            cmd = new SQLiteCommand(sql, conn);

            //execute command
            cmd.ExecuteNonQuery();

            //medium
            sql = "INSERT INTO HardScore (username, wins, loses, highestScore) Values ('" +
                    username + "', 0, 0, 0)";

            //create command
            cmd = new SQLiteCommand(sql, conn);

            //execute command
            cmd.ExecuteNonQuery();
             */

            //close connection
            conn.Close();
        }

        //Action when selecting text boxes and password boses
        private void passwordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox selectedBox = sender as PasswordBox;
            selectedBox.SelectAll();
            selectedBox.Background = new SolidColorBrush(Colors.White);
        }

        private void usernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox selectedBox = sender as TextBox;
            selectedBox.SelectAll();
            selectedBox.Background = new SolidColorBrush(Colors.White);
        }

        private void usernameTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TextBox selectedBox = sender as TextBox;
            selectedBox.SelectAll();
            selectedBox.Background = new SolidColorBrush(Colors.White);
        }

        private void passwordTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            PasswordBox selectedBox = sender as PasswordBox;
            selectedBox.SelectAll();
            selectedBox.Background = new SolidColorBrush(Colors.White);
        }
        //
        //
        /*
         * 
         * 
         * testing functions and stuff below this
         * 
         * 
         * 
         *
         * 
         * */

        //This is just to check if the database works do not use in actual program
        private void databaseTest(object sender, MouseButtonEventArgs e)
        {
            //Connect to the database
            SQLiteConnection conn = new SQLiteConnection(sqlconnection);
            //open connection
            conn.Open();

            //create sql query
            string sql = "SELECT * FROM Users";

            //create command and add query
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //create reader to read data from table
            SQLiteDataReader reader = cmd.ExecuteReader();

            

            conn.Close();
            
        }


       
        

    }
}
