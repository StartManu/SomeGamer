using SomeGamer.Core.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeGamer.Core.Entity.Account
{
    public class ChangePassword :IdKeyIdentity
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O {0} Deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a nova senha")]
        [Compare("NewPassword", ErrorMessage = "As senhas não são iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
