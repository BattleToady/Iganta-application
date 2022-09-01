using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class relationSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int talking { get; set; }
        public int friendship { get; set; }
        public int love { get; set; }
        public int family { get; set; }

        public relationSphere() { }

        public relationSphere(int UserId)
        {
            this.UserId = UserId;
            this.talking = 0;
            this.friendship = 0;
            this.love = 0;
            this.family = 0;
        }
    }
}
