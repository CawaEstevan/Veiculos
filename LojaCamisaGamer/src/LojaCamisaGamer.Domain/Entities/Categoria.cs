namespace LojaCamisaGamer.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Ativa { get; set; }
        public DateTime DataCriacao { get; set; }

        // Relacionamento 1:N - Uma categoria tem muitas camisas
        public virtual ICollection<Camisa> Camisas { get; set; } = new List<Camisa>();
    }
}