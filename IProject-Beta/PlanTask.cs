using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class PlanTask
    {
        [Key]
        public int Id { get; set; }

        public int PlanPhaseId { get; set; }

        public string Task { get; set; }

        public string Deadline { get; set; }

        public int Position { get; set; }

        public int Done { get; set; }

        public PlanTask() { }

        public PlanTask(int PlanPhaseId)
        {
            this.PlanPhaseId = PlanPhaseId;
            this.Done = 0;
        }

        public PlanTask(int PlanPhaseId, string Task, string Deadline, int Position)
        {
            this.PlanPhaseId = PlanPhaseId;
            this.Done = 0;
            this.Task = Task;
            this.Deadline = Deadline;
            this.Position = Position;
            this.Done = 0;
        }
    }
}
