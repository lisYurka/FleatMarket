using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
