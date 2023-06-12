using System.ComponentModel.DataAnnotations;

namespace Vendas_Site.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Informe o nome de usuário")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Informe uma senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string URL { get; set; }
    }
}
