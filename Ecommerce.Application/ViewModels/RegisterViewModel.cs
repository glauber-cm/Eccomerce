using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não conferem!")]
        public string ConfirmarSenha { get; set; } = string.Empty;


    }
}
