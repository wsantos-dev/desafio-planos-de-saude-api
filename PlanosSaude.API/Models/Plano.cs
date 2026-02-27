using PlanosSaude.API.Models.Enums;

namespace PlanosSaude.API.Models
{
    public class Plano
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public string? Codigo { get; set; }

        public decimal CustoMensal { get; set; }

        public Cobertura Cobertura { get; set; }

        public ICollection<Contratacao> Contratacoes { get; set; } =
            new List<Contratacao>();

    }
}