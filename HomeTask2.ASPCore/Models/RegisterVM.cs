using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Username unique")]
        [StringLength(20)]
        [UIHint("Login for SignIn")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        [UIHint("Email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [UIHint("Password")]
        public string Password { get; set; }
        
        public string ImageUrl { get; set; }
    }
}
