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
    /// Interaction logic for branchPage.xaml
    /// </summary>
    public partial class branchPage : Page
    {
        ApplicationContext db;
        Branch branch;
        DiltsPiramid piramid;
        int userId;
        bool wasCreated;
        bool displayPiramid = false;
        public branchPage()
        {
            InitializeComponent();
            using (db = new ApplicationContext())
            {

                float percent = 0;

                int userId = (int)Application.Current.Properties["user"];
                int branchId = (int)Application.Current.Properties["branch"];
                branch = db.Branches.Single(b => b.Id == branchId);

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

                branch.Progress = percent;
                db.SaveChanges();

            }
            userId = (int)Application.Current.Properties["user"];
            if (Application.Current.Properties["branch"] != null)
            {
                int id = (int)Application.Current.Properties["branch"];
                wasCreated = true;
                using (db = new ApplicationContext())
                {
                    //DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == id).First();

                    branch = db.Branches.Single(b => b.Id == id);

                    branchNameBox.Text = branch.Name;

                    if (db.SMARTs.Where(s => s.ProjectId == branch.Id).Count() > 0)
                    {
                        SMART smart = db.SMARTs.Single(s => s.ProjectId == branch.Id);
                        SMART_S_Box.Text = smart.S;
                        SMART_M_Box.Text = smart.M;
                        SMART_A_Box.Text = smart.A;
                        SMART_R_Box.Text = smart.R;
                        SMART_T_Box.Text = smart.T;

                        addMarkButton.Visibility = Visibility.Collapsed;
                        SMARTGrid.Visibility = Visibility.Visible;

                        markBox.Visibility = Visibility.Visible;
                        markLabel.Visibility = Visibility.Visible;
                        markBox.Text = branch.Mark;
                    }
                    else
                    {
                        addMarkButton.Visibility = Visibility.Visible;
                        SMARTGrid.Visibility = Visibility.Collapsed;
                        markBox.Visibility = Visibility.Collapsed;
                        markLabel.Visibility = Visibility.Collapsed;
                    }
                    if (db.PlanPhases.Where(p => p.BranchId == branch.Id).Count() > 0)
                    {
                        addPlanButton.Visibility = Visibility.Collapsed;

                        progressBar.Visibility = Visibility.Visible;
                        progressLabel.Visibility = Visibility.Visible;
                        progressButton.Visibility = Visibility.Visible;

                        progressBar.Value = branch.Progress;
                        progressLabel.Content = branch.Progress.ToString() + '%';
                    }
                    else
                    {
                        addPlanButton.Visibility = Visibility.Visible;

                        progressBar.Visibility = Visibility.Collapsed;
                        progressLabel.Visibility = Visibility.Collapsed;
                        progressButton.Visibility = Visibility.Collapsed;
                    }
                    if (db.DiltsPiramids.Where(p => p.UserId == userId & p.ProjectId == branch.Id).Count() > 0)
                    {
                        AddPiramidButton.Visibility = Visibility.Collapsed;

                        piramidBorder.Visibility = Visibility.Visible;
                        piramidCanvas.Visibility = Visibility.Visible;
                        missionTexBox.Visibility = Visibility.Visible;

                        displayPiramid = true;
                        RefreshPiramid();
                    }
                    else
                    {
                        AddPiramidButton.Visibility = Visibility.Visible;

                        piramidBorder.Visibility = Visibility.Collapsed;
                        missionTexBox.Visibility = Visibility.Collapsed;
                    }
                    missionTexBox.Text = branch.Mission;

                    closeButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                branch = null;
                wasCreated = false;
                SMARTGrid.Visibility = Visibility.Collapsed;

                piramidBorder.Visibility = Visibility.Collapsed;
                AddPiramidButton.Visibility = Visibility.Collapsed;
                missionTexBox.Visibility = Visibility.Collapsed;

                progressBar.Visibility = Visibility.Collapsed;
                progressLabel.Visibility = Visibility.Collapsed;
                progressButton.Visibility = Visibility.Collapsed;

                addMarkButton.Visibility = Visibility.Collapsed;
                markBox.Visibility = Visibility.Collapsed;
                markLabel.Visibility = Visibility.Collapsed;

                closeButton.Visibility = Visibility.Collapsed;
                addPlanButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            branchNameBox.Width = middleCol.ActualWidth * 0.8f;
            piramidCanvas.Children.Clear();
            //LoadDiltsPiramidCanvas();


        }

        private void progressPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Properties["secondPage"] = true;
            Application.Current.Properties["lastPage"] = this;
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            PageFrame.Content = new DiltsPiramidPage();
        }

        void LoadDiltsPiramidCanvas()
        {
            int cornerRadius = 5;
            double borderThickness = 1f;
            Brush backgroung = Brushes.LightYellow;
            Brush borderBrush = Brushes.Black;

            double canvasWidth = piramidCanvas.ActualWidth;
            double canvasHeight = piramidCanvas.ActualHeight;

            double blockWidthStep = canvasWidth / 20;
            double blockHeightStep = canvasHeight / 8;

            Border rectangle_1 = new Border();
            rectangle_1.Width = blockWidthStep * 13;
            rectangle_1.Height = blockHeightStep;
            piramidCanvas.Children.Add(rectangle_1);
            rectangle_1.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_1.Background = Brushes.AliceBlue;
            rectangle_1.BorderBrush = borderBrush;
            rectangle_1.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_1, canvasWidth / 2 - rectangle_1.Width / 2);
            Canvas.SetTop(rectangle_1, blockHeightStep);

            StackPanel panel_1 = new StackPanel();
            rectangle_1.Child = panel_1;

            TextBlock label_1 = new TextBlock();
            label_1.Width = blockWidthStep * 13;
            label_1.Height = blockHeightStep / 2;
            label_1.Text = "Purpose";
            label_1.TextAlignment = TextAlignment.Center;
            label_1.TextWrapping = TextWrapping.WrapWithOverflow;
            label_1.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_1, canvasWidth / 2 - rectangle_1.Width / 2);
            Canvas.SetTop(label_1, blockHeightStep);
            panel_1.Children.Add(label_1);

            TextBlock textbox_1 = new TextBlock();
            textbox_1.Width = blockWidthStep * 13;
            textbox_1.Height = blockHeightStep / 2;
            textbox_1.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_1.VerticalAlignment = VerticalAlignment.Center;
            textbox_1.TextAlignment = TextAlignment.Center;
            textbox_1.TextWrapping = TextWrapping.WrapWithOverflow;
            textbox_1.Text = piramid.People;
            panel_1.Children.Add(textbox_1);

            Border rectangle_2 = new Border();
            rectangle_2.Width = blockWidthStep * 14;
            rectangle_2.Height = blockHeightStep;
            piramidCanvas.Children.Add(rectangle_2);
            rectangle_2.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_2.Background = Brushes.BlueViolet;
            rectangle_2.BorderBrush = borderBrush;
            rectangle_2.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_2, canvasWidth / 2 - rectangle_2.Width / 2);
            Canvas.SetTop(rectangle_2, blockHeightStep * 2);

            StackPanel panel_2 = new StackPanel();
            rectangle_2.Child = panel_2;

            TextBlock label_2 = new TextBlock();
            label_2.Width = blockWidthStep * 14;
            label_2.Height = blockHeightStep / 2;
            label_2.Text = "Indentity";
            label_2.TextAlignment = TextAlignment.Center;
            label_2.TextWrapping = TextWrapping.WrapWithOverflow;
            label_2.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_2, canvasWidth / 2 - rectangle_2.Width / 2);
            Canvas.SetTop(label_2, blockHeightStep * 2);
            panel_2.Children.Add(label_2);

            TextBlock textbox_2 = new TextBlock();
            textbox_2.Width = blockWidthStep * 13;
            textbox_2.Height = blockHeightStep / 2;
            textbox_2.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_2.VerticalAlignment = VerticalAlignment.Center;
            textbox_2.TextAlignment = TextAlignment.Center;
            textbox_2.TextWrapping = TextWrapping.WrapWithOverflow;
            textbox_2.Text = piramid.Role;
            panel_2.Children.Add(textbox_2);

            Border rectangle_3 = new Border();
            rectangle_3.Width = blockWidthStep * 15;
            rectangle_3.Height = blockHeightStep;
            piramidCanvas.Children.Add(rectangle_3);
            rectangle_3.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_3.Background = Brushes.DeepSkyBlue;
            rectangle_3.BorderBrush = borderBrush;
            rectangle_3.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_3, canvasWidth / 2 - rectangle_3.Width / 2);
            Canvas.SetTop(rectangle_3, blockHeightStep * 3);

            StackPanel panel_3 = new StackPanel();
            rectangle_3.Child = panel_3;

            TextBlock label_3 = new TextBlock();
            label_3.Width = blockWidthStep * 15;
            label_3.Height = blockHeightStep / 2;
            label_3.Text = "Beliefs and Values";
            label_3.TextAlignment = TextAlignment.Center;
            label_3.TextWrapping = TextWrapping.WrapWithOverflow;
            label_3.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_3, canvasWidth / 2 - rectangle_3.Width / 2);
            Canvas.SetTop(label_3, blockHeightStep * 3);
            panel_3.Children.Add(label_3);

            TextBlock textbox_3 = new TextBlock();
            textbox_3.Width = blockWidthStep * 13;
            textbox_3.Height = blockHeightStep / 2;
            textbox_3.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_3.VerticalAlignment = VerticalAlignment.Center;
            textbox_3.TextAlignment = TextAlignment.Center;
            textbox_3.TextWrapping = TextWrapping.WrapWithOverflow;
            textbox_3.Text = piramid.Values;
            panel_3.Children.Add(textbox_3);

            Border rectangle_4 = new Border();
            rectangle_4.Width = blockWidthStep * 16;
            rectangle_4.Height = blockHeightStep;
            piramidCanvas.Children.Add(rectangle_4);
            rectangle_4.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_4.Background = Brushes.ForestGreen;
            rectangle_4.BorderBrush = borderBrush;
            rectangle_4.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_4, canvasWidth / 2 - rectangle_4.Width / 2);
            Canvas.SetTop(rectangle_4, blockHeightStep * 4);

            StackPanel panel_4 = new StackPanel();
            rectangle_4.Child = panel_4;

            TextBlock label_4 = new TextBlock();
            label_4.Width = blockWidthStep * 16;
            label_4.Height = blockHeightStep / 2;
            label_4.Text = "Capabilities";
            label_4.TextAlignment = TextAlignment.Center;
            label_4.TextWrapping = TextWrapping.WrapWithOverflow;
            label_4.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_4, canvasWidth / 2 - rectangle_4.Width / 2);
            Canvas.SetTop(label_4, blockHeightStep * 4);
            panel_4.Children.Add(label_4);

            TextBlock textbox_4 = new TextBlock();
            textbox_4.Width = blockWidthStep * 13;
            textbox_4.Height = blockHeightStep / 2;
            textbox_4.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_4.VerticalAlignment = VerticalAlignment.Center;
            textbox_4.TextAlignment = TextAlignment.Center;
            textbox_4.TextWrapping = TextWrapping.WrapWithOverflow;
            textbox_4.Text = piramid.Skills;
            panel_4.Children.Add(textbox_4);

            Border rectangle_5 = new Border();
            rectangle_5.Width = blockWidthStep * 17;
            rectangle_5.Height = blockHeightStep;
            piramidCanvas.Children.Add(rectangle_5);
            rectangle_5.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_5.Background = Brushes.LightGoldenrodYellow;
            rectangle_5.BorderBrush = borderBrush;
            rectangle_5.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_5, canvasWidth / 2 - rectangle_5.Width / 2);
            Canvas.SetTop(rectangle_5, blockHeightStep * 5);

            StackPanel panel_5 = new StackPanel();
            rectangle_5.Child = panel_5;

            TextBlock label_5 = new TextBlock();
            label_5.Width = blockWidthStep * 17;
            label_5.Height = blockHeightStep / 2;
            label_5.Text = "Behaviour";
            label_5.TextAlignment = TextAlignment.Center;
            label_5.TextWrapping = TextWrapping.WrapWithOverflow;
            label_5.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_5, canvasWidth / 2 - rectangle_5.Width / 2);
            Canvas.SetTop(label_5, blockHeightStep * 5);
            panel_5.Children.Add(label_5);

            TextBlock textbox_5 = new TextBlock();
            textbox_5.Width = blockWidthStep * 13;
            textbox_5.Height = blockHeightStep / 2;
            textbox_5.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_5.VerticalAlignment = VerticalAlignment.Center;
            textbox_5.TextAlignment = TextAlignment.Center;
            textbox_5.TextWrapping = TextWrapping.WrapWithOverflow;
            textbox_5.Text = piramid.Behavior;
            panel_5.Children.Add(textbox_5);

            Border rectangle_6 = new Border();
            rectangle_6.Width = blockWidthStep * 18;
            rectangle_6.Height = blockHeightStep;
            piramidCanvas.Children.Add(rectangle_6);
            rectangle_6.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_6.Background = Brushes.IndianRed;
            rectangle_6.BorderBrush = borderBrush;
            rectangle_6.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_6, canvasWidth / 2 - rectangle_6.Width / 2);
            Canvas.SetTop(rectangle_6, blockHeightStep * 6);

            StackPanel panel_6 = new StackPanel();
            rectangle_6.Child = panel_6;

            TextBlock label_6 = new TextBlock();
            label_6.Width = blockWidthStep * 18;
            label_6.Height = blockHeightStep / 2;
            label_6.Text = "Environment";
            label_6.TextAlignment = TextAlignment.Center;
            label_6.TextWrapping = TextWrapping.WrapWithOverflow;
            label_6.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_6, canvasWidth / 2 - rectangle_6.Width / 2);
            Canvas.SetTop(label_6, blockHeightStep * 6);
            panel_6.Children.Add(label_6);

            TextBlock textbox_6 = new TextBlock();
            textbox_6.Width = blockWidthStep * 13;
            textbox_6.Height = blockHeightStep / 2;
            textbox_6.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_6.VerticalAlignment = VerticalAlignment.Center;
            textbox_6.TextAlignment = TextAlignment.Center;
            textbox_6.TextWrapping = TextWrapping.WrapWithOverflow;
            textbox_6.Text = piramid.Environment;
            panel_6.Children.Add(textbox_6);
        }

        private void progressButton_Click(object sender, RoutedEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["secondPage"] = true;
            Application.Current.Properties["lastPage"] = this;
            PageFrame.Content = new PlanGeneralPage();
        }

        private void branchNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                db.Branches.Single(bran => bran.Id == branch.Id).Name = branchNameBox.Text;
                db.SaveChanges();
                ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Project: " + "\"" + branchNameBox.Text + "\" page";
            }
        }

        private void addMarkButton_Click(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                Branch b = db.Branches.Single(bran => bran.Id == branch.Id);
                branch.Mark = "";
                markBox.Text = "";
                markBox.Visibility = Visibility.Visible;
                markLabel.Visibility = Visibility.Visible;
                SMARTGrid.Visibility = Visibility.Visible;
                addMarkButton.Visibility = Visibility.Collapsed;
                db.SMARTs.Add(new SMART(b.Id));
                db.SaveChanges();
            }

        }

        private void markBox_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                Branch b = db.Branches.Single(bran => bran.Id == branch.Id);
                b.Mark = markBox.Text;
                db.SaveChanges();
            }
        }

        private void SMART_S_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                SMART smart = db.SMARTs.Single(s => s.ProjectId == branch.Id);
                smart.S = SMART_S_Box.Text;
                db.SaveChanges();
            }
        }

        private void SMART_M_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                SMART smart = db.SMARTs.Single(s => s.ProjectId == branch.Id);
                smart.M = SMART_M_Box.Text;
                db.SaveChanges();
            }
        }

        private void SMART_A_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                SMART smart = db.SMARTs.Single(s => s.ProjectId == branch.Id);
                smart.A = SMART_A_Box.Text;
                db.SaveChanges();
            }
        }

        private void SMART_R_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                SMART smart = db.SMARTs.Single(s => s.ProjectId == branch.Id);
                smart.R = SMART_R_Box.Text;
                db.SaveChanges();
            }
        }

        private void SMART_T_Box_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                SMART smart = db.SMARTs.Single(s => s.ProjectId == branch.Id);
                smart.T = SMART_T_Box.Text;
                db.SaveChanges();
            }
        }

        private void addPlanButton_Click(object sender, RoutedEventArgs e)
        {
            addPlanButton.Visibility = Visibility.Collapsed;

            progressBar.Visibility = Visibility.Visible;
            progressLabel.Visibility = Visibility.Visible;
            progressButton.Visibility = Visibility.Visible;
        }

        private void AddPiramidButton_Click(object sender, RoutedEventArgs e)
        {

            using (db = new ApplicationContext())
            {
                DiltsPiramid piramid = new DiltsPiramid(userId, branch.Id);
                db.DiltsPiramids.Add(piramid);
                db.SaveChanges();
                AddPiramidButton.Visibility = Visibility.Collapsed;

                piramidBorder.Visibility = Visibility.Visible;
                missionTexBox.Visibility = Visibility.Visible;

                displayPiramid = true;
            }
            RefreshPiramid();
        }

        private void missionTexBox_LostFocus(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {
                Branch bran = db.Branches.Single(b => b.Id == branch.Id);
                bran.Mission = missionTexBox.Text;
                db.SaveChanges();
            }
        }

        private void piramidBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            piramidBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void piramidBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            piramidBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        void RefreshPiramid()
        {
            if (displayPiramid)
            {
                piramidCanvas.Children.Clear();
                using (db = new ApplicationContext())
                {
                    int projectId = branch.Id;

                    DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();

                    piramidCanvas.MinHeight = 350;
                    piramidCanvas.MinWidth = 400;

                    int cornerRadius = 5;
                    double borderThickness = 1f;
                    Brush backgroung = Brushes.LightYellow;
                    Brush borderBrush = Brushes.Black;

                    double canvasWidth = piramidCanvas.ActualWidth;
                    double canvasHeight = piramidCanvas.ActualHeight;

                    double blockWidthStep = canvasWidth / 20;
                    double blockHeightStep = canvasHeight / 8;

                    Border rectangle_1 = new Border();
                    rectangle_1.Width = blockWidthStep * 13;
                    rectangle_1.Height = blockHeightStep;
                    piramidCanvas.Children.Add(rectangle_1);
                    rectangle_1.CornerRadius = new CornerRadius(cornerRadius);
                    rectangle_1.Background = Brushes.AliceBlue;
                    rectangle_1.BorderBrush = borderBrush;
                    rectangle_1.BorderThickness = new Thickness(borderThickness);
                    Canvas.SetLeft(rectangle_1, canvasWidth / 2 - rectangle_1.Width / 2);
                    Canvas.SetTop(rectangle_1, blockHeightStep * 1);


                    StackPanel panel_1 = new StackPanel();
                    rectangle_1.Child = panel_1;

                    TextBlock label_1 = new TextBlock();
                    label_1.Width = blockWidthStep * 13;
                    label_1.Height = blockHeightStep / 2;
                    label_1.Text = "Purpose";
                    label_1.TextAlignment = TextAlignment.Center;
                    label_1.TextWrapping = TextWrapping.WrapWithOverflow;
                    label_1.FontWeight = FontWeights.Bold;
                    Canvas.SetLeft(label_1, canvasWidth / 2 - rectangle_1.Width / 2);
                    Canvas.SetTop(label_1, blockHeightStep);
                    panel_1.Children.Add(label_1);

                    TextBlock textbox_1 = new TextBlock();
                    textbox_1.Width = blockWidthStep * 13;
                    textbox_1.Height = blockHeightStep / 2;
                    textbox_1.HorizontalAlignment = HorizontalAlignment.Center;
                    textbox_1.VerticalAlignment = VerticalAlignment.Center;
                    textbox_1.TextAlignment = TextAlignment.Center;
                    textbox_1.Text = piramid.People;
                    panel_1.Children.Add(textbox_1);

                    Border rectangle_2 = new Border();
                    rectangle_2.Width = blockWidthStep * 14;
                    rectangle_2.Height = blockHeightStep;
                    piramidCanvas.Children.Add(rectangle_2);
                    rectangle_2.CornerRadius = new CornerRadius(cornerRadius);
                    rectangle_2.Background = Brushes.BlueViolet;
                    rectangle_2.BorderBrush = borderBrush;
                    rectangle_2.BorderThickness = new Thickness(borderThickness);
                    Canvas.SetLeft(rectangle_2, canvasWidth / 2 - rectangle_2.Width / 2);
                    Canvas.SetTop(rectangle_2, blockHeightStep * 2);

                    StackPanel panel_2 = new StackPanel();
                    rectangle_2.Child = panel_2;

                    TextBlock label_2 = new TextBlock();
                    label_2.Width = blockWidthStep * 14;
                    label_2.Height = blockHeightStep / 2;
                    label_2.Text = "Indentity";
                    label_2.TextAlignment = TextAlignment.Center;
                    label_2.TextWrapping = TextWrapping.WrapWithOverflow;
                    label_2.FontWeight = FontWeights.Bold;
                    Canvas.SetLeft(label_2, canvasWidth / 2 - rectangle_2.Width / 2);
                    Canvas.SetTop(label_2, blockHeightStep * 2);
                    panel_2.Children.Add(label_2);

                    TextBlock textbox_2 = new TextBlock();
                    textbox_2.Width = blockWidthStep * 13;
                    textbox_2.Height = blockHeightStep / 2;
                    textbox_2.HorizontalAlignment = HorizontalAlignment.Center;
                    textbox_2.TextAlignment = TextAlignment.Center;
                    textbox_2.Text = piramid.Role;
                    panel_2.Children.Add(textbox_2);

                    Border rectangle_3 = new Border();
                    rectangle_3.Width = blockWidthStep * 15;
                    rectangle_3.Height = blockHeightStep;
                    piramidCanvas.Children.Add(rectangle_3);
                    rectangle_3.CornerRadius = new CornerRadius(cornerRadius);
                    rectangle_3.Background = Brushes.DeepSkyBlue;
                    rectangle_3.BorderBrush = borderBrush;
                    rectangle_3.BorderThickness = new Thickness(borderThickness);
                    Canvas.SetLeft(rectangle_3, canvasWidth / 2 - rectangle_3.Width / 2);
                    Canvas.SetTop(rectangle_3, blockHeightStep * 3);

                    StackPanel panel_3 = new StackPanel();
                    rectangle_3.Child = panel_3;

                    TextBlock label_3 = new TextBlock();
                    label_3.Width = blockWidthStep * 15;
                    label_3.Height = blockHeightStep / 2;
                    label_3.Text = "Beliefs and Values";
                    label_3.TextAlignment = TextAlignment.Center;
                    label_3.TextWrapping = TextWrapping.WrapWithOverflow;
                    label_3.FontWeight = FontWeights.Bold;
                    Canvas.SetLeft(label_3, canvasWidth / 2 - rectangle_3.Width / 2);
                    Canvas.SetTop(label_3, blockHeightStep * 3);
                    panel_3.Children.Add(label_3);

                    TextBlock textbox_3 = new TextBlock();
                    textbox_3.Width = blockWidthStep * 13;
                    textbox_3.Height = blockHeightStep / 2;
                    textbox_3.HorizontalAlignment = HorizontalAlignment.Center;
                    textbox_3.VerticalAlignment = VerticalAlignment.Center;
                    textbox_3.TextAlignment = TextAlignment.Center;
                    textbox_3.Text = piramid.Values;
                    panel_3.Children.Add(textbox_3);

                    Border rectangle_4 = new Border();
                    rectangle_4.Width = blockWidthStep * 16;
                    rectangle_4.Height = blockHeightStep;
                    piramidCanvas.Children.Add(rectangle_4);
                    rectangle_4.CornerRadius = new CornerRadius(cornerRadius);
                    rectangle_4.Background = Brushes.ForestGreen;
                    rectangle_4.BorderBrush = borderBrush;
                    rectangle_4.BorderThickness = new Thickness(borderThickness);
                    Canvas.SetLeft(rectangle_4, canvasWidth / 2 - rectangle_4.Width / 2);
                    Canvas.SetTop(rectangle_4, blockHeightStep * 4);

                    StackPanel panel_4 = new StackPanel();
                    rectangle_4.Child = panel_4;

                    TextBlock label_4 = new TextBlock();
                    label_4.Width = blockWidthStep * 16;
                    label_4.Height = blockHeightStep / 2;
                    label_4.Text = "Capabilities";
                    label_4.TextAlignment = TextAlignment.Center;
                    label_4.TextWrapping = TextWrapping.WrapWithOverflow;
                    label_4.FontWeight = FontWeights.Bold;
                    Canvas.SetLeft(label_4, canvasWidth / 2 - rectangle_4.Width / 2);
                    Canvas.SetTop(label_4, blockHeightStep * 4);
                    panel_4.Children.Add(label_4);

                    TextBlock textbox_4 = new TextBlock();
                    textbox_4.Width = blockWidthStep * 13;
                    textbox_4.Height = blockHeightStep / 2;
                    textbox_4.HorizontalAlignment = HorizontalAlignment.Center;
                    textbox_4.VerticalAlignment = VerticalAlignment.Center;
                    textbox_4.TextAlignment = TextAlignment.Center;
                    textbox_4.Text = piramid.Skills;
                    panel_4.Children.Add(textbox_4);

                    Border rectangle_5 = new Border();
                    rectangle_5.Width = blockWidthStep * 17;
                    rectangle_5.Height = blockHeightStep;
                    piramidCanvas.Children.Add(rectangle_5);
                    rectangle_5.CornerRadius = new CornerRadius(cornerRadius);
                    rectangle_5.Background = Brushes.LightGoldenrodYellow;
                    rectangle_5.BorderBrush = borderBrush;
                    rectangle_5.BorderThickness = new Thickness(borderThickness);
                    Canvas.SetLeft(rectangle_5, canvasWidth / 2 - rectangle_5.Width / 2);
                    Canvas.SetTop(rectangle_5, blockHeightStep * 5);

                    StackPanel panel_5 = new StackPanel();
                    rectangle_5.Child = panel_5;

                    TextBlock label_5 = new TextBlock();
                    label_5.Width = blockWidthStep * 17;
                    label_5.Height = blockHeightStep / 2;
                    label_5.Text = "Behaviour";
                    label_5.TextAlignment = TextAlignment.Center;
                    label_5.TextWrapping = TextWrapping.WrapWithOverflow;
                    label_5.FontWeight = FontWeights.Bold;
                    Canvas.SetLeft(label_5, canvasWidth / 2 - rectangle_5.Width / 2);
                    Canvas.SetTop(label_5, blockHeightStep * 5);
                    panel_5.Children.Add(label_5);

                    TextBlock textbox_5 = new TextBlock();
                    textbox_5.Width = blockWidthStep * 13;
                    textbox_5.Height = blockHeightStep / 2;
                    textbox_5.HorizontalAlignment = HorizontalAlignment.Center;
                    textbox_5.VerticalAlignment = VerticalAlignment.Center;
                    textbox_5.TextAlignment = TextAlignment.Center;
                    textbox_5.Text = piramid.Behavior;
                    panel_5.Children.Add(textbox_5);

                    Border rectangle_6 = new Border();
                    rectangle_6.Width = blockWidthStep * 18;
                    rectangle_6.Height = blockHeightStep;
                    piramidCanvas.Children.Add(rectangle_6);
                    rectangle_6.CornerRadius = new CornerRadius(cornerRadius);
                    rectangle_6.Background = Brushes.IndianRed;
                    rectangle_6.BorderBrush = borderBrush;
                    rectangle_6.BorderThickness = new Thickness(borderThickness);
                    Canvas.SetLeft(rectangle_6, canvasWidth / 2 - rectangle_6.Width / 2);
                    Canvas.SetTop(rectangle_6, blockHeightStep * 6);

                    StackPanel panel_6 = new StackPanel();
                    rectangle_6.Child = panel_6;

                    TextBlock label_6 = new TextBlock();
                    label_6.Width = blockWidthStep * 18;
                    label_6.Height = blockHeightStep / 2;
                    label_6.Text = "Environment";
                    label_6.TextAlignment = TextAlignment.Center;
                    label_6.TextWrapping = TextWrapping.WrapWithOverflow;
                    label_6.FontWeight = FontWeights.Bold;
                    Canvas.SetLeft(label_6, canvasWidth / 2 - rectangle_6.Width / 2);
                    Canvas.SetTop(label_6, blockHeightStep * 6);
                    panel_6.Children.Add(label_6);

                    TextBlock textbox_6 = new TextBlock();
                    textbox_6.Width = blockWidthStep * 13;
                    textbox_6.Height = blockHeightStep / 2;
                    textbox_6.HorizontalAlignment = HorizontalAlignment.Center;
                    textbox_6.VerticalAlignment = VerticalAlignment.Center;
                    textbox_6.TextAlignment = TextAlignment.Center;
                    textbox_6.Text = piramid.Environment;
                    panel_6.Children.Add(textbox_6);
                }
            }
        }

        private void piramidCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshPiramid();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            { }
            else
            {
                using (db = new ApplicationContext())
                {
                    int id = branch.Id;
                    db.Branches.Single(bran => bran.Id == id).Closed = 1;
                    db.SaveChanges();
                    Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
                    Application.Current.Properties["secondPage"] = false;
                    Application.Current.Properties["inMainPage"] = true;
                    PageFrame.Content = new MainPage();
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (db = new ApplicationContext())
            {

                float percent = 0;

                int userId = (int)Application.Current.Properties["user"];
                int branchId = (int)Application.Current.Properties["branch"];
                branch = db.Branches.Single(b => b.Id == branchId);

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

                branch.Progress = percent;
                db.SaveChanges();

            }
            userId = (int)Application.Current.Properties["user"];
            if (Application.Current.Properties["branch"] != null)
            {
                int id = (int)Application.Current.Properties["branch"];
                wasCreated = true;
                using (db = new ApplicationContext())
                {
                    //DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == id).First();

                    branch = db.Branches.Single(b => b.Id == id);

                    branchNameBox.Text = branch.Name;

                    if (db.SMARTs.Where(s => s.ProjectId == branch.Id).Count() > 0)
                    {
                        SMART smart = db.SMARTs.Single(s => s.ProjectId == branch.Id);
                        SMART_S_Box.Text = smart.S;
                        SMART_M_Box.Text = smart.M;
                        SMART_A_Box.Text = smart.A;
                        SMART_R_Box.Text = smart.R;
                        SMART_T_Box.Text = smart.T;

                        addMarkButton.Visibility = Visibility.Collapsed;
                        SMARTGrid.Visibility = Visibility.Visible;

                        markBox.Visibility = Visibility.Visible;
                        markLabel.Visibility = Visibility.Visible;
                        markBox.Text = branch.Mark;
                    }
                    else
                    {
                        addMarkButton.Visibility = Visibility.Visible;
                        SMARTGrid.Visibility = Visibility.Collapsed;
                        markBox.Visibility = Visibility.Collapsed;
                        markLabel.Visibility = Visibility.Collapsed;
                    }
                    if (db.PlanPhases.Where(p => p.BranchId == branch.Id).Count() > 0)
                    {
                        addPlanButton.Visibility = Visibility.Collapsed;

                        progressBar.Visibility = Visibility.Visible;
                        progressLabel.Visibility = Visibility.Visible;
                        progressButton.Visibility = Visibility.Visible;

                        progressBar.Value = branch.Progress;
                        progressLabel.Content = branch.Progress.ToString() + '%';
                    }
                    else
                    {
                        addPlanButton.Visibility = Visibility.Visible;

                        progressBar.Visibility = Visibility.Collapsed;
                        progressLabel.Visibility = Visibility.Collapsed;
                        progressButton.Visibility = Visibility.Collapsed;
                    }
                    if (db.DiltsPiramids.Where(p => p.UserId == userId & p.ProjectId == branch.Id).Count() > 0)
                    {
                        AddPiramidButton.Visibility = Visibility.Collapsed;

                        piramidBorder.Visibility = Visibility.Visible;
                        piramidCanvas.Visibility = Visibility.Visible;
                        missionTexBox.Visibility = Visibility.Visible;

                        displayPiramid = true;
                        RefreshPiramid();
                    }
                    else
                    {
                        AddPiramidButton.Visibility = Visibility.Visible;

                        piramidBorder.Visibility = Visibility.Collapsed;
                        missionTexBox.Visibility = Visibility.Collapsed;
                    }
                    missionTexBox.Text = branch.Mission;

                    closeButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                branch = null;
                wasCreated = false;
                SMARTGrid.Visibility = Visibility.Collapsed;

                piramidBorder.Visibility = Visibility.Collapsed;
                AddPiramidButton.Visibility = Visibility.Collapsed;
                missionTexBox.Visibility = Visibility.Collapsed;

                progressBar.Visibility = Visibility.Collapsed;
                progressLabel.Visibility = Visibility.Collapsed;
                progressButton.Visibility = Visibility.Collapsed;

                addMarkButton.Visibility = Visibility.Collapsed;
                markBox.Visibility = Visibility.Collapsed;
                markLabel.Visibility = Visibility.Collapsed;

                closeButton.Visibility = Visibility.Collapsed;
                addPlanButton.Visibility = Visibility.Collapsed;
            }
        }
    }
}
