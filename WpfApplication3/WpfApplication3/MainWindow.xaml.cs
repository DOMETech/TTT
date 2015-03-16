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
            sound = true;
            soundElement.Play();

                     
            

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
                catch(Exception err)
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
            mainMenuPanel.Visibility = Visibility.Visible;
        }

        

        

   

        
        


    }
}
