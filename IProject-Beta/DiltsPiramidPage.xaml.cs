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
    /// Interaction logic for DiltsPiramidPage.xaml
    /// </summary>
    public partial class DiltsPiramidPage : Page
    {
        ApplicationContext db;
        DiltsPiramid piramid;
        int userId;
        int projectId;
        public DiltsPiramidPage()
        {
            InitializeComponent();

            using(db = new ApplicationContext())
            { 
                userId = Convert.ToInt32(Application.Current.Properties["user"]);
                projectId = (int)Application.Current.Properties["branch"];
                piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            piramidCanvas.Children.Clear();
            RefreshPiramid();
        }

        void RefreshPiramid()
        {
            using (db = new ApplicationContext())
            {
                int userId = Convert.ToInt32(Application.Current.Properties["user"]);
                int projectId = (int)Application.Current.Properties["branch"];

                Branch branch = db.Branches.Where(b => b.Id == projectId).First();

                projectNameLabel.Content = branch.Name + " piramid";

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
                label_1.Text = "Purpose(Why am I here?)";
                label_1.TextAlignment = TextAlignment.Center;
                label_1.TextWrapping = TextWrapping.WrapWithOverflow;
                label_1.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(label_1, canvasWidth / 2 - rectangle_1.Width / 2);
                Canvas.SetTop(label_1, blockHeightStep);
                panel_1.Children.Add(label_1);

                TextBox textbox_1 = new TextBox();
                textbox_1.Width = blockWidthStep * 13;
                textbox_1.Height = blockHeightStep / 2;
                textbox_1.HorizontalAlignment = HorizontalAlignment.Center;
                textbox_1.VerticalAlignment = VerticalAlignment.Center;
                textbox_1.HorizontalContentAlignment = HorizontalAlignment.Center;
                textbox_1.VerticalContentAlignment = VerticalAlignment.Center;
                textbox_1.Text = piramid.People;
                textbox_1.LostFocus += textBox_1_LostFocus;
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
                label_2.Text = "Indentity/Role(Who am I?)";
                label_2.TextAlignment = TextAlignment.Center;
                label_2.TextWrapping = TextWrapping.WrapWithOverflow;
                label_2.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(label_2, canvasWidth / 2 - rectangle_2.Width / 2);
                Canvas.SetTop(label_2, blockHeightStep * 2);
                panel_2.Children.Add(label_2);

                TextBox textbox_2 = new TextBox();
                textbox_2.Width = blockWidthStep * 13;
                textbox_2.Height = blockHeightStep / 2;
                textbox_2.HorizontalAlignment = HorizontalAlignment.Center;
                textbox_2.VerticalAlignment = VerticalAlignment.Center;
                textbox_2.HorizontalContentAlignment = HorizontalAlignment.Center;
                textbox_2.VerticalContentAlignment = VerticalAlignment.Center;
                textbox_2.LostFocus += textBox_2_LostFocus;
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
                label_3.Text = "Beliefs and Values(What matters to me?)";
                label_3.TextAlignment = TextAlignment.Center;
                label_3.TextWrapping = TextWrapping.WrapWithOverflow;
                label_3.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(label_3, canvasWidth / 2 - rectangle_3.Width / 2);
                Canvas.SetTop(label_3, blockHeightStep * 3);
                panel_3.Children.Add(label_3);

                TextBox textbox_3 = new TextBox();
                textbox_3.Width = blockWidthStep * 13;
                textbox_3.Height = blockHeightStep / 2;
                textbox_3.HorizontalAlignment = HorizontalAlignment.Center;
                textbox_3.VerticalAlignment = VerticalAlignment.Center;
                textbox_3.HorizontalContentAlignment = HorizontalAlignment.Center;
                textbox_3.VerticalContentAlignment = VerticalAlignment.Center;
                textbox_3.Text = piramid.Values;
                textbox_3.LostFocus += textBox_3_LostFocus;
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
                label_4.Text = "Capabilities(What skills do I have?)";
                label_4.TextAlignment = TextAlignment.Center;
                label_4.TextWrapping = TextWrapping.WrapWithOverflow;
                label_4.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(label_4, canvasWidth / 2 - rectangle_4.Width / 2);
                Canvas.SetTop(label_4, blockHeightStep * 4);
                panel_4.Children.Add(label_4);

                TextBox textbox_4 = new TextBox();
                textbox_4.Width = blockWidthStep * 13;
                textbox_4.Height = blockHeightStep / 2;
                textbox_4.HorizontalAlignment = HorizontalAlignment.Center;
                textbox_4.VerticalAlignment = VerticalAlignment.Center;
                textbox_4.HorizontalContentAlignment = HorizontalAlignment.Center;
                textbox_4.VerticalContentAlignment = VerticalAlignment.Center;
                textbox_4.Text = piramid.Skills;
                textbox_4.LostFocus += textBox_4_LostFocus;
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
                label_5.Text = "Behaviour(What do I do?)";
                label_5.TextAlignment = TextAlignment.Center;
                label_5.TextWrapping = TextWrapping.WrapWithOverflow;
                label_5.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(label_5, canvasWidth / 2 - rectangle_5.Width / 2);
                Canvas.SetTop(label_5, blockHeightStep * 5);
                panel_5.Children.Add(label_5);

                TextBox textbox_5 = new TextBox();
                textbox_5.Width = blockWidthStep * 13;
                textbox_5.Height = blockHeightStep / 2;
                textbox_5.HorizontalAlignment = HorizontalAlignment.Center;
                textbox_5.VerticalAlignment = VerticalAlignment.Center;
                textbox_5.HorizontalContentAlignment = HorizontalAlignment.Center;
                textbox_5.VerticalContentAlignment = VerticalAlignment.Center;
                textbox_5.Text = piramid.Behavior;
                textbox_5.LostFocus += textBox_5_LostFocus;
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
                label_6.Text = "Environment(Where am I? Who/What is around me?)";
                label_6.TextAlignment = TextAlignment.Center;
                label_6.TextWrapping = TextWrapping.WrapWithOverflow;
                label_6.FontWeight = FontWeights.Bold;
                Canvas.SetLeft(label_6, canvasWidth / 2 - rectangle_6.Width / 2);
                Canvas.SetTop(label_6, blockHeightStep * 6);
                panel_6.Children.Add(label_6);

                TextBox textbox_6 = new TextBox();
                textbox_6.Width = blockWidthStep * 13;
                textbox_6.Height = blockHeightStep / 2;
                textbox_6.HorizontalAlignment = HorizontalAlignment.Center;
                textbox_6.VerticalAlignment = VerticalAlignment.Center;
                textbox_6.HorizontalContentAlignment = HorizontalAlignment.Center;
                textbox_6.VerticalContentAlignment = VerticalAlignment.Center;
                textbox_6.Text = piramid.Environment;
                textbox_6.LostFocus += textBox_6_LostFocus;
                panel_6.Children.Add(textbox_6);
            }
        }

        void textBox_1_LostFocus(object sender, RoutedEventArgs args)
        {
            using (db = new ApplicationContext())
            {
                DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                piramid.People = ((TextBox)sender).Text;
                piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                db.SaveChanges();
            }
        }

        void textBox_2_LostFocus(object sender, RoutedEventArgs args)
        {
            using (db = new ApplicationContext())
            {
                DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                piramid.Role = ((TextBox)sender).Text;
                piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                db.SaveChanges();
            }
        }

        void textBox_3_LostFocus(object sender, RoutedEventArgs args)
        {
            using (db = new ApplicationContext())
            {
                DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                piramid.Values = ((TextBox)sender).Text;
                piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                db.SaveChanges();
            }
        }

        void textBox_4_LostFocus(object sender, RoutedEventArgs args)
        {
            using (db = new ApplicationContext())
            {
                DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                piramid.Skills = ((TextBox)sender).Text;
                piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                db.SaveChanges();
            }
        }

        void textBox_5_LostFocus(object sender, RoutedEventArgs args)
        {
            using (db = new ApplicationContext())
            {
                DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                piramid.Behavior = ((TextBox)sender).Text;
                piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                db.SaveChanges();
            }
        }

        void textBox_6_LostFocus(object sender, RoutedEventArgs args)
        {
            using (db = new ApplicationContext())
            {
                DiltsPiramid piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                piramid.Environment = ((TextBox)sender).Text;
                piramid = db.DiltsPiramids.Where(p => p.UserId == userId && p.ProjectId == projectId).First();
                db.SaveChanges();
            }
        }
    }
}
