using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class healthSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int selffeeling { get; set; }
        public int looking { get; set; }
        public int energy { get; set; }
        public int meals { get; set; }
        public int sport { get; set; }
        public int sleeping { get; set; }


        public healthSphere() { }

        public healthSphere(int UserId)
        {
            this.UserId = UserId;
            this.selffeeling = 0;
            this.looking = 0;
            this.energy = 0;
            this.meals = 0;
            this.sport = 0;
            this.sleeping = 0;
        }
    }
}
