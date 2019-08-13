using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Basic_Example.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Էլ․ Փոստ")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Ծննդ․ տարին")]
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage = "Գաղտնաբառերը չեն համընկնում")]
        [DataType(DataType.Password)]
        [Display(Name = "Հաստատել գաղտնաբառը")]
        public string PasswordConfirm { get; set; }
    }
}
