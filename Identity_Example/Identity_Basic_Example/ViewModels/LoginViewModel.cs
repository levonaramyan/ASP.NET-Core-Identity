using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Basic_Example.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Էլ․ Փոստ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }

        [Display(Name = "Հիշե՞լ")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
