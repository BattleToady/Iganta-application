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
    /// Interaction logic for ideasBucketPage.xaml
    /// </summary>
    public partial class ideasBucketPage : Page
    {
        ApplicationContext db;
        List<string> Tags;
        List<Idea> Ideas;
        string selectedTag = null;
        int userId;
        public ideasBucketPage()
        {
            InitializeComponent();
            userId = (int)Application.Current.Properties["user"];
            Tags = new List<string>();
            db = new ApplicationContext();
            Application.Current.Properties["db"] = db;
            Ideas = db.Ideas.Where(i => i.UserId == userId).ToList();
            foreach (Idea idea in Ideas)
                if (!Tags.Contains(idea.Tag))
                    Tags.Add(idea.Tag);

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
            IdeasGrid.Children.Clear();

            int counter = 0;

            foreach (Idea idea in Ideas)
            {
                if (idea.Tag == selectedTag)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(50);
                    IdeasGrid.RowDefinitions.Add(row);

                    TextBox achName = new TextBox();
                    achName.FontSize = 10;
                    achName.Text = idea.Name;
                    achName.VerticalContentAlignment = VerticalAlignment.Center;
                    achName.VerticalAlignment = VerticalAlignment.Center;
                    IdeasGrid.Children.Add(achName);
                    achName.Margin = new Thickness(0, 0, 5, 0);
                    achName.Tag = idea;
                    achName.LostFocus += NameBoxValueChanged;
                    Grid.SetColumn(achName, 0);
                    Grid.SetRow(achName, counter);

                    TextBox achDescription = new TextBox();
                    achDescription.FontSize = 10;
                    achDescription.Text = idea.Description;
                    IdeasGrid.Children.Add(achDescription);
                    achDescription.Tag = idea;
                    achDescription.LostFocus += DescriptionBoxValueChanged;
                    achDescription.VerticalContentAlignment = VerticalAlignment.Center;
                    achDescription.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(achDescription, 1);
                    Grid.SetRow(achDescription, counter);

                    Button removeButton = new Button();
                    removeButton.FontSize = 10;
                    removeButton.Content = "remove";
                    removeButton.Tag = idea;
                    removeButton.Click += RemoveButtonClick;
                    IdeasGrid.Children.Add(removeButton);
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
            Idea idea = (Idea)((TextBox)sender).Tag;

            db.Ideas.Single(i => i.Id == idea.Id).Name = ((TextBox)sender).Text;
            db.SaveChanges();
        }

        void RemoveButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            { }
            else
            {
                Idea idea = (Idea)((Button)sender).Tag;
                db.Ideas.Remove(idea);
                Ideas.Remove(idea);
                RefreshAchievements();
                db.SaveChanges();
            }

        }

        void DescriptionBoxValueChanged(object sender, EventArgs e)
        {
            Application.Current.Properties["wereChanges"] = true;

            Idea idea = (Idea)((TextBox)sender).Tag;

            db.Ideas.Single(i => i.Id == idea.Id).Description = ((TextBox)sender).Text;
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
            while (true)
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
            int counter = 1;
            List<string> ideaNames = new List<string>();
            foreach (Idea idea in Ideas)
                if (idea.Tag == selectedTag)
                    ideaNames.Add(idea.Name);
            while (true)
            {
                if (!ideaNames.Contains("NewIdea_" + counter.ToString()))
                {
                    Idea idea = new Idea(userId, selectedTag, "NewIdea_" + counter.ToString());
                    Ideas.Add(idea);
                    db.Ideas.Add(idea);
                    break;
                }
                else
                    counter++;
            }
            db.SaveChanges();
            RefreshAchievements();
        }

        private void TagNameBox_TextChanged(object sender, TextChangedEventArgs e)
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
                List<Idea> ideaList = db.Ideas.Where(i => i.UserId == userId && i.Tag == selectedTag).ToList();
                foreach (Idea idea in ideaList)
                    idea.Tag = TagNameBox.Text;
                selectedTag = TagNameBox.Text;
                db.SaveChanges();
                RefillTagList();
                RefreshAchievements();
            }

        }
    }
}
