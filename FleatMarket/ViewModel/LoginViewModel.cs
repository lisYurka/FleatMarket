using FleatMarket.Base.Entities;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.ViewModel
{
    public class LoginViewModel
    {
        [Required,DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        //public bool RememberUser { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
