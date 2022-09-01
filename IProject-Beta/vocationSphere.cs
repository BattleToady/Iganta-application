using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class vocationSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int carier_business { get; set; }
        public int hobby { get; set; }

        public vocationSphere() { }

        public vocationSphere(int UserId)
        {
            this.UserId = UserId;
            this.carier_business = 0;
            this.hobby = 0;
        }
    }
}
