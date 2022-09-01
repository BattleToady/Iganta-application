using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class LongtermPlan
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Week_Plan { get; set; }
        public string Shortterm_Plan { get; set; }
        public string Longterm_Plan { get; set; }
        public string General_Plan { get; set; }
        public string Global_Marks { get; set; }
        public string Life_Values { get; set; }

        public LongtermPlan() { }

        public LongtermPlan(int UserId)
        {
            this.UserId = UserId;
            this.Week_Plan = "...";
            this.Shortterm_Plan = "...";
            this.Longterm_Plan = "...";
            this.General_Plan = "...";
            this.Global_Marks = "...";
            this.Life_Values = "...";
        }
    }
}
