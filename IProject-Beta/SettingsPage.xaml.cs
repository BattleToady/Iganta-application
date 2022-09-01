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
    public partial class SettingsPage : Page
    {
        int userId;
        ApplicationContext db;
        public SettingsPage()
        {
            InitializeComponent();
            userId = (int)Application.Current.Properties["user"];
            using (db = new ApplicationContext())
                newNameBox.Text = db.Users.Single(u => u.Id == userId).Name;
        }

        void hideStatusLabelResult()
        {
            statusLabel.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(db = new ApplicationContext())
            {
                if((passwordBox.Password != db.Users.Single(u => u.Id == userId).Password) & !(db.Users.Single(u => u.Id == userId).Password == null && passwordBox.Password == ""))
                {
                    statusLabel.Content = "Wrong password";
                    statusLabel.Foreground = Brushes.IndianRed;
                    statusLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    if(newPasswordBox.Password != ConfirmPasswordBox.Password)
                    {
                        statusLabel.Content = "New password isn't confirmed";
                        statusLabel.Foreground = Brushes.IndianRed;
                        statusLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (newNameBox.Text != db.Users.Single(u => u.Id == userId).Name)
                            db.Users.Single(u => u.Id == userId).Name = newNameBox.Text;


                        statusLabel.Content = "Sucesses";
                        statusLabel.Foreground = Brushes.ForestGreen;
                        statusLabel.Visibility = Visibility.Visible;

                        if (newPasswordBox.Password != "")
                        {
                            db.Users.Single(u => u.Id == userId).Password = newPasswordBox.Password;
                        }
                        else
                            db.Users.Single(u => u.Id == userId).Password = null;

                        db.SaveChanges();
                    }
                }

                
            }
        }

        private void newNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hideStatusLabelResult();
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hideStatusLabelResult();
        }

        private void newPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hideStatusLabelResult();
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            hideStatusLabelResult();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this user?", "Question", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBoxResult clarify_result = MessageBox.Show("Are you sure? This changes can't be recovered.", "Question", MessageBoxButton.YesNo);
                if (clarify_result == MessageBoxResult.Yes)
                {
                    using(db = new ApplicationContext())
                    {
                        User user = db.Users.Single(u => u.Id == userId);
                        db.Users.Remove(user);

                        foreach (Achievement ach in db.Achievements.Where(a => a.UserId == userId).ToList())
                            db.Achievements.Remove(ach);

                        foreach (Branch branch in db.Branches.Where(b => b.UserId == userId).ToList())
                        {
                            foreach (SMART smrt in db.SMARTs.Where(s => s.ProjectId == branch.Id).ToList())
                                db.SMARTs.Remove(smrt);

                            db.Branches.Remove(branch);
                        }

                        foreach (DiaryRecord page in db.DiaryRecords.Where(r => r.UserId == userId).ToList())
                            db.DiaryRecords.Remove(page);

                        foreach (DiltsPiramid piramid in db.DiltsPiramids.Where(p => p.UserId == userId).ToList())
                            db.DiltsPiramids.Remove(piramid);

                        foreach (GeneralSphere sphere in db.GeneralSpheres.Where(s => s.UserId == userId).ToList())
                            db.GeneralSpheres.Remove(sphere);

                        foreach (Idea idea in db.Ideas.Where(i => i.UserId == userId).ToList())
                            db.Ideas.Remove(idea);

                        foreach (LongtermPlan pln in db.LongtermPlans.Where(p => p.UserId == userId).ToList())
                            db.LongtermPlans.Remove(pln);

                        foreach (PlanPhase phase in db.PlanPhases.Where(p => p.UserId == userId).ToList())
                        {
                            foreach (PlanTask task in db.PlanTasks.Where(t => t.PlanPhaseId == phase.Id))
                                db.PlanTasks.Remove(task);

                            db.PlanPhases.Remove(phase);
                        }

                        

                        foreach (brightnessSphere sphere in db.brightnessSpheres.Where(s => s.UserId == userId).ToList())
                            db.brightnessSpheres.Remove(sphere);

                        foreach (environmentSphere sphere in db.environmentSpheres.Where(s => s.UserId == userId).ToList())
                            db.environmentSpheres.Remove(sphere);

                        foreach (healthSphere sphere in db.healthSpheres.Where(s => s.UserId == userId).ToList())
                            db.healthSpheres.Remove(sphere);

                        foreach (independenceSphere sphere in db.independenceSpheres.Where(s => s.UserId == userId).ToList())
                            db.independenceSpheres.Remove(sphere);

                        foreach (relationSphere sphere in db.relationSpheres.Where(s => s.UserId == userId).ToList())
                            db.relationSpheres.Remove(sphere);

                        foreach (selfdevelopmentSphere sphere in db.selfdevelopmentSpheres.Where(s => s.UserId == userId).ToList())
                            db.selfdevelopmentSpheres.Remove(sphere);

                        foreach (spiritualitySphere sphere in db.spiritualitySpheres.Where(s => s.UserId == userId).ToList())
                            db.spiritualitySpheres.Remove(sphere);

                        foreach (vocationSphere sphere in db.vocationSpheres.Where(s => s.UserId == userId).ToList())
                            db.vocationSpheres.Remove(sphere);

                        db.SaveChanges();

                        Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
                        Application.Current.Properties["inMainPage"] = false;
                        ((Label)Application.Current.Properties["pageNameLabel"]).Content = "";
                        Application.Current.Properties["user"] = null;
                        ((Button)Application.Current.Properties["SettingsButton"]).Visibility = Visibility.Hidden;
                        Application.Current.Properties["logIn"] = false;
                        PageFrame.Content = new Authorization();
                    }
                }
                else
                if (clarify_result == MessageBoxResult.No)
                {
                }
            }
            else
            if (result == MessageBoxResult.No)
            {
            }
        }
    }
}
