using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendas_Site.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [StringLength(100,ErrorMessage = "O tamanho máximo é {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome da Categoria")]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }
        [StringLength(200,ErrorMessage = "Descrição no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        public List<Lanche> Lanches { get; set; }
    }
}
