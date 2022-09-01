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
    /// Interaction logic for SpheresPage.xaml
    /// </summary>
    public partial class SpheresPage : Page
    {
        ApplicationContext db;

        Dictionary<string, int> generalZones;
        Dictionary<string, int> healthZones;
        Dictionary<string, int> relationshipZones;
        Dictionary<string, int> environmentZones;
        Dictionary<string, int> vocationZones;
        Dictionary<string, int> independenceZones;
        Dictionary<string, int> selfdevelopmentZones;
        Dictionary<string, int> brightnessZones;
        Dictionary<string, int> spiritualityZones;

        Dictionary<string, int> selectedZones;
        Canvas selectedCanvas;
        public SpheresPage()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            generalZones = new Dictionary<string, int>();
            healthZones = new Dictionary<string, int>();
            relationshipZones = new Dictionary<string, int>();
            environmentZones = new Dictionary<string, int>();
            vocationZones = new Dictionary<string, int>();
            independenceZones = new Dictionary<string, int>();
            selfdevelopmentZones = new Dictionary<string, int>();
            brightnessZones = new Dictionary<string, int>();
            spiritualityZones = new Dictionary<string, int>();

            using(db = new ApplicationContext())
            {
                int userId = (int)Application.Current.Properties["user"];
                if (!db.GeneralSpheres.Where(s => s.UserId == userId).Any())
                {
                    db.GeneralSpheres.Add(new GeneralSphere(userId));
                    db.healthSpheres.Add(new healthSphere(userId));
                    db.relationSpheres.Add(new relationSphere(userId));
                    db.environmentSpheres.Add(new environmentSphere(userId));
                    db.vocationSpheres.Add(new vocationSphere(userId));
                    db.independenceSpheres.Add(new independenceSphere(userId));
                    db.selfdevelopmentSpheres.Add(new selfdevelopmentSphere(userId));
                    db.brightnessSpheres.Add(new brightnessSphere(userId));
                    db.spiritualitySpheres.Add(new spiritualitySphere(userId));
                    db.SaveChanges();
                }
                GeneralSphere genSphere = db.GeneralSpheres.Single(s => s.UserId == userId);
                generalZones["health"] = genSphere.health;
                generalZones["relationship"] = genSphere.relationship;
                generalZones["environment"] = genSphere.environment;
                generalZones["vocation"] = genSphere.vocation;
                generalZones["independence"] = genSphere.independence;
                generalZones["self development"] = genSphere.selfdevelopment;
                generalZones["brightness of life"] = genSphere.brightness;
                generalZones["self development"] = genSphere.selfrealization;
                generalZones["spirituality"] = genSphere.spirituality;

                healthSphere hSphere = db.healthSpheres.Single(s => s.UserId == userId);
                healthZones["self-feeling"] = hSphere.selffeeling;
                healthZones["appearance"] = hSphere.looking;
                healthZones["energy"] = hSphere.energy;
                healthZones["ration"] = hSphere.meals;
                healthZones["sport"] = hSphere.sport;
                healthZones["sleep"] = hSphere.sleeping;

                relationSphere rSphere = db.relationSpheres.Single(s => s.UserId == userId);
                relationshipZones["communication"] = rSphere.talking;
                relationshipZones["friends"] = rSphere.friendship;
                relationshipZones["beloved"] = rSphere.love;
                relationshipZones["family"] = rSphere.family;

                environmentSphere eSphere = db.environmentSpheres.Single(s => s.UserId == userId);
                environmentZones["children"] = eSphere.children;
                environmentZones["relatives"] = eSphere.relatives;
                environmentZones["colleagues"] = eSphere.colegs;
                environmentZones["friends"] = eSphere.friends;
                environmentZones["neigbours"] = eSphere.neigbours;
                environmentZones["acquaintances"] = eSphere.acquintances;

                vocationSphere vSphere = db.vocationSpheres.Single(s => s.UserId == userId);
                vocationZones["carier/business"] = vSphere.carier_business;
                vocationZones["hobby"] = vSphere.hobby;

                independenceSphere iSphere = db.independenceSpheres.Single(s => s.UserId == userId);
                independenceZones["incomes"] = iSphere.incomes;
                independenceZones["expenses"] = iSphere.expenses;
                independenceZones["spending possibility"] = iSphere.spending_possibility;

                selfdevelopmentSphere sSphere = db.selfdevelopmentSpheres.Single(s => s.UserId == userId);
                selfdevelopmentZones["learning a new"] = sSphere.learning;
                selfdevelopmentZones["achievement of goals"] = sSphere.work;
                selfdevelopmentZones["personal growth"] = sSphere.growth;

                brightnessSphere bSphere = db.brightnessSpheres.Single(s => s.UserId == userId);
                brightnessZones["refreshment"] = bSphere.rest;
                brightnessZones["travels"] = bSphere.travels;
                brightnessZones["impression"] = bSphere.impression;

                spiritualitySphere spSphere = db.spiritualitySpheres.Single(s => s.UserId == userId);
                spiritualityZones["outlook"] = spSphere.outlook;
                spiritualityZones["art"] = spSphere.art;
                spiritualityZones["meaning of life"] = spSphere.lifeMeaning;
            }
        }

        void SaveData()
        {
            using (db = new ApplicationContext())
            {
                int userId = (int)Application.Current.Properties["user"];

                GeneralSphere genSphere = db.GeneralSpheres.Single(s => s.UserId == userId);
                genSphere.health = generalZones["health"];
                genSphere.relationship = generalZones["relationship"];
                genSphere.environment = generalZones["environment"];
                genSphere.vocation = generalZones["vocation"];
                genSphere.independence = generalZones["independence"];
                genSphere.selfdevelopment = generalZones["self development"];
                genSphere.brightness = generalZones["brightness of life"];
                genSphere.selfrealization = generalZones["self development"];
                genSphere.spirituality = generalZones["spirituality"];

                healthSphere hSphere = db.healthSpheres.Single(s => s.UserId == userId);
                hSphere.selffeeling = healthZones["self-feeling"];
                hSphere.looking = healthZones["appearance"];
                hSphere.energy = healthZones["energy"];
                hSphere.meals = healthZones["ration"];
                hSphere.sport = healthZones["sport"];
                hSphere.sleeping = healthZones["sleep"];

                relationSphere rSphere = db.relationSpheres.Single(s => s.UserId == userId);
                rSphere.talking = relationshipZones["communication"];
                rSphere.friendship = relationshipZones["friends"];
                rSphere.love = relationshipZones["beloved"];
                rSphere.family = relationshipZones["family"];

                environmentSphere eSphere = db.environmentSpheres.Single(s => s.UserId == userId);
                eSphere.children = environmentZones["children"];
                eSphere.relatives = environmentZones["relatives"];
                eSphere.colegs = environmentZones["colleagues"];
                eSphere.friends = environmentZones["friends"];
                eSphere.neigbours = environmentZones["neigbours"];
                eSphere.acquintances = environmentZones["acquaintances"];

                vocationSphere vSphere = db.vocationSpheres.Single(s => s.UserId == userId);
                vSphere.carier_business = vocationZones["carier/business"];
                vSphere.hobby = vocationZones["hobby"];

                independenceSphere iSphere = db.independenceSpheres.Single(s => s.UserId == userId);
                iSphere.incomes = independenceZones["incomes"];
                iSphere.expenses = independenceZones["expenses"];
                iSphere.spending_possibility = independenceZones["spending possibility"];

                selfdevelopmentSphere sSphere = db.selfdevelopmentSpheres.Single(s => s.UserId == userId);
                sSphere.learning = selfdevelopmentZones["learning a new"];
                sSphere.work = selfdevelopmentZones["achievement of goals"];
                sSphere.growth = selfdevelopmentZones["personal growth"];

                brightnessSphere bSphere = db.brightnessSpheres.Single(s => s.UserId == userId);
                bSphere.rest = brightnessZones["refreshment"];
                bSphere.travels = brightnessZones["travels"];
                bSphere.impression = brightnessZones["impression"];

                spiritualitySphere spSphere = db.spiritualitySpheres.Single(s => s.UserId == userId);
                spSphere.outlook = spiritualityZones["outlook"];
                spSphere.art = spiritualityZones["art"];
                spSphere.lifeMeaning = spiritualityZones["meaning of life"];

                db.SaveChanges();
            }
        }

        void RefreshAllSpheres()
        {
            RedrawSphere(sphereCanvas, generalZones);
            RedrawSphere(healthSphereCanvas, healthZones);
            RedrawSphere(relationshipSphereCanvas, relationshipZones);
            RedrawSphere(environmentSphereCanvas, environmentZones);
            RedrawSphere(vocationSphereCanvas, vocationZones);
            RedrawSphere(independenceSphereCanvas, independenceZones);
            RedrawSphere(selfdevelopmentSphereCanvas, selfdevelopmentZones);
            RedrawSphere(brightnesSphereCanvas, brightnessZones);
            RedrawSphere(spiritualitySphereCanvas, spiritualityZones);
        }

        void RedrawSphere(Canvas sphrCanvas, Dictionary<string, int> zones)
        {
            sphrCanvas.Children.Clear();

            const double radian = 0.0174532925f;
            Ellipse sphere = new Ellipse();
            double mx = Math.Min(sphrCanvas.ActualHeight, sphrCanvas.ActualWidth);
            sphere.Height = mx;
            sphere.Width = mx;
            sphere.Fill = new SolidColorBrush(Color.FromRgb(230, 234, 255));
            Canvas.SetLeft(sphere, sphrCanvas.ActualWidth / 2 - sphere.Width / 2);
            Canvas.SetTop(sphere, sphrCanvas.ActualHeight / 2 - sphere.Height / 2);
            sphrCanvas.Children.Add(sphere);

            Ellipse badSphere = new Ellipse();
            badSphere.Width = 3 * mx / 10;
            badSphere.Height = 3 * mx / 10;
            badSphere.Stroke = Brushes.Red;
            badSphere.Fill = Brushes.Transparent;
            Canvas.SetLeft(badSphere, sphrCanvas.ActualWidth / 2 - badSphere.Width / 2);
            Canvas.SetTop(badSphere, sphrCanvas.ActualHeight / 2 - badSphere.Height / 2);
            sphrCanvas.Children.Add(badSphere);

            Ellipse middleSphere = new Ellipse();
            middleSphere.Width = 5 * mx / 10;
            middleSphere.Height = 5 * mx / 10;
            middleSphere.Stroke = Brushes.Yellow;
            middleSphere.Fill = Brushes.Transparent;
            Canvas.SetLeft(middleSphere, sphrCanvas.ActualWidth / 2 - middleSphere.Width / 2);
            Canvas.SetTop(middleSphere, sphrCanvas.ActualHeight / 2 - middleSphere.Height / 2);
            sphrCanvas.Children.Add(middleSphere);

            Ellipse goodSphere = new Ellipse();
            goodSphere.Width = 7 * mx / 10;
            goodSphere.Height = 7 * mx / 10;
            goodSphere.Stroke = Brushes.Green;
            goodSphere.Fill = Brushes.Transparent;
            Canvas.SetLeft(goodSphere, sphrCanvas.ActualWidth / 2 - goodSphere.Width / 2);
            Canvas.SetTop(goodSphere, sphrCanvas.ActualHeight / 2 - goodSphere.Height / 2);
            sphrCanvas.Children.Add(goodSphere);


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

                TextBlock zoneName = new TextBlock();
                zoneName.Width = 80;
                zoneName.Height = 30;
                zoneName.FontSize = 11;
                zoneName.FontStyle = FontStyles.Italic;
                zoneName.Text = key;
                zoneName.TextAlignment = TextAlignment.Center;
                zoneName.TextWrapping = TextWrapping.WrapWithOverflow;


                sphrCanvas.Children.Add(zone);
                sphrCanvas.Children.Add(zoneName);

                Canvas.SetLeft(zone, sphrCanvas.ActualWidth / 2);
                Canvas.SetTop(zone, sphrCanvas.ActualHeight / 2);

                Canvas.SetLeft(zoneName, sphrCanvas.ActualWidth / 2 + Math.Sin((i + 0.5f) * (360 * radian / count)) * (0.8f) * r - zoneName.Width / 2);
                Canvas.SetTop(zoneName, sphrCanvas.ActualHeight / 2 - Math.Cos((i + 0.5f) * (360 * radian / count)) * (0.8f) * r - zoneName.Height / 2);

                i++;
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAllSpheres();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshAllSpheres();
        }

        private void sphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = generalZones;
            selectedCanvas = sphereCanvas;
            clickedSphere(sphereCanvas, generalZones);
        }

        void clickedSphere(Canvas sphrCanvas, Dictionary<string, int> zones)
        {
            sphereEditMenu.Children.Clear();

            Label sphereName = new Label();
            string sphere_name;
            switch(sphrCanvas.Name)
            {
                case "sphereCanvas":
                    sphere_name = "Spheres of Life";
                    break;

                case "healthSphereCanvas":
                    sphere_name = "Health Sphere";
                    break;

                case "relationshipSphereCanvas":
                    sphere_name = "Relationship Sphere";
                    break;

                case "environmentSphereCanvas":
                    sphere_name = "Environment Sphere";
                    break;

                case "vocationSphereCanvas":
                    sphere_name = "Vocation Sphere";
                    break;

                case "independenceSphereCanvas":
                    sphere_name = "Independence Sphere";
                    break;

                case "selfdevelopmentSphereCanvas":
                    sphere_name = "Self-Development Sphere";
                    break;

                case "brightnesSphereCanvas":
                    sphere_name = "Brightnes Sphere";
                    break;

                case "spiritualitySphereCanvas":
                    sphere_name = "Spirituality Sphere";
                    break;

                default:
                    sphere_name = "Sphere";
                    break;
            }
            sphereName.Content = sphere_name;
            sphereName.HorizontalContentAlignment = HorizontalAlignment.Center;
            sphereName.VerticalContentAlignment = VerticalAlignment.Center;
            sphereName.FontWeight = FontWeights.Bold;
            sphereName.FontSize = 20;
            sphereEditMenu.Children.Add(sphereName);
            foreach (string zone in zones.Keys)
            {
                StackPanel element = new StackPanel();
                element.Margin = new Thickness(0, 10, 0, 0);
                element.Orientation = Orientation.Horizontal;

                Label name = new Label();
                name.Content = zone;
                element.Children.Add(name);

                Slider slider = new Slider();
                slider.Width = 150;
                slider.Interval = 1;
                slider.Minimum = 0;
                slider.Maximum = 10;
                slider.Value = zones[zone];
                slider.IsSnapToTickEnabled = true;
                slider.TickFrequency = 1;
                slider.ValueChanged += SliderValueChanged;
                slider.LostFocus += SliderLostFocus;
                slider.Tag = zone;
                element.Children.Add(slider);

                Label num = new Label();
                Binding sliderBinder = new Binding();
                sliderBinder.Source = slider;
                sliderBinder.Path = new PropertyPath("Value");
                num.SetBinding(Label.ContentProperty, sliderBinder);
                element.Children.Add(num);

                sphereEditMenu.Children.Add(element);
            }
        }

        void SliderValueChanged(object sender, RoutedEventArgs e)
        {
            Slider slider = (Slider)sender;
            selectedZones[slider.Tag.ToString()] = Convert.ToInt32(slider.Value);
            RefreshAllSpheres();
        }

        void SliderLostFocus(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void sphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            sphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void sphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            sphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void healthSphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            healthSphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void healthSphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            healthSphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void relationshipSphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            relationshipSphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void relationshipSphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            relationshipSphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void environmentSphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            environmentSphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void environmentSphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            environmentSphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void vocationSphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            vocationSphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void vocationSphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            vocationSphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void brightnesSphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            brightnesSphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void brightnesSphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            brightnesSphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void spiritualitySphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            spiritualitySphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void spiritualitySphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            spiritualitySphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void selfdevelopmentSphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            selfdevelopmentSphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void selfdevelopmentSphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            selfdevelopmentSphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void independenceSphereCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            independenceSphereBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffbf00");
        }

        private void independenceSphereCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            independenceSphereBorder.BorderBrush = Brushes.DarkSlateGray;
        }

        private void healthSphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = healthZones;
            selectedCanvas = healthSphereCanvas;
            clickedSphere(healthSphereCanvas, healthZones);
        }

        private void relationshipSphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = relationshipZones;
            selectedCanvas = relationshipSphereCanvas;
            clickedSphere(relationshipSphereCanvas, relationshipZones);
        }

        private void environmentSphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = environmentZones;
            selectedCanvas = environmentSphereCanvas;
            clickedSphere(environmentSphereCanvas, environmentZones);
        }

        private void vocationSphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = vocationZones;
            selectedCanvas = vocationSphereCanvas;
            clickedSphere(vocationSphereCanvas, vocationZones);
        }

        private void independenceSphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = independenceZones;
            selectedCanvas = independenceSphereCanvas;
            clickedSphere(independenceSphereCanvas, independenceZones);
        }

        private void selfdevelopmentSphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = selfdevelopmentZones;
            selectedCanvas = selfdevelopmentSphereCanvas;
            clickedSphere(selfdevelopmentSphereCanvas, selfdevelopmentZones);
        }

        private void brightnesSphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = brightnessZones;
            selectedCanvas = brightnesSphereCanvas;
            clickedSphere(brightnesSphereCanvas, brightnessZones);
        }

        private void spiritualitySphereCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedZones = spiritualityZones;
            selectedCanvas = spiritualitySphereCanvas;
            clickedSphere(spiritualitySphereCanvas, spiritualityZones);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to refresh all spheres?", "Question", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                generalZones["health"] = 0;
                generalZones["relationship"] = 0;
                generalZones["environment"] = 0;
                generalZones["vocation"] = 0;
                generalZones["independence"] = 0;
                generalZones["self development"] = 0;
                generalZones["brightness of life"] = 0;
                generalZones["self development"] = 0;
                generalZones["spirituality"] = 0;

                healthZones["self-feeling"] = 0;
                healthZones["appearance"] = 0;
                healthZones["energy"] = 0;
                healthZones["ration"] = 0;
                healthZones["sport"] = 0;
                healthZones["sleep"] = 0;

                relationshipZones["communication"] = 0;
                relationshipZones["friends"] = 0;
                relationshipZones["beloved"] = 0;
                relationshipZones["family"] = 0;

                environmentZones["children"] = 0;
                environmentZones["relatives"] = 0;
                environmentZones["colleagues"] = 0;
                environmentZones["friends"] = 0;
                environmentZones["neigbours"] = 0;
                environmentZones["acquaintances"] = 0;

                vocationZones["carier/business"] = 0;
                vocationZones["hobby"] = 0;

                independenceZones["incomes"] = 0;
                independenceZones["expenses"] = 0;
                independenceZones["spending possibility"] = 0;

                selfdevelopmentZones["learning a new"] = 0;
                selfdevelopmentZones["achievement of goals"] = 0;
                selfdevelopmentZones["personal growth"] = 0;

                brightnessZones["refreshment"] = 0;
                brightnessZones["travels"] = 0;
                brightnessZones["impression"] = 0;

                spiritualityZones["outlook"] = 0;
                spiritualityZones["art"] = 0;
                spiritualityZones["meaning of life"] = 0;

                SaveData();
                RefreshAllSpheres();
            }
        }
    }
}
