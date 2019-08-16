using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Basic_Example.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Էլ․ փոստ")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառ")]
        [StringLength(100,ErrorMessage = "Գաղտնաբառը պետք է բաղկացած լինի առնվազն 6 և ոչ ավել քան 100 սիմվոլից", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Գաղտնաբառի կրկնություն")]
        [Compare("Password",ErrorMessage = "Գաղտնաբառերը չեն համընկնում")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
