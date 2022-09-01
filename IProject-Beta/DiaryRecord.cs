using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class DiaryRecord
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Record { get; set; }

        public string Date { get; set; }

        public DiaryRecord() { }

        public DiaryRecord(int UserId, string Date)
        {
            this.UserId = UserId;
            this.Date = Date;
        }
    }
}
