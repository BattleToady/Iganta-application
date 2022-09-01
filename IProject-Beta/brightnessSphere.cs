using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class brightnessSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int rest { get; set; }
        public int travels { get; set; }
        public int impression { get; set; }

        public brightnessSphere() { }

        public brightnessSphere(int UserId)
        {
            this.UserId = UserId;
            this.rest = 0;
            this.travels = 0;
            this.impression = 0;
        }
    }
}
