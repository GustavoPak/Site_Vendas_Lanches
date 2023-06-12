using System.ComponentModel.DataAnnotations;

namespace Vendas_Site.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Informe um nome")]
        [Display(Name = "Nome")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Informe um email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe uma senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string Url { get; set; }

    }
}
