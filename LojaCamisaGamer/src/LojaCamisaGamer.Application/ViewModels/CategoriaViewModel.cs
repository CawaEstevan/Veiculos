using System.ComponentModel.DataAnnotations;

namespace LojaCamisaGamer.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        public bool Ativa { get; set; }
        public DateTime DataCriacao { get; set; }

        // Para exibição
        public int QuantidadeCamisas { get; set; }
    }
}