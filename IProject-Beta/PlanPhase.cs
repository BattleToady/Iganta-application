using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class PlanPhase
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BranchId { get; set; }
        public string Name { get; set; }

        public int Position {get;set;}

        public int Progress { get;set;}

        public PlanPhase() { }

        public PlanPhase(int UserId, int BranchId, int Position)
        {
            this.UserId = UserId;
            this.BranchId = BranchId;
            this.Position = Position;
        }

        public PlanPhase(int UserId, int BranchId, int Position, string Name)
        {
            this.UserId = UserId;
            this.BranchId = BranchId;
            this.Position = Position;
            this.Name = Name;
        }

        public PlanPhase(int UserId, int BranchId, int Position, string Name, int Progress)
        {
            this.UserId = UserId;
            this.BranchId = BranchId;
            this.Position = Position;
            this.Name = Name;
            this.Progress = Progress;
        }
    }
}
