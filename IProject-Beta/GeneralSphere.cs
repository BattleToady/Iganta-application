using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class GeneralSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int health { get; set; }
        public int relationship { get; set; }
        public int environment { get; set; }
        public int vocation { get; set; }
        public int independence { get; set; }
        public int selfdevelopment { get; set; }
        public int brightness { get; set; }
        public int selfrealization { get; set; }
        public int spirituality { get; set; }


        public GeneralSphere() { }

        public GeneralSphere(int UserId)
        {
            this.UserId = UserId;
            this.health = 0;
            this.relationship = 0;
            this.environment = 0;
            this.vocation = 0;
            this.independence = 0;
            this.selfdevelopment = 0;
            this.brightness = 0;
            this.selfrealization = 0;
            this.spirituality = 0;
        }
    }
}
