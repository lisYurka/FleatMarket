using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleatMarket.Web.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-Mail не должен быть пустым!")]
        [RegularExpression(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$", ErrorMessage = "E-Mail имеет неверный формат!")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Пароль не должен быть пустым!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //public bool RememberUser { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
