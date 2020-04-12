using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.ViewModel
{
    public class PersonalAreaViewModel
    {
        public string ImagePath { get; set; }
        public string UserName { get; set; }
        public int AllDeclarationsCount { get; set; }
        public int SoldDeclarationsCount { get; set; }
        public string RegistrationDate { get; set; }
        public string LastDateOfEdit { get; set; }
    }
}
