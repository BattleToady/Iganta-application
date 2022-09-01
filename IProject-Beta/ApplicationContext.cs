using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IProject_Beta
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }

        public DbSet<LongtermPlan> LongtermPlans { get; set; }

        public DbSet<DiltsPiramid> DiltsPiramids { get; set; }

        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<DiaryRecord> DiaryRecords { get; set; }

        public DbSet<PlanPhase> PlanPhases { get; set; }

        public DbSet<PlanTask> PlanTasks { get; set; }

        public DbSet<SMART> SMARTs { get; set; }

        public DbSet<GeneralSphere> GeneralSpheres { get; set; }
        public DbSet<brightnessSphere> brightnessSpheres { get; set; }
        public DbSet<environmentSphere> environmentSpheres { get; set; }
        public DbSet<healthSphere> healthSpheres { get; set; }
        public DbSet<independenceSphere> independenceSpheres { get; set; }

        public DbSet<relationSphere> relationSpheres { get; set; }
        public DbSet<selfdevelopmentSphere> selfdevelopmentSpheres { get; set; }
        public DbSet<spiritualitySphere> spiritualitySpheres { get; set; }

        public DbSet<vocationSphere> vocationSpheres { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}
