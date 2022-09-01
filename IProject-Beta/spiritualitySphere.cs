using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class spiritualitySphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int outlook { get; set; }
        public int art { get; set; }
        public int lifeMeaning { get; set; }

        public spiritualitySphere() { }

        public spiritualitySphere(int UserId)
        {
            this.UserId = UserId;
            this.outlook = 0;
            this.art = 0;
            this.lifeMeaning = 0;
        }
    }
}
