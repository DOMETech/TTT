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
using Finisar.SQLite;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool sound;

        public MainWindow()
        {
            InitializeComponent();
            mainMenuPanel.Visibility = Visibility.Hidden;
            loginSinglePlayer.Visibility = Visibility.Hidden;
            registerPanel.Visibility = Visibility.Hidden;
            sound = false;
            //soundElement.Play();




        }

        private void highlightLabel(object sender, MouseEventArgs e)
        {
            Label selectedLabel = sender as Label;
            selectedLabel.Foreground = new SolidColorBrush(Colors.Yellow);
            Mouse.OverrideCursor = Cursors.Hand;
            selectedLabel.FontSize += 10;
            selectedLabel.FontWeight = FontWeights.SemiBold;

        }



        private void unhighlightLabel(object sender, MouseEventArgs e)
        {
            Label selectedLabel = sender as Label;
            selectedLabel.Foreground = new SolidColorBrush(Colors.White);
            Mouse.OverrideCursor = Cursors.Arrow;
            selectedLabel.FontSize -= 10;
            selectedLabel.FontWeight = FontWeights.Regular;

        }

        private void clickStartaGame(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Visible;
            startScreenMenu.Visibility = Visibility.Hidden;

        }

        private void quitProgram(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

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

        private void soundElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            soundElement.Position = TimeSpan.Zero;
            soundElement.Play();
        }


        private void singlePayerButton(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Hidden;
            loginSinglePlayer.Visibility = Visibility.Visible;
        }


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

        private void backToMainMenu(object sender, MouseButtonEventArgs e)
        {
            loginSinglePlayer.Visibility = Visibility.Hidden;
            registerPanel.Visibility = Visibility.Hidden;
            mainMenuPanel.Visibility = Visibility.Visible;
        }

        private void registration(object sender, MouseButtonEventArgs e)
        {
            mainMenuPanel.Visibility = Visibility.Hidden;
            loginSinglePlayer.Visibility = Visibility.Hidden;
            registerPanel.Visibility = Visibility.Visible;
        }

        private void enterButton_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (passRegPassBox.Password == confirmPassRegPassBox.Password)
            {
                // We use these three SQLite objects:
                SQLiteConnection sqlite_conn;
                SQLiteCommand sqlite_cmd;

                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source=TTTdatabase.db;Version=3;New=True;Compress=True;");

                // open the connection:
                sqlite_conn.Open();

                // create a new SQL command:
                

                string usernameData = userRegTextBox.Text;
                string passwordData = passRegPassBox.Password;
                string emailData = emailTextBox.Text;

                var usernameParam = new SQLiteParameter("@usernameParam", System.Data.DbType.String) { Value = usernameData };
                var passParam = new SQLiteParameter("@passParam", System.Data.DbType.String) { Value = passwordData};
                var emailParam = new SQLiteParameter("@emailParam", System.Data.DbType.String) { Value = emailData };

                sqlite_cmd = new SQLiteCommand("INSERT INTO Users (username, password, email) VALUES (@usernameParam, @passParam, @emailParam);");
                sqlite_cmd.Parameters.Add(usernameParam);
                sqlite_cmd.Parameters.Add(passParam);
                sqlite_cmd.Parameters.Add(emailParam);

                try
                {
                    
                    sqlite_cmd.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString());
                }
                sqlite_conn.Close();

            }
            
        }

    }
}
