using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FleatMarket.Base.Entities
{
    //public class User//:IdentityUser
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Surname { get; set; }
    //    public string EMail { get; set; }
    //    public string Phone { get; set; }
    //    public string Password { get; set; }
    //    public bool IsActive { get; set; } = true;

    //    public int RoleId { get; set; }
    //    [ForeignKey("RoleId")]
    //    public Role Role { get; set; }

    //    public List<Declaration> Declarations { get; set; }
    //}
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; } = true;

        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public List<Declaration> Declarations { get; set; }
    }
}
