using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class SMART
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string S { get; set; }

        public string M { get; set; }

        public string A { get; set; }

        public string T { get; set; }

        public string R { get; set; }

        public SMART() { }

        public SMART(int ProjectId)
        {
            this.ProjectId = ProjectId;
        }
    }
}
