using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class selfdevelopmentSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int learning { get; set; }
        public int work { get; set; }
        public int growth { get; set; }

        public selfdevelopmentSphere() { }

        public selfdevelopmentSphere(int UserId)
        {
            this.UserId = UserId;
            this.learning = 0;
            this.work = 0;
            this.growth = 0;
        }
    }
}
