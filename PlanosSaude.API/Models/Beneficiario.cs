namespace PlanosSaude.API.Models
{
    public class Beneficiario
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public bool IsAtivo { get; set; }
        public DateTimeOffset DataCriacao { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DataAtualizacao { get; set; }
        public ICollection<Contratacao> Contratacoes { get; set; } = new List<Contratacao>();
    }
}