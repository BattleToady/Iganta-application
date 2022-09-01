using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class independenceSphere
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int incomes { get; set; }
        public int expenses { get; set; }
        public int spending_possibility  { get; set; }


        public independenceSphere() { }

        public independenceSphere(int UserId)
        {
            this.UserId = UserId;
            this.incomes = 0;
            this.expenses = 0;
            this.spending_possibility = 0;
        }
    }
}
