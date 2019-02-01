using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPproject.Models
{
    public class User
    {
       public int Id { get; set; }
        [Display(Name ="Email")]
       public string Email { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; } 

       public int RoleID { get; set; }
       public Role Role { get; set; }

    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}