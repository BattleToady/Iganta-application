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
    /// Interaction logic for DoneListPage.xaml
    /// </summary>
    public partial class DoneListPage : Page
    {
        ApplicationContext db;
        List<string> Tags;
        List<Achievement> Achievements;
        string selectedTag = null;
        int userId;
        public DoneListPage()
        {
            InitializeComponent();
            userId = (int)Application.Current.Properties["user"];
            Tags = new List<string>();
            db = new ApplicationContext();
            Application.Current.Properties["db"] = db;
            Achievements = db.Achievements.Where(ach => ach.UserId == userId).ToList();
            foreach(Achievement ach in Achievements)
                if(!Tags.Contains(ach.Tag))
                    Tags.Add(ach.Tag);

            fillTagList();
        }

        void fillTagList()
        {
            foreach (string tag in Tags)
            {
                ListViewItem item = new ListViewItem();
                item.Content = tag;
                item.Tag = tag;
                item.MouseLeftButtonUp += listItemClick;
                TagsList.Items.Add(item);
            }
        }

        void RefillTagList()
        {
            TagsList.Items.Clear();
            fillTagList();
        }

        void RefreshAchievements()
        {
            AchievemntsGrid.Children.Clear();

            int counter = 0;

            foreach (Achievement ach in Achievements)
            {
                if(ach.Tag == selectedTag)
                { 
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(50);
                    AchievemntsGrid.RowDefinitions.Add(row);

                    TextBox achName = new TextBox();
                    achName.FontSize = 10;
                    achName.Text = ach.Name;
                    achName.VerticalContentAlignment = VerticalAlignment.Center;
                    achName.VerticalAlignment = VerticalAlignment.Center;
                    AchievemntsGrid.Children.Add(achName);
                    achName.Margin = new Thickness(0, 0, 5, 0);
                    achName.Tag = ach;
                    achName.LostFocus += NameBoxValueChanged;
                    Grid.SetColumn(achName, 0);
                    Grid.SetRow(achName, counter);

                    TextBox achDescription = new TextBox();
                    achDescription.FontSize = 10;
                    achDescription.Text = ach.Description;
                    AchievemntsGrid.Children.Add(achDescription);
                    achDescription.Tag = ach;
                    achDescription.LostFocus += DescriptionBoxValueChanged;
                    achDescription.VerticalAlignment = VerticalAlignment.Center;
                    achDescription.VerticalContentAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(achDescription, 1);
                    Grid.SetRow(achDescription, counter);

                    Button removeButton = new Button();
                    removeButton.FontSize = 10;
                    removeButton.Content = "remove";
                    removeButton.Tag = ach;
                    removeButton.Click += RemoveButtonClick;
                    AchievemntsGrid.Children.Add(removeButton);
                    removeButton.Margin = new Thickness(4, 0, 2, 0);
                    removeButton.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(removeButton, 3);
                    Grid.SetRow(removeButton, counter);

                    counter++;
                }
            }
        }

        void NameBoxValueChanged(object sender, EventArgs e)
        {
            Application.Current.Properties["wereChanges"] = true;

            Achievement ach = (Achievement)((TextBox)sender).Tag;

            db.Achievements.Single(a => a.Id == ach.Id).Name = ((TextBox)sender).Text;
            db.SaveChanges();
        }

        void RemoveButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            { }
            else
            {
                Achievement ach = (Achievement)((Button)sender).Tag;
                db.Achievements.Remove(ach);
                Achievements.Remove(ach);
                RefreshAchievements();
                db.SaveChanges();
            }
            
        }

        void DescriptionBoxValueChanged(object sender, EventArgs e)
        {
            Application.Current.Properties["wereChanges"] = true;

            Achievement ach = (Achievement)((TextBox)sender).Tag;

            db.Achievements.Single(a => a.Id == ach.Id).Description = ((TextBox)sender).Text;
            db.SaveChanges();
        }

        void listItemClick(object sender, MouseButtonEventArgs e)
        {
            selectedTag = (string)((ListViewItem)sender).Tag;
            TagNameBox.Text = selectedTag;
            RefreshAchievements();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int counter = 1;
            while(true)
            {
                if (!Tags.Contains("NewTag_" + counter.ToString()))
                {
                    Tags.Add("NewTag_" + counter.ToString());
                    break;
                }
                else
                    counter++;
            }
            RefillTagList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(selectedTag != null)
            { 
                int counter = 1;
                List<string> achNames = new List<string>();
                foreach (Achievement achievement in Achievements)
                    if(achievement.Tag == selectedTag)
                        achNames.Add(achievement.Name);
                while (true)
                {
                    if (!achNames.Contains("NewAchievement_" + counter.ToString()))
                    {
                        Achievement ach = new Achievement(userId, selectedTag, "NewAchievement_" + counter.ToString());
                        Achievements.Add(ach);
                        db.Achievements.Add(ach);
                        break;
                    }
                    else
                        counter++;
                }
                Application.Current.Properties["wereChanges"] = true;
                db.SaveChanges();
                RefreshAchievements();
            }
        }

        private void TagNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (selectedTag != null)
            {
                foreach (string tag in Tags)
                    if (tag == selectedTag)
                    {
                        int index = Tags.IndexOf(tag);
                        Tags.Remove(tag);
                        Tags.Insert(index, TagNameBox.Text);
                        break;
                    }
                List<Achievement> achList = db.Achievements.Where(a => a.UserId == userId && a.Tag == selectedTag).ToList();
                foreach (Achievement ach in achList)
                    ach.Tag = TagNameBox.Text;
                selectedTag = TagNameBox.Text;
                db.SaveChanges();
                RefillTagList();
                RefreshAchievements();
            }
        }
    }
}
