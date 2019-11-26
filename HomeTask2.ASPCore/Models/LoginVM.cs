using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask2.ASPCore.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage ="This is required field")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="You are not write password...")]
        public string Password { get; set; }
    }
}
