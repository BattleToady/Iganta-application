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

namespace IProject_Beta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Properties["logIn"] = false;
            Application.Current.Properties["inMainPage"] = false;
            PageFrame.Content = new Authorization();
            Application.Current.Properties["Window"] = this;
            Application.Current.Properties["PageFrame"] = PageFrame;
            Application.Current.Properties["secondPage"] = false;
            Application.Current.Properties["thirdPage"] = false;
            Application.Current.Properties["lastPage"] = null;
            Application.Current.Properties["preLastPage"] = null;
            Application.Current.Properties["db"] = null;
            Application.Current.Properties["SettingsButton"] = SettingsButton;
            Application.Current.Properties["pageNameLabel"] = pageNameLabel;


        }


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if((bool)Application.Current.Properties["logIn"])
            if ((bool)Application.Current.Properties["inMainPage"])
            {
                BackButton.Visibility = Visibility.Hidden;
                LogOutButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Visible;
                LogOutButton.Visibility = Visibility.Hidden;
            }
            else
            {
                BackButton.Visibility = Visibility.Hidden;
                LogOutButton.Visibility = Visibility.Hidden;
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["logIn"] = false;
            Application.Current.Properties["inMainPage"] = false;
            ((Button)Application.Current.Properties["SettingsButton"]).Visibility = Visibility.Hidden;
            PageFrame.Content = new Authorization();
            pageNameLabel.Content = "";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            
            if ((bool)Application.Current.Properties["thirdPage"])
            {
                PageFrame.Content = (Page)Application.Current.Properties["preLastPage"];
                Application.Current.Properties["thirdPage"] = false;
            }
            else
            { 
                if ((bool)Application.Current.Properties["secondPage"])
                {
                    Application.Current.Properties["inMainPage"] = false;
                    Application.Current.Properties["secondPage"] = false;
                    PageFrame.Content = (Page)Application.Current.Properties["lastPage"];
                }
                else
                {
                    Application.Current.Properties["secondPage"] = false;
                    Application.Current.Properties["branch"] = null;
                    Application.Current.Properties["inMainPage"] = true;
                    PageFrame.Content = new MainPage();

                }
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Application.Current.Properties["logIn"])
            {
                PageFrame.Content = new SettingsPage();
                Application.Current.Properties["inMainPage"] = false;
            }
        }
    }
}
