using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class Branch
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }

        public string Mark { get; set; }

        public string Mission { get; set; }

        public float Progress { get; set; }
        public int Closed { get; set; }


        public Branch() { }

        public Branch(int UserId, string name)
        {
            this.Name = name;
            this.UserId = UserId;
            this.Closed = 0;
        }

        public Branch(int UserId)
        {
            this.UserId = UserId;
            this.Name = "New Project";
            this.Closed = 0;
        }
    }
}
