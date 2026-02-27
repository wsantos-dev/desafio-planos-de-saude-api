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
    }
}