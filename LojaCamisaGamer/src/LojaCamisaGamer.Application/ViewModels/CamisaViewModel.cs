using System.ComponentModel.DataAnnotations;
using LojaCamisaGamer.Application.Validations;

namespace LojaCamisaGamer.Application.ViewModels
{
    public class CamisaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 150 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "A descrição deve ter entre 20 e 1000 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [PrecoMinimo(10, ErrorMessage = "O preço mínimo deve ser R$ 10,00")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório")]
        [EstoquePositivo(ErrorMessage = "O estoque deve ser maior ou igual a zero")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "O tamanho é obrigatório")]
        [RegularExpression("^(PP|P|M|G|GG|XG)$", ErrorMessage = "Tamanho inválido. Use: PP, P, M, G, GG ou XG")]
        public string Tamanho { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cor é obrigatória")]
        [StringLength(50, ErrorMessage = "A cor deve ter no máximo 50 caracteres")]
        public string Cor { get; set; } = string.Empty;

        [Url(ErrorMessage = "URL da imagem inválida")]
        [StringLength(500, ErrorMessage = "A URL da imagem deve ter no máximo 500 caracteres")]
        public string ImagemUrl { get; set; } = string.Empty;

        public bool Disponivel { get; set; }
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria válida")]
        public int CategoriaId { get; set; }

        // Para exibição
        public string? CategoriaNome { get; set; }
    }
}