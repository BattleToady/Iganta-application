using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class environmentSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int children { get; set; }
        public int relatives { get; set; }
        public int colegs { get; set; }
        public int friends { get; set; }
        public int neigbours { get; set; }
        public int acquintances { get; set; }

        public environmentSphere() { }

        public environmentSphere(int UserId)
        {
            this.UserId = UserId;
            this.children = 0;
            this.relatives = 0;
            this.colegs = 0;
            this.friends = 0;
            this.neigbours = 0;
            this.acquintances = 0;
        }
    }
}
