using PlanosSaude.API.DTOs;
using PlanosSaude.API.DTOs.Beneficiarios;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Mappings
{
    public static class ContratacaoMapping
    {
        public static ContratacaoResponseDto ToResponseDto(
            this Contratacao contratacao,
            string beneficiarioNome,
            string planoNome)
        {
            return new ContratacaoResponseDto(
                contratacao.Id,
                contratacao.BeneficiarioId,
                beneficiarioNome,
                contratacao.PlanoId,
                planoNome,
                contratacao.DataInicio,
                contratacao.DataFim,
                contratacao.Ativa
            );
        }
    }
}