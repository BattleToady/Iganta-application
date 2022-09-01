using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for DiaryPage.xaml
    /// </summary>
    public partial class DiaryPage : Page
    {
        ApplicationContext db;
		int UserId;
		private List<DateTime> significantDates;
		public DiaryPage()
        {
            InitializeComponent();
			significantDates = new List<DateTime>();

			UserId = (int)Application.Current.Properties["user"];
			using (db = new ApplicationContext())
            {
				List<DiaryRecord> records = db.DiaryRecords.Where(r => r.UserId == UserId).ToList();

				foreach (DiaryRecord record in records)
					significantDates.Add(DateTime.Parse(record.Date));
            }
		}
		private void calendarButton_Loaded(object sender, EventArgs e)
		{
			CalendarDayButton button = (CalendarDayButton)sender;
			DateTime date = (DateTime)button.DataContext;
			HighlightDay(button, date);
			button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
		}

		private void HighlightDay(CalendarDayButton button, DateTime date)
		{
			if (significantDates.Contains(date))
				button.Background = Brushes.LightBlue;
			else
				button.Background = Brushes.White;
		}

		private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			CalendarDayButton button = (CalendarDayButton)sender;
			DateTime date = (DateTime)button.DataContext;
			HighlightDay(button, date);
		}

        private void recordPicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
			using(db = new ApplicationContext())
            {
				DateTime date = recordPicker.SelectedDates[0];
				string dateString = date.ToShortDateString();
				if (db.DiaryRecords.Where(record => record.UserId == UserId & record.Date == dateString).ToList().Count > 0)
				{
					previewBox.Text = db.DiaryRecords.Single(record => record.UserId == UserId & record.Date == dateString).Record;
				}
				else
					previewBox.Text = "";
				

			}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			Frame PageFrame = (Frame)Application.Current.Properties["PageFrame"];
			Application.Current.Properties["thirdPage"] = true;
			Application.Current.Properties["preLastPage"] = this;
			Application.Current.Properties["diaryDate"] = recordPicker.SelectedDates[0].ToShortDateString();
			PageFrame.Content = new DiaryRecordPage();
		}
    }
}
