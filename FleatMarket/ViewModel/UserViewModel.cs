using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
