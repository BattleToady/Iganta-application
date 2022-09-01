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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        ApplicationContext db;
        LongtermPlan plan;
        Dictionary<string, int> zones = new Dictionary<string, int>();
        List<Branch> branches;
        public MainPage()
        {
            InitializeComponent();
            int userId = (int)Application.Current.Properties["user"];
            using(db = new ApplicationContext())
            { 
                string userName = db.Users.Single(u => u.Id == userId).Name;
                ((Label)Application.Current.Properties["pageNameLabel"]).Content = userName +  "-page";

                if (db.LongtermPlans.Where(p => p.UserId == userId).Count() > 0)
                {
                    plan = db.LongtermPlans.Where(p => p.UserId == userId).First();
                }
                else
                {
                    plan = new LongtermPlan(userId);
                    db.LongtermPlans.Add(plan);
                }


                if (!db.GeneralSpheres.Where(s => s.UserId == userId).Any())
                {
                    zones["health"] = 0;
                    zones["finance"] = 0;
                    zones["carier"] = 0;
                    zones["friends"] = 0;
                    zones["family"] = 0;
                    zones["relationship"] = 0;
                    zones["home"] = 0;
                    zones["self_realization"] = 0;
                    zones["hobby"] = 0;
                    zones["fun"] = 0;
                    zones["mission"] = 0;
                }
                else
                {
                    GeneralSphere genSphere = db.GeneralSpheres.Single(s => s.UserId == userId);
                    zones["health"] = genSphere.health;
                    zones["relationship"] = genSphere.relationship;
                    zones["environment"] = genSphere.environment;
                    zones["vocation"] = genSphere.vocation;
                    zones["independence"] = genSphere.independence;
                    zones["self development"] = genSphere.selfdevelopment;
                    zones["brightness of life"] = genSphere.brightness;
                    zones["self_realization"] = genSphere.selfrealization;
                    zones["spirituality"] = genSphere.spirituality;
                }

                branches = db.Branches.Where(b => b.UserId == userId).ToList();

                db.SaveChanges();
            }
            FillPlanCanvas();
        }

        void FillPlanCanvas()
        {
            int userId = Convert.ToInt32(Application.Current.Properties["user"]);
                
                
            int cornerRadius = 5;
            double borderThickness = 1f;
            Brush backgroung = new SolidColorBrush(Color.FromRgb(255, 243, 204));
            Brush borderBrush = Brushes.Black;

            double canvasWidth = planCanvas.ActualWidth;
            double canvasHeight = planCanvas.ActualHeight;

            double blockWidthStep = canvasWidth / 18;
            double blockHeightStep = canvasHeight / 6;

            Border rectangle_1 = new Border();
            rectangle_1.Width = blockWidthStep * 13;
            rectangle_1.Height = blockHeightStep;
            planCanvas.Children.Add(rectangle_1);
            rectangle_1.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_1.Background = backgroung;
            rectangle_1.BorderBrush = borderBrush;
            rectangle_1.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_1, canvasWidth / 2 - rectangle_1.Width / 2);
            Canvas.SetTop(rectangle_1, 0);

            StackPanel panel_1 = new StackPanel();
            rectangle_1.Child = panel_1;

            TextBlock label_1 = new TextBlock();
            label_1.Width = blockWidthStep * 13;
            label_1.Height = blockHeightStep / 2;
            label_1.Text = "Daily-Plan";
            label_1.TextAlignment = TextAlignment.Center;
            label_1.TextWrapping = TextWrapping.WrapWithOverflow;
            label_1.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_1, canvasWidth / 2 - rectangle_1.Width / 2);
            Canvas.SetTop(label_1, 0);
            panel_1.Children.Add(label_1);

            TextBlock textbox_1 = new TextBlock();
            textbox_1.Width = blockWidthStep * 13;
            textbox_1.Height = blockHeightStep / 2;
            textbox_1.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_1.VerticalAlignment = VerticalAlignment.Center;
            textbox_1.TextAlignment = TextAlignment.Center;
            textbox_1.Text = plan.Week_Plan;
            panel_1.Children.Add(textbox_1);

            Border rectangle_2 = new Border();
            rectangle_2.Width = blockWidthStep * 14;
            rectangle_2.Height = blockHeightStep;
            planCanvas.Children.Add(rectangle_2);
            rectangle_2.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_2.Background = backgroung;
            rectangle_2.BorderBrush = borderBrush;
            rectangle_2.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_2, canvasWidth / 2 - rectangle_2.Width / 2);
            Canvas.SetTop(rectangle_2, blockHeightStep * 1);

            StackPanel panel_2 = new StackPanel();
            rectangle_2.Child = panel_2;

            TextBlock label_2 = new TextBlock();
            label_2.Width = blockWidthStep * 14;
            label_2.Height = blockHeightStep / 2;
            label_2.Text = "Short-term Plan";
            label_2.TextAlignment = TextAlignment.Center;
            label_2.TextWrapping = TextWrapping.WrapWithOverflow;
            label_2.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_2, canvasWidth / 2 - rectangle_2.Width / 2);
            Canvas.SetTop(label_2, blockHeightStep * 1);
            panel_2.Children.Add(label_2);

            TextBlock textbox_2 = new TextBlock();
            textbox_2.Width = blockWidthStep * 13;
            textbox_2.Height = blockHeightStep / 2;
            textbox_2.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_2.VerticalAlignment = VerticalAlignment.Center;
            textbox_2.TextAlignment = TextAlignment.Center;
            textbox_2.Text = plan.Shortterm_Plan;
            panel_2.Children.Add(textbox_2);

            Border rectangle_3 = new Border();
            rectangle_3.Width = blockWidthStep * 15;
            rectangle_3.Height = blockHeightStep;
            planCanvas.Children.Add(rectangle_3);
            rectangle_3.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_3.Background = backgroung;
            rectangle_3.BorderBrush = borderBrush;
            rectangle_3.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_3, canvasWidth / 2 - rectangle_3.Width / 2);
            Canvas.SetTop(rectangle_3, blockHeightStep * 2);

            StackPanel panel_3 = new StackPanel();
            rectangle_3.Child = panel_3;

            TextBlock label_3 = new TextBlock();
            label_3.Width = blockWidthStep * 15;
            label_3.Height = blockHeightStep / 2;
            label_3.Text = "Long-term Plan";
            label_3.TextAlignment = TextAlignment.Center;
            label_3.TextWrapping = TextWrapping.WrapWithOverflow;
            label_3.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_3, canvasWidth / 2 - rectangle_3.Width / 2);
            Canvas.SetTop(label_3, blockHeightStep * 2);
            panel_3.Children.Add(label_3);

            TextBlock textbox_3 = new TextBlock();
            textbox_3.Width = blockWidthStep * 13;
            textbox_3.Height = blockHeightStep / 2;
            textbox_3.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_3.VerticalAlignment = VerticalAlignment.Center;
            textbox_3.TextAlignment = TextAlignment.Center;
            textbox_3.Text = plan.Longterm_Plan;
            panel_3.Children.Add(textbox_3);

            Border rectangle_4 = new Border();
            rectangle_4.Width = blockWidthStep * 16;
            rectangle_4.Height = blockHeightStep;
            planCanvas.Children.Add(rectangle_4);
            rectangle_4.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_4.Background = backgroung;
            rectangle_4.BorderBrush = borderBrush;
            rectangle_4.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_4, canvasWidth / 2 - rectangle_4.Width / 2);
            Canvas.SetTop(rectangle_4, blockHeightStep * 3);

            StackPanel panel_4 = new StackPanel();
            rectangle_4.Child = panel_4;

            TextBlock label_4 = new TextBlock();
            label_4.Width = blockWidthStep * 16;
            label_4.Height = blockHeightStep / 2;
            label_4.Text = "General Plan";
            label_4.TextAlignment = TextAlignment.Center;
            label_4.TextWrapping = TextWrapping.WrapWithOverflow;
            label_4.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_4, canvasWidth / 2 - rectangle_4.Width / 2);
            Canvas.SetTop(label_4, blockHeightStep * 3);
            panel_4.Children.Add(label_4);

            TextBlock textbox_4 = new TextBlock();
            textbox_4.Width = blockWidthStep * 13;
            textbox_4.Height = blockHeightStep / 2;
            textbox_4.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_4.VerticalAlignment = VerticalAlignment.Center;
            textbox_4.TextAlignment = TextAlignment.Center;
            textbox_4.Text = plan.General_Plan;
            panel_4.Children.Add(textbox_4);

            Border rectangle_5 = new Border();
            rectangle_5.Width = blockWidthStep * 17;
            rectangle_5.Height = blockHeightStep;
            planCanvas.Children.Add(rectangle_5);
            rectangle_5.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_5.Background = backgroung;
            rectangle_5.BorderBrush = borderBrush;
            rectangle_5.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_5, canvasWidth / 2 - rectangle_5.Width / 2);
            Canvas.SetTop(rectangle_5, blockHeightStep * 4);

            StackPanel panel_5 = new StackPanel();
            rectangle_5.Child = panel_5;

            TextBlock label_5 = new TextBlock();
            label_5.Width = blockWidthStep * 17;
            label_5.Height = blockHeightStep / 2;
            label_5.Text = "Global Goals";
            label_5.TextAlignment = TextAlignment.Center;
            label_5.TextWrapping = TextWrapping.WrapWithOverflow;
            label_5.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_5, canvasWidth / 2 - rectangle_5.Width / 2);
            Canvas.SetTop(label_5, blockHeightStep * 4);
            panel_5.Children.Add(label_5);

            TextBlock textbox_5 = new TextBlock();
            textbox_5.Width = blockWidthStep * 13;
            textbox_5.Height = blockHeightStep / 2;
            textbox_5.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_5.VerticalAlignment = VerticalAlignment.Center;
            textbox_5.TextAlignment = TextAlignment.Center;
            textbox_5.Text = plan.Global_Marks;
            panel_5.Children.Add(textbox_5);

            Border rectangle_6 = new Border();
            rectangle_6.Width = blockWidthStep * 18;
            rectangle_6.Height = blockHeightStep;
            planCanvas.Children.Add(rectangle_6);
            rectangle_6.CornerRadius = new CornerRadius(cornerRadius);
            rectangle_6.Background = backgroung;
            rectangle_6.BorderBrush = borderBrush;
            rectangle_6.BorderThickness = new Thickness(borderThickness);
            Canvas.SetLeft(rectangle_6, canvasWidth / 2 - rectangle_6.Width / 2);
            Canvas.SetTop(rectangle_6, blockHeightStep * 5);

            StackPanel panel_6 = new StackPanel();
            rectangle_6.Child = panel_6;

            TextBlock label_6 = new TextBlock();
            label_6.Width = blockWidthStep * 18;
            label_6.Height = blockHeightStep / 2;
            label_6.Text = "The Main Vital Values";
            label_6.TextAlignment = TextAlignment.Center;
            label_6.TextWrapping = TextWrapping.WrapWithOverflow;
            label_6.FontWeight = FontWeights.Bold;
            Canvas.SetLeft(label_6, canvasWidth / 2 - rectangle_6.Width / 2);
            Canvas.SetTop(label_6, blockHeightStep * 5);
            panel_6.Children.Add(label_6);

            TextBlock textbox_6 = new TextBlock();
            textbox_6.Width = blockWidthStep * 13;
            textbox_6.Height = blockHeightStep / 2;
            textbox_6.HorizontalAlignment = HorizontalAlignment.Center;
            textbox_6.VerticalAlignment = VerticalAlignment.Center;
            textbox_6.TextAlignment = TextAlignment.Center;
            textbox_6.Text = plan.Life_Values;
            panel_6.Children.Add(textbox_6);
        }

        void DrawSphere()
        {
            const double radian = 0.0174532925f;
            Ellipse sphere = new Ellipse();
            double mx = Math.Min(spheresCanvas.ActualHeight, spheresCanvas.ActualWidth);
            sphere.Height = mx;
            sphere.Width = mx;
            sphere.Fill = new SolidColorBrush(Color.FromRgb(230, 234, 255));
            Canvas.SetLeft(sphere, spheresCanvas.ActualWidth / 2 - sphere.Width / 2);
            Canvas.SetTop(sphere, spheresCanvas.ActualHeight / 2 - sphere.Height / 2);
            spheresCanvas.Children.Add(sphere);

            Ellipse badSphere = new Ellipse();
            badSphere.Width = 3 * mx / 10;
            badSphere.Height = 3 * mx / 10;
            badSphere.Stroke = Brushes.Red;
            badSphere.Fill = Brushes.Transparent;
            Canvas.SetLeft(badSphere, spheresCanvas.ActualWidth / 2 - badSphere.Width / 2);
            Canvas.SetTop(badSphere, spheresCanvas.ActualHeight / 2 - badSphere.Height / 2);
            spheresCanvas.Children.Add(badSphere);

            Ellipse middleSphere = new Ellipse();
            middleSphere.Width = 5 * mx / 10;
            middleSphere.Height = 5 * mx / 10;
            middleSphere.Stroke = Brushes.Yellow;
            middleSphere.Fill = Brushes.Transparent;
            Canvas.SetLeft(middleSphere, spheresCanvas.ActualWidth / 2 - middleSphere.Width / 2);
            Canvas.SetTop(middleSphere, spheresCanvas.ActualHeight / 2 - middleSphere.Height / 2);
            spheresCanvas.Children.Add(middleSphere);

            Ellipse goodSphere = new Ellipse();
            goodSphere.Width = 7 * mx / 10;
            goodSphere.Height = 7 * mx / 10;
            goodSphere.Stroke = Brushes.Green;
            goodSphere.Fill = Brushes.Transparent;
            Canvas.SetLeft(goodSphere, spheresCanvas.ActualWidth / 2 - goodSphere.Width / 2);
            Canvas.SetTop(goodSphere, spheresCanvas.ActualHeight / 2 - goodSphere.Height / 2);
            spheresCanvas.Children.Add(goodSphere);


            double r = sphere.Width / 2;

            
            int i = 0;
            int count = zones.Keys.Count;


            foreach (string key in zones.Keys)
            {

                double radius = r * (Convert.ToSingle(zones[key]) / 10f);
                PathFigure zonePathFigure = new PathFigure();
                zonePathFigure.StartPoint = new Point(0, 0);

                LineSegment line1 = new LineSegment();
                line1.Point = new Point(Math.Sin(i * (360 * radian / count)) * radius, -Math.Cos(i * (360 * radian / count)) * radius);

                ArcSegment arc = new ArcSegment();
                arc.Point = new Point(Math.Sin((i + 1) * (360 * radian / count)) * radius, -Math.Cos((i + 1) * (360 * radian / count)) * radius);
                arc.Size = new Size(radius, radius);
                arc.RotationAngle = 0;
                arc.SweepDirection = SweepDirection.Clockwise;
                arc.IsLargeArc = false;

                LineSegment line2 = new LineSegment();
                line2.Point = new Point(0, 0);

                PathSegmentCollection zonePathSegmentCollection = new PathSegmentCollection();
                zonePathSegmentCollection.Add(line1);
                zonePathSegmentCollection.Add(arc);
                zonePathSegmentCollection.Add(line2);

                zonePathFigure.Segments = zonePathSegmentCollection;

                PathFigureCollection zonePathFigureCollection = new PathFigureCollection();
                zonePathFigureCollection.Add(zonePathFigure);

                PathGeometry zonePathGeometry = new PathGeometry();
                zonePathGeometry.Figures = zonePathFigureCollection;

                Path zone = new Path();
                zone.Data = zonePathGeometry;
                zone.Fill = new SolidColorBrush(Color.FromRgb(255, 243, 204));
                zone.Stroke = Brushes.DarkSlateGray;

                Label zoneName = new Label();
                zoneName.Width = 80;
                zoneName.Height = 25;
                zoneName.FontSize = 9.5;
                zoneName.Content = key;
                zoneName.HorizontalContentAlignment = HorizontalAlignment.Center;
                zoneName.VerticalContentAlignment = VerticalAlignment.Center;


                spheresCanvas.Children.Add(zone);
                spheresCanvas.Children.Add(zoneName);

                Canvas.SetLeft(zone, spheresCanvas.ActualWidth / 2);
                Canvas.SetTop(zone, spheresCanvas.ActualHeight / 2);
                
                Canvas.SetLeft(zoneName, spheresCanvas.ActualWidth / 2 + Math.Sin((i + 0.5f) * (360 * radian / count)) * (0.9f)*r - zoneName.Width/2);
                Canvas.SetTop(zoneName, spheresCanvas.ActualHeight / 2 - Math.Cos((i + 0.5f) * (360 * radian / count)) * (0.9f) * r - zoneName.Height / 2);

                i++;
            }
        }

        private void spheresCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            sphereCanvasBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC000");
        }

        private void spheresCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            sphereCanvasBorder.BorderBrush = Brushes.Transparent;
        }


        private void planCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            planCanvasBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFC000");
        }

        private void planCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            planCanvasBorder.BorderBrush = Brushes.Transparent;
        }

        private void branchesCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void branchesCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void branchesDetailsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void branchesDetailsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void doneCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            doneDetailsButton.Visibility = Visibility.Visible;
        }

        private void doneCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            doneDetailsButton.Visibility = Visibility.Hidden;
        }

        private void doneDetailsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            doneDetailsButton.Visibility = Visibility.Visible;
        }

        private void doneDetailsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            doneDetailsButton.Visibility = Visibility.Hidden;
        }

        private void spheresCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            spheresCanvas.Children.Clear();
            DrawSphere();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int userId = (int)Application.Current.Properties["user"];
            using (db = new ApplicationContext())
            {
                db.Branches.Add(new Branch(userId));
                db.SaveChanges();
            }
            using(db = new ApplicationContext())
                branches = db.Branches.Where(b => b.UserId == userId).ToList();
            RefreshBranchGrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            PageFrame.Content = new ideasBucketPage();
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Ideas Bucket";
            Application.Current.Properties["inMainPage"] = false;
        }

        private void planCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            planCanvas.Children.Clear();
            FillPlanCanvas();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            PageFrame.Content = new branchPage();
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "New Project";
            Application.Current.Properties["inMainPage"] = false;
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //RefreshBranchesUI();
            
        }

        void RefreshBranchGrid()
        {
            branchesGrid.Children.Clear();
            branchesGrid.ColumnDefinitions.Clear();
            branchesGrid.RowDefinitions.Clear();
            int count = branches.Count();
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
            int counter_x = 0;
            int counter_y = 0;
            foreach (Branch branch in branches)
            {
                if(branch.Closed != 1)
                {
                    Button but = new Button();
                    but.Width = buttonWidth - 4;
                    but.Height = buttonWidth - 4;
                    but.Margin = new Thickness(2, 2, 2, 2);
                    but.Background = Brushes.Transparent;
                    

                    StackPanel panel = new StackPanel();
                    but.Content = panel;

                    

                    TextBlock labelName = new TextBlock();
                    labelName.Text = branch.Name;
                    labelName.TextAlignment = TextAlignment.Center;
                    labelName.TextWrapping = TextWrapping.WrapWithOverflow;
                    labelName.FontSize = 12;
                    labelName.FontWeight = FontWeights.Bold;

                    Border borderName = new Border();
                    borderName.BorderThickness = new Thickness(0, 0, 0, 1.75);
                    borderName.BorderBrush = Brushes.DarkGray;
                    borderName.Child = labelName;

                    panel.Children.Add(borderName);

                    TextBlock labelMark = new TextBlock();
                    labelMark.Text = branch.Mark;
                    labelMark.TextWrapping = TextWrapping.WrapWithOverflow;
                    labelMark.TextAlignment = TextAlignment.Center;
                    labelMark.FontSize = 10;

                    Border borderMark = new Border();
                    borderMark.BorderThickness = new Thickness(0, 0, 0, 0.5);
                    borderMark.BorderBrush = Brushes.DarkGray;
                    borderMark.Child = labelMark;
                    panel.Children.Add(borderMark);

                    TextBlock labelMission = new TextBlock();
                    labelMission.Text = branch.Mission;
                    labelMission.TextWrapping = TextWrapping.WrapWithOverflow;
                    labelMission.TextAlignment = TextAlignment.Center;
                    labelMission.FontSize = 10;

                    Border borderMission = new Border();
                    borderMission.BorderThickness = new Thickness(0, 0, 0, 0.5);
                    borderMission.BorderBrush = Brushes.DarkGray;
                    borderMission.Child = labelMission;
                    panel.Children.Add(borderMission);

                    TextBlock labelProgress = new TextBlock();
                    labelProgress.Text = branch.Progress.ToString() + '%';
                    labelProgress.TextWrapping = TextWrapping.WrapWithOverflow;
                    labelProgress.TextAlignment = TextAlignment.Center;
                    labelProgress.FontSize = 10;
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

        private void branchesBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshBranchGrid();
        }

        private void branchesBorder_Loaded(object sender, RoutedEventArgs e)
        {
            //RefreshBranchGrid();
        }


        void branchButtonClick(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;

            int branch = Convert.ToInt32(but.Tag);
            Branch bran;
            using (db = new ApplicationContext())
                bran = db.Branches.Single(b => b.Id == branch);
            Application.Current.Properties["branch"] = branch;

            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Project: " + "\"" + bran.Name + "\"";
            PageFrame.Content = new branchPage();
        }

        private void doneDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Done List";
            PageFrame.Content = new DoneListPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Diary";
            PageFrame.Content = new DiaryPage();
        }

        private void spheresCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Life Spheres";
            PageFrame.Content = new SpheresPage();
        }

        private void planDetailsButton_Click(object sender, MouseButtonEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Longterm Plan";
            PageFrame.Content = new LongtermPlanPage();
        }

        private void projectArchive_Click(object sender, RoutedEventArgs e)
        {
            Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
            Application.Current.Properties["inMainPage"] = false;
            ((Label)Application.Current.Properties["pageNameLabel"]).Content = "Projects Archive";
            PageFrame.Content = new ProjectArchivePage();
        }
    }
}
