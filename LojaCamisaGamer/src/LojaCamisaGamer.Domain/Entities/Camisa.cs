namespace LojaCamisaGamer.Domain.Entities
{
    public class Camisa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string Tamanho { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public string ImagemUrl { get; set; } = string.Empty;
        public bool Disponivel { get; set; }
        public DateTime DataCadastro { get; set; }

        // Chave estrangeira explícita
        public int CategoriaId { get; set; }
        
        // Propriedade de navegação
        public virtual Categoria Categoria { get; set; } = null!;
    }
}