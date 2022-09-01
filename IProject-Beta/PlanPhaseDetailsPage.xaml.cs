
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
    /// Interaction logic for PlanPhaseDetailsPage.xaml
    /// </summary>
    public partial class PlanPhaseDetailsPage : Page
    {
        ApplicationContext db;
        int PhaseId;
        PlanTask selectedTask;
        public PlanPhaseDetailsPage()
        {
            InitializeComponent();

            PhaseId = (int)Application.Current.Properties["planPhaseId"];

            

            RefreshTaskGrid();
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                List<PlanTask> tasks = db.PlanTasks.Where(t => t.PlanPhaseId == PhaseId).ToList();
                int position;
                if (tasks.Count() > 0)
                    position = tasks.Count() + 1;
                else
                    position = 1;
                db.PlanTasks.Add(new PlanTask(PhaseId, newTaskNameBox.Text, DateTime.Today.AddDays(1).ToShortDateString(), position));
                newTaskNameBox.Text = "New Task";

                db.SaveChanges();
                RefreshTaskGrid();
            }
        }

        void RefreshTaskGrid()
        {
            TasksGrid.Children.Clear();
            TasksGrid.RowDefinitions.Clear();

            using(db = new ApplicationContext())
            {
                List<PlanTask> tasks = db.PlanTasks.Where(t => t.PlanPhaseId == PhaseId).ToList();

                int counter = 1;
                while(counter < tasks.Count + 1)
                    foreach(PlanTask task in tasks)
                    {
                        if(task.Position == counter)
                        {
                            RowDefinition row = new RowDefinition();
                            row.Height = new GridLength(30);
                            TasksGrid.RowDefinitions.Add(row);

                            CheckBox doneBox = new CheckBox();
                            if (task.Done == 1)
                                doneBox.IsChecked = true;
                            else
                                doneBox.IsChecked = false;
                            doneBox.VerticalAlignment = VerticalAlignment.Center;
                            doneBox.HorizontalAlignment = HorizontalAlignment.Center;
                            doneBox.Tag = task.Id;
                            doneBox.Checked += checkDoneBox;
                            doneBox.Unchecked += checkDoneBox;

                            Button but = new Button();
                            but.Foreground = Brushes.Black;
                            but.BorderBrush = Brushes.Transparent;
                            but.HorizontalContentAlignment = HorizontalAlignment.Left;
                            but.VerticalContentAlignment = VerticalAlignment.Center;
                            but.Content = task.Task;
                            but.Tag = task;
                            but.Background = Brushes.Transparent;
                            but.Margin = new Thickness(0, 0, 0, 5);
                            but.Click += TaskButtonClick;

                            Label deadline = new Label();
                            deadline.Content = task.Deadline;
                            deadline.HorizontalAlignment = HorizontalAlignment.Left;
                            deadline.VerticalAlignment = VerticalAlignment.Center;
                            if (DateTime.Parse(task.Deadline) < DateTime.Now)
                            {
                                deadline.Foreground = Brushes.OrangeRed;
                                deadline.FontWeight = FontWeights.Bold;
                            }
                            else
                                deadline.Foreground = Brushes.Black;

                            TasksGrid.Children.Add(doneBox);
                            TasksGrid.Children.Add(but);
                            TasksGrid.Children.Add(deadline);
                            Grid.SetColumn(doneBox, 0);
                            Grid.SetColumn(but, 1);
                            Grid.SetColumn(deadline, 2);
                            Grid.SetRow(doneBox, counter - 1);
                            Grid.SetRow(but, counter - 1);
                            Grid.SetRow(deadline, counter - 1);
                            counter++;
                        }
                    }
            }
        }

        void checkDoneBox(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                CheckBox box = (CheckBox)sender;
                int id = (int)((CheckBox)sender).Tag;
                int done = 0;
                if ((bool)box.IsChecked)
                    done = 1;
                db.PlanTasks.Single(t => t.Id == id).Done = done;
                db.SaveChanges();
            }
            
        }
        void TaskButtonClick(object sender, RoutedEventArgs e)
        {
            selectedTask = (PlanTask)(((Button)sender).Tag);

            changeNameBox.Text = selectedTask.Task;
            DeadlinePicker.SelectedDate = DateTime.Parse(selectedTask.Deadline);

            changeNameBox.Visibility = Visibility.Visible;
            DeadlinePicker.Visibility = Visibility.Visible;
            RemoveButton.Visibility = Visibility.Visible;
            movePanel.Visibility = Visibility.Visible;
            closeButton.Visibility = Visibility.Visible;

            newTaskNameBox.Visibility = Visibility.Hidden;
            addTaskButton.Visibility = Visibility.Hidden;
        }

        void changeNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                db.PlanTasks.Single(t => t.Id == selectedTask.Id).Task = changeNameBox.Text;

                db.SaveChanges();
            }
            RefreshTaskGrid();
        }

        private void DeadlinePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                db.PlanTasks.Single(t => t.Id == selectedTask.Id).Deadline = ((DateTime)DeadlinePicker.SelectedDate).ToShortDateString();

                db.SaveChanges();
            }
            RefreshTaskGrid();
        }



        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            { }
            else
            {
                MessageBox.Show(selectedTask.Id.ToString());
                using(db = new ApplicationContext())
                {
                    PlanTask task = db.PlanTasks.Single(t => t.Id == selectedTask.Id);
                    db.PlanTasks.Remove(task);
                    db.SaveChanges();

                    selectedTask = null;

                    changeNameBox.Visibility = Visibility.Hidden;
                    DeadlinePicker.Visibility = Visibility.Hidden;
                    RemoveButton.Visibility = Visibility.Hidden;
                    movePanel.Visibility = Visibility.Hidden;
                    closeButton.Visibility = Visibility.Hidden;

                    newTaskNameBox.Visibility = Visibility.Visible;
                    addTaskButton.Visibility = Visibility.Visible;

                    RefreshTaskGrid();
                }
            }
        }

        private void moveDownButton_Click(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                List<PlanTask> tasks = db.PlanTasks.Where(t => t.PlanPhaseId == PhaseId).ToList();
                if(selectedTask.Position < tasks.Count)
                {
                    db.PlanTasks.Single(t => (t.PlanPhaseId == selectedTask.PlanPhaseId) & (t.Position == selectedTask.Position + 1)).Position -= 1;
                    db.PlanTasks.Single(t => t.Id == selectedTask.Id).Position += 1;
                    

                    db.SaveChanges();
                    RefreshTaskGrid();
                    selectedTask.Position += 1;
                }
            }
        }

        private void moveUpButton_Click(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                List<PlanTask> tasks = db.PlanTasks.Where(t => t.PlanPhaseId == PhaseId).ToList();
                if (selectedTask.Position > 1)
                {
                    db.PlanTasks.Single(t => (t.PlanPhaseId == selectedTask.PlanPhaseId) & (t.Position == selectedTask.Position - 1)).Position += 1;
                    db.PlanTasks.Single(t => t.Id == selectedTask.Id).Position -= 1;

                    db.SaveChanges();
                    RefreshTaskGrid();
                    selectedTask.Position -= 1;
                }
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            selectedTask = null;

            changeNameBox.Visibility = Visibility.Hidden;
            DeadlinePicker.Visibility = Visibility.Hidden;
            RemoveButton.Visibility = Visibility.Hidden;
            movePanel.Visibility = Visibility.Hidden;
            closeButton.Visibility = Visibility.Hidden;

            newTaskNameBox.Visibility = Visibility.Visible;
            addTaskButton.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshTaskGrid();
        }
    }
}
