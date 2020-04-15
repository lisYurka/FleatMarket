using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Имя не должно быть пустым!")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁё]+$", ErrorMessage = "Имя должно содержать только буквы!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Фамилия не должна быть пустой!")]
        [RegularExpression(@"^[A-Za-zА-Яа-яЁё]+$", ErrorMessage = "Фамилия должна содержать только буквы!")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "E-Mail не должен быть пустым!")]
        [RegularExpression(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$", ErrorMessage = "E-Mail имеет неверный формат!")]
        public string EMail { get; set; }

        [StringLength(13,MinimumLength = 7, ErrorMessage = "Проверьте правильность ввода!")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Телефон должен содержать тольцо цифры!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пароль не должен быть пустым!")]
        [RegularExpression(@"^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", 
            ErrorMessage = "Пароль должен содержать минимум 8 символов, одну цифру, одну букву в нижнем и одну букву в верхнем регистрах!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RegistrationDate { get; set; }
    }
}
