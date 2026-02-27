using PlanosSaude.API.Errors.Exceptions;
using PlanosSaude.API.Models.Enums;

namespace PlanosSaude.API.Models
{
    public class Contratacao
    {
        public Guid Id { get; set; }
        public Guid BeneficiarioId { get; set; }
        public Beneficiario? Beneficiario { get; set; }
        public Guid PlanoId { get; set; }
        public Plano? Plano { get; set; }
        public DateTimeOffset DataInicio { get; set; }
        public DateTimeOffset? DataFim { get; set; }
        public StatusContratacao Status { get; set; }

        public bool Ativa => DataFim == null;

        private Contratacao() { }

        public Contratacao(
        Guid beneficiarioId,
        Guid planoId,
        DateTimeOffset dataInicio,
        DateOnly dataNascimento)
        {
            if (dataInicio.Date < DateTime.UtcNow.Date)
                throw new BusinessException("Data de início não pode ser no passado.");

            if (CalcularIdade(dataNascimento) < 18)
                throw new BusinessException("Beneficiário deve ter no mínimo 18 anos.");

            Id = Guid.NewGuid();
            BeneficiarioId = beneficiarioId;
            PlanoId = planoId;
            DataInicio = dataInicio;
        }

        public void Cancelar()
        {
            if (!Ativa)
                throw new BusinessException("Somente contratações ativas podem ser canceladas.");

            DataFim = DateTime.UtcNow;
        }

        public static int CalcularIdade(DateOnly dataNascimento)
        {
            var hoje = DateOnly.FromDateTime(DateTime.UtcNow);

            int idade = hoje.Year - dataNascimento.Year;

            if (hoje < dataNascimento.AddYears(idade))
                idade--;

            return idade;

        }
    }
}