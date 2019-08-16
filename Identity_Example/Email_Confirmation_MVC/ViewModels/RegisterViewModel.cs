using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Email_Confirmation_MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Էլ․ փոստ")]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Գաղտնաբառ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage = "Գաղտնաբառեռրը չեն համընկնում")]
        [Display(Name = "Գաղտնաբառի հաստատում")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
