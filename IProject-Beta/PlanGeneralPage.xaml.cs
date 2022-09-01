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
    /// Interaction logic for PlanGeneralPage.xaml
    /// </summary>
    public partial class PlanGeneralPage : Page
    {
        ApplicationContext db;
        string selectedItem;
        int branchId;
        int userId;
        int selectedPhaseId;
        public PlanGeneralPage()
        {
            InitializeComponent();
            
            branchId = (int)Application.Current.Properties["branch"];
            userId = (int)Application.Current.Properties["user"];

            RefreshUI();

            for (int i = 0; i <= 100; i++)
                progressValueBox.Items.Add(i.ToString());
            progressValueBox.SelectedValue = "0";
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            NewPhaseBox.Visibility = Visibility.Visible;
            AddPhaseButton.Visibility = Visibility.Visible;

            progressValueBox.Visibility = Visibility.Hidden;
            DetailsButton.Visibility = Visibility.Hidden;
            PhaseNameBox.Visibility = Visibility.Hidden;
            closeButton.Visibility = Visibility.Hidden;
            removeButton.Visibility = Visibility.Hidden;

            Application.Current.Properties["planPhaseId"] = selectedPhaseId;
            Application.Current.Properties["thirdPage"] = true;
            Application.Current.Properties["preLastPage"] = this;
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            PageFrame.Content = new PlanPhaseDetailsPage();
            selectedPhaseId = -1;
        }

        void RefreshUI()
        {
            PhasesPanel.Children.Clear();

            using (db = new ApplicationContext())
            {
                List<PlanPhase> phases = db.PlanPhases.Where(p => p.UserId == userId & p.BranchId == branchId).ToList();

                int counter = 1;
                while (counter < phases.Count + 1)
                    foreach (PlanPhase phase in phases)
                    {
                        List<PlanTask> tasks = db.PlanTasks.Where(t => t.PlanPhaseId == phase.Id).ToList();
                        int doneTasks = 0;
                        int countOfTasks = 0;

                        foreach(PlanTask task in tasks)
                        {
                            if (task.Done == 1)
                                doneTasks++;
                            countOfTasks++;
                        }

                        if (phase.Position == counter)
                        {
                            Button but = new Button();
                            but.Margin = new Thickness(0, 0, 0, 5);
                            but.Content = "Phase " + phase.Position.ToString() + ": " + phase.Name + " " + doneTasks.ToString() + '/' + countOfTasks.ToString() + "        % of work: " + phase.Progress;
                            but.Tag = phase;
                            PhasesPanel.Children.Add(but);
                            but.Click += PhaseButton_Click;

                            counter++;
                        }
                    }
            }
        }

        private void ListViewItem_DragEnter(object sender, DragEventArgs e)
        {
            selectedItem = "Test value";
        }

        private void ScrollViewer_DragOver(object sender, DragEventArgs e)
        {
            MessageBox.Show(selectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                List<PlanPhase> phases = db.PlanPhases.Where(p => p.UserId == userId & p.BranchId == branchId).ToList();

                db.PlanPhases.Add(new PlanPhase(userId, branchId, phases.Count + 1, NewPhaseBox.Text));

                db.SaveChanges();

                RefreshUI();
            }
        }

        private void PhaseButton_Click(object sender, EventArgs e)
        {
            PlanPhase phase = (PlanPhase)(((Button)sender).Tag);
            if(selectedPhaseId != phase.Id)
            {
                selectedPhaseId = phase.Id;

                NewPhaseBox.Visibility = Visibility.Hidden;
                AddPhaseButton.Visibility = Visibility.Hidden;

                removeButton.Visibility = Visibility.Visible;
                progressValueBox.Visibility = Visibility.Visible;
                DetailsButton.Visibility = Visibility.Visible;
                closeButton.Visibility = Visibility.Visible;
                PhaseNameBox.Visibility = Visibility.Visible;
                PhaseNameBox.Text = phase.Name;
                progressValueBox.SelectedValue = phase.Progress.ToString();
            }
            else
            {
                selectedPhaseId = -1;

                NewPhaseBox.Visibility = Visibility.Visible;
                AddPhaseButton.Visibility = Visibility.Visible;

                progressValueBox.Visibility = Visibility.Hidden;
                DetailsButton.Visibility = Visibility.Hidden;
                PhaseNameBox.Visibility = Visibility.Hidden;
                closeButton.Visibility = Visibility.Hidden;
                removeButton.Visibility = Visibility.Hidden;
            }
        }

        private void progressValueBox_LostFocus(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                PlanPhase phase = db.PlanPhases.Single(p => p.Id == selectedPhaseId);

                phase.Progress = Convert.ToInt32(progressValueBox.SelectedValue);

                db.SaveChanges();

                RefreshUI();
            }
            using (db = new ApplicationContext())
            {

                float percent = 0;

                int userId = (int)Application.Current.Properties["user"];
                int branchId = (int)Application.Current.Properties["branch"];
                Branch branch = db.Branches.Single(b => b.Id == branchId);

                List<PlanPhase> phases = db.PlanPhases.Where(phase => phase.UserId == userId & phase.BranchId == branch.Id).ToList();
                foreach (PlanPhase phase in phases)
                {
                    List<PlanTask> tasks = db.PlanTasks.Where(t => t.PlanPhaseId == phase.Id).ToList();
                    int doneCounter = 0;
                    int generalCounter = 0;
                    foreach (PlanTask task in tasks)
                    {
                        if (task.Done == 1)
                            doneCounter++;
                        generalCounter++;
                    }
                    if ((doneCounter > 0) & (doneCounter == generalCounter))
                    {
                        percent += phase.Progress;
                    }
                }
                Branch bran = db.Branches.Single(b => b.Id == branch.Id);
                bran.Progress = percent;
                db.SaveChanges();

            }
        }

        private void PhaseNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                PlanPhase phase = db.PlanPhases.Single(p => p.Id == selectedPhaseId);

                phase.Name = PhaseNameBox.Text;

                db.SaveChanges();

                RefreshUI();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            { }
            else
            {
                using (db = new ApplicationContext())
                {

                    db.PlanPhases.Remove(db.PlanPhases.Single(p => p.Id == selectedPhaseId));
                    foreach(PlanTask task in db.PlanTasks.Where(p => p.PlanPhaseId == selectedPhaseId).ToList())
                        db.PlanTasks.Remove(task);
                    db.SaveChanges();
                }
            }
            selectedPhaseId = -1;
            NewPhaseBox.Visibility = Visibility.Visible;
            AddPhaseButton.Visibility = Visibility.Visible;

            progressValueBox.Visibility = Visibility.Hidden;
            DetailsButton.Visibility = Visibility.Hidden;
            PhaseNameBox.Visibility = Visibility.Hidden;
            closeButton.Visibility = Visibility.Hidden;
            removeButton.Visibility = Visibility.Hidden;
            RefreshUI();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            selectedPhaseId = -1;
            NewPhaseBox.Visibility = Visibility.Visible;
            AddPhaseButton.Visibility = Visibility.Visible;

            progressValueBox.Visibility = Visibility.Hidden;
            DetailsButton.Visibility = Visibility.Hidden;
            PhaseNameBox.Visibility = Visibility.Hidden;
            closeButton.Visibility = Visibility.Hidden;
            removeButton.Visibility = Visibility.Hidden;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUI();
        }
    }
}
