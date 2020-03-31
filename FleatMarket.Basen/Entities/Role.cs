using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FleatMarket.Base.Entities
{
    //public class Role//:IdentityRole
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    public string RoleName { get; set; }

    //    public List<User> Users { get; set; }
    //}

    public class Role:IdentityRole
    {
        public string RoleName { get; set; }

        public List<User> Roles { get; set; }
    }
}
