using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }    
        public Person Person { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
