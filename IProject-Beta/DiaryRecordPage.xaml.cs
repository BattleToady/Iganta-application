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
    /// Interaction logic for DiaryRecordPage.xaml
    /// </summary>
    public partial class DiaryRecordPage : Page
    {
        ApplicationContext db;
        int UserId;
        string recordDate;
        bool wasCreated = true;
        public DiaryRecordPage()
        {
            InitializeComponent();
            UserId = (int)Application.Current.Properties["user"];
            recordDate = (string)Application.Current.Properties["diaryDate"];
            using (db = new ApplicationContext())
            {
                if (db.DiaryRecords.Where(record => record.UserId == UserId & record.Date == recordDate).Count() > 0)
                    RecordBox.Text = db.DiaryRecords.Single(record => record.UserId == UserId & record.Date == recordDate).Record;
                else
                    wasCreated = false;
            }
            //RecordBox.Text
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RecordBox.Text != String.Empty)
            {
                if (wasCreated)
                {
                    using(db = new ApplicationContext())
                    { 
                        DiaryRecord record = db.DiaryRecords.Single(rec => rec.UserId == UserId & rec.Date == recordDate);
                        record.Record = RecordBox.Text;
                        db.SaveChanges();
                    }
                }
                else
                {
                    using (db = new ApplicationContext())
                    {
                        DiaryRecord record = new DiaryRecord(UserId, recordDate);
                        record.Record = RecordBox.Text;
                        db.DiaryRecords.Add(record);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                if (wasCreated)
                {
                    using (db = new ApplicationContext())
                    {
                        DiaryRecord record = db.DiaryRecords.Single(rec => rec.UserId == UserId & rec.Date == recordDate);
                        db.DiaryRecords.Remove(record);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
