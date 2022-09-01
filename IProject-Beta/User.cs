using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IProject_Beta
{
    class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public User() { }

        public User(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        public User(string name)
        {
            this.Name = name;
            this.Password = null;
        }

    }
}
