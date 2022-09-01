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
    /// Interaction logic for ProjectArchivePage.xaml
    /// </summary>
    public partial class ProjectArchivePage : Page
    {
        ApplicationContext db;
        Branch selectedBranch;
        public ProjectArchivePage()
        {
            InitializeComponent();
        }

        void RefreshBranchGrid()
        {
            branchesGrid.Children.Clear();
            branchesGrid.ColumnDefinitions.Clear();
            branchesGrid.RowDefinitions.Clear();
            using (db = new ApplicationContext())
            {
                int userId = Convert.ToInt32(Application.Current.Properties["user"]);
                int count = db.Branches.Where(b => b.UserId == userId).Count();
                double UIWidth = branchesGrid.ActualWidth;
                int countInRow = Convert.ToInt32(Math.Floor(UIWidth / 150));
                double buttonWidth = UIWidth / countInRow;

                while (count > 0)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(buttonWidth);
                    branchesGrid.RowDefinitions.Add(row);
                    count -= countInRow;
                }
                for (int i = 0; i < countInRow; i++)
                {
                    ColumnDefinition col = new ColumnDefinition();
                    col.Width = new GridLength(buttonWidth);
                    branchesGrid.ColumnDefinitions.Add(col);
                }
                List<Branch> branches = db.Branches.Where(b => b.UserId == userId).ToList();
                int counter_x = 0;
                int counter_y = 0;
                foreach (Branch branch in branches)
                {
                    if (branch.Closed == 1)
                    {
                        Button but = new Button();
                        but.Width = buttonWidth - 4;
                        but.Height = buttonWidth - 4;
                        but.Margin = new Thickness(2, 2, 2, 2);
                        but.Background = Brushes.Transparent;


                        StackPanel panel = new StackPanel();
                        but.Content = panel;



                        Label labelName = new Label();
                        labelName.Content = branch.Name;
                        labelName.HorizontalContentAlignment = HorizontalAlignment.Center;
                        labelName.FontSize = 16;
                        labelName.FontWeight = FontWeights.Bold;

                        Border borderName = new Border();
                        borderName.BorderThickness = new Thickness(0, 0, 0, 1.75);
                        borderName.BorderBrush = Brushes.DarkGray;
                        borderName.Child = labelName;

                        panel.Children.Add(borderName);

                        Label labelMark = new Label();
                        labelMark.Content = branch.Mark;
                        labelMark.HorizontalContentAlignment = HorizontalAlignment.Center;
                        labelMark.FontSize = 12;

                        Border borderMark = new Border();
                        borderMark.BorderThickness = new Thickness(0, 0, 0, 0.5);
                        borderMark.BorderBrush = Brushes.DarkGray;
                        borderMark.Child = labelMark;
                        panel.Children.Add(borderMark);

                        Label labelMission = new Label();
                        labelMission.Content = branch.Mission;
                        labelMission.HorizontalContentAlignment = HorizontalAlignment.Center;
                        labelMission.FontSize = 12;

                        Border borderMission = new Border();
                        borderMission.BorderThickness = new Thickness(0, 0, 0, 0.5);
                        borderMission.BorderBrush = Brushes.DarkGray;
                        borderMission.Child = labelMission;
                        panel.Children.Add(borderMission);

                        Label labelProgress = new Label();
                        labelProgress.Content = branch.Progress.ToString() + '%';
                        labelProgress.HorizontalContentAlignment = HorizontalAlignment.Center;
                        labelProgress.FontSize = 12;
                        panel.Children.Add(labelProgress);

                        branchesGrid.Children.Add(but);
                        but.Tag = branch.Id;
                        but.Click += branchButtonClick;
                        Grid.SetColumn(but, counter_x);
                        Grid.SetRow(but, counter_y);
                        counter_x++;
                        if (counter_x >= countInRow)
                        {
                            counter_x = 0;
                            counter_y++;
                        }
                    }
                }
            }
        }

        void branchButtonClick(object sender, RoutedEventArgs e)
        {
            int branchId = (int)((Button)sender).Tag;
            using (db = new ApplicationContext())
            {
                selectedBranch = db.Branches.Single(b => b.Id == branchId);
            }
            projectNameLabel.Content = selectedBranch.Name;
            deleteButton.Visibility = Visibility.Visible;
            reopenButton.Visibility = Visibility.Visible;
        }

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshBranchGrid();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this project?", "Question", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                using(db = new ApplicationContext())
                {
                    Branch foundBranch = db.Branches.Single(b => b.Id == selectedBranch.Id);
                    db.Branches.Remove(foundBranch);
                    db.SaveChanges();
                }
                RefreshBranchGrid();
                projectNameLabel.Content = "";
                deleteButton.Visibility = Visibility.Hidden;
                reopenButton.Visibility = Visibility.Hidden;
            }
            else
            if(result == MessageBoxResult.No)
            {
            }
        }

        private void reopenButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to reopen this project?", "Question", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (db = new ApplicationContext())
                {
                    db.Branches.Single(b => b.Id == selectedBranch.Id).Closed = 0;
                    db.SaveChanges();
                }
                RefreshBranchGrid();
                projectNameLabel.Content = "";
                deleteButton.Visibility = Visibility.Hidden;
                reopenButton.Visibility = Visibility.Hidden;
            }
            else
            if (result == MessageBoxResult.No)
            {
            }
        }
    }
}
