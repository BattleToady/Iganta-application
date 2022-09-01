using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    internal class DiltsPiramid
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        
        public int ProjectId { get; set; }

        public string People { get; set; }

        public string Role { get; set; }

        public string Values { get; set; }

        public string Skills { get; set; }

        public string Behavior { get; set; }

        public string Environment { get; set; }

        public DiltsPiramid() { }

        public DiltsPiramid(int userId, int projectId)
        {
            this.UserId = userId;
            this.ProjectId = projectId;
        }
    }
}
