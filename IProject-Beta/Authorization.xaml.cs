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
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        ApplicationContext db;
        List<PasswordBox> passBoxes = new List<PasswordBox>();
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                int userId = Convert.ToInt32(((Button)sender).Tag);
                User user = db.Users.Single(u => u.Id == userId);
                if(user != null)
                {
                    if(user.Password != null)
                    {
                        foreach(PasswordBox pb in passBoxes)
                        {
                            if(Convert.ToInt32(pb.Tag) == userId)
                            {
                                if (pb.Password == user.Password)
                                {
                                    Application.Current.Properties["user"] = user.Id;
                                    ((Button)Application.Current.Properties["SettingsButton"]).Visibility = Visibility.Visible;
                                    Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
                                    Application.Current.Properties["logIn"] = true;
                                    Application.Current.Properties["inMainPage"] = true;
                                    PageFrame.Content = new MainPage();
                                }
                                else
                                    MessageBox.Show("Wrong password");
                            }
                        }
                    }
                    else
                    {
                        Application.Current.Properties["user"] = user.Id;
                        ((Button)Application.Current.Properties["SettingsButton"]).Visibility = Visibility.Visible;
                        Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
                        Application.Current.Properties["logIn"] = true;
                        Application.Current.Properties["inMainPage"] = true;
                        PageFrame.Content = new MainPage();
                    }
                }
            }
            
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsersView();
        }

        void RefreshUsersView()
        {
            usersGrid.Children.Clear();
            passBoxes.Clear();

            int counter = 0;

            using (db = new ApplicationContext())
            {
                List<User> users = db.Users.ToList();
                foreach (User user in users)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(50);
                    usersGrid.RowDefinitions.Add(row);

                    Border border = new Border();
                    usersGrid.Children.Add(border);
                    border.BorderThickness = new Thickness(0,0,0,0.5f);
                    border.BorderBrush = Brushes.DarkGray;
                    Grid.SetColumn(border, 0);
                    Grid.SetRow(border, counter);
                    Grid.SetColumnSpan(border, 4);

                    Label userName = new Label();
                    userName.Content = user.Name;
                    usersGrid.Children.Add(userName);
                    Grid.SetColumn(userName, 0);
                    Grid.SetRow(userName, counter);
                    userName.VerticalAlignment = VerticalAlignment.Center;
                    userName.VerticalContentAlignment = VerticalAlignment.Center;

                    if (user.Password != null)
                    {
                        PasswordBox passBox = new PasswordBox();
                        passBox.VerticalAlignment = VerticalAlignment.Center;
                        passBox.VerticalContentAlignment = VerticalAlignment.Center;
                        usersGrid.Children.Add(passBox);
                        Grid.SetColumn(passBox, 1);
                        Grid.SetRow(passBox, counter);
                        passBox.Tag = user.Id;
                        passBoxes.Add(passBox);
                    }

                    Button but = new Button();
                    but.Content = "Log In";
                    usersGrid.Children.Add(but);
                    Grid.SetColumn(but, 3);
                    Grid.SetRow(but, counter);
                    but.Tag = user.Id;
                    but.Click += Button_Click;

                    counter++;
                }
            }
        }

        private void AddUser_Button_Click(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                if (newUserTextBox.Text.Length > 0)
                    db.Users.Add(new User(newUserTextBox.Text));
                else
                    MessageBox.Show("Input user name");
                db.SaveChanges();
                RefreshUsersView();
            }
        }
    }
}
