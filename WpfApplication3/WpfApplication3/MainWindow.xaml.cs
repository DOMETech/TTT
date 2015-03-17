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
        private bool sound;
        private string sqlconnection = "Data Source=TTTdatabase.sqlite;Version=3;";
        private bool mainMenuScreenActive;
        private bool singlePlayerScreenActive;
        private bool multiPlayer1ScreenActive;
        private bool multiPlayer2ScreenActive;
        private User player1;
        private User player2;

        public MainWindow()
        {
            InitializeComponent();
            mainMenuPanel.Visibility = Visibility.Hidden;
            mainMenuScreenActive = false;
            loginSinglePlayer.Visibility = Visibility.Hidden;
            singlePlayerScreenActive = false;
            registerPanel.Visibility = Visibility.Hidden;
            multiPlayer1Panel.Visibility = Visibility.Hidden;
            multiPlayer1ScreenActive = false;
            multiPlayer2Panel.Visibility = Visibility.Hidden;
            multiPlayer2ScreenActive = false;

            
            sound = false;//erase this if sound wanted in the program and uncomment the 2 lines bellow
            //sound = true;
            //soundElement.Play();

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
            loginSinglePlayer.Visibility = Visibility.Visible;
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
            loginSinglePlayer.Visibility = Visibility.Hidden;
            multiPlayer1Panel.Visibility = Visibility.Hidden;
            multiPlayer2Panel.Visibility = Visibility.Hidden;
            mainMenuPanel.Visibility = Visibility.Visible;
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

        private void registration(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Hidden;
            loginSinglePlayer.Visibility = Visibility.Hidden;
            multiPlayer2Panel.Visibility = Visibility.Hidden;
            multiPlayer1Panel.Visibility = Visibility.Hidden;
            registerPanel.Visibility = Visibility.Visible;
        }

        private void enterButtonRegistration(object sender, MouseButtonEventArgs e)
        {

            if (passRegPassBox.Password == confirmPassRegPassBox.Password)
            {              
               

                if (checkUsername(userRegTextBox.Text)){

                    MessageBox.Show("The Username already exists please try another one");

                }
                else
                {
                    registerUser(userRegTextBox.Text, passRegPassBox.Password, emailTextBox.Text);
                    
                    backRegistration();                  
                  
                }
            }
            
        }

        //This is the login button in the single player screen
        private void loginToSinglePlayerGame(object sender, MouseButtonEventArgs e)
        {

            if (checkUsernameAndPassword(usernameTextBoxSinglePlayer.Text, passwordBoxSinglePlayer.Password))
            {

                //Create User object for single player
                player1 = new User(usernameTextBoxSinglePlayer.Text);

                //TO-DO add the properties to display the difficulty selection settings



                //This if for testing remove when finish
                MessageBox.Show("It work your username is: " + player1.getUsername());

            }
            else
            {
                MessageBox.Show("The username and/or password you input do not match our records./nPlease try again.");
            }

        }

        //Login player 1 multiplayer
        private void loginMultiPlayer1(object sender, MouseButtonEventArgs e)
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
                MessageBox.Show("The username and/or password you input do not match our records./nPlease try again.");
            }
        }

        //Back button registration
        private void backButtonReg(object sender, MouseButtonEventArgs e)
        {
            backRegistration();
        }
        

        /*
         * 
         * 
         * Functions to be use int he program by the buttons actions
         * 
         * 
         * 
         * */

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
                loginSinglePlayer.Visibility = Visibility.Visible;

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

            return reader.Read();//Returns true if the username and password are found otherwise it returns false

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

            return reader.Read();//Returns true if username is found in the database false otherwise
           

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

          
            conn.Close();
        }

        //Action when selecting text boxes and password boses
        private void passwordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox selectedBox = sender as PasswordBox;
            selectedBox.SelectAll();
        }

        private void usernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox selectedBox = sender as TextBox;
            selectedBox.SelectAll();
        }

        private void usernameTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            TextBox selectedBox = sender as TextBox;
            selectedBox.SelectAll();
        }

        private void passwordTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            PasswordBox selectedBox = sender as PasswordBox;
            selectedBox.SelectAll();
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
