using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class Idea
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public Idea() { }

        public Idea(int UserId, string Tag)
        {
            this.UserId = UserId;
            this.Tag = Tag;
        }

        public Idea(int UserId, string Tag, string Name)
        {
            this.UserId = UserId;
            this.Tag = Tag;
            this.Name = Name;
        }
    }
}
