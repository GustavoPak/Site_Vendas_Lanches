using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vendas_Site.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [Display(Name = "Nome do lanche")]
        [StringLength(60,MinimumLength = 2,ErrorMessage = "O {0} deve ter no máximo {1} caracteres e no minimo {2}")]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Descrição do lanche")]
        [MinLength(15,ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo {1} caracteres")]
        public string DescricaoCurta { get; set; }
        [Required]
        [Display(Name = "Descrição detalhada do lanche")]
        [MinLength(15, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição deve ter no máximo {1} caracteres")]
        public string DescricaoDetalhada { get; set; }
        [Required(ErrorMessage = "Infome o preço do lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1,999.99,ErrorMessage = "Minimo de {1}R$ e máximo de {2}R$")]
        public decimal Preco { get; set; }
        [Display(Name = "Caminho de imagem")]
        [StringLength(200,ErrorMessage = "Numero máximo de caracteres excedido")]
        public string ImagemUrl { get; set; }
        [Display(Name = "Caminho de imagem Miniatura")]
        [StringLength(200, ErrorMessage = "Numero máximo de caracteres excedido")]
        public string ImagemThumbnailUrl { get; set; }
        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }
        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
