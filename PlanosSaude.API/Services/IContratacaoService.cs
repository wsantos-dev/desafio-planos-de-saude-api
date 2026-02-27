using PlanosSaude.API.DTOs.Beneficiarios;

namespace PlanosSaude.API.Services
{
    public interface IContratacaoService
    {
        Task<ContratacaoResponseDto> ContratarAsync(
            Guid beneficiarioId,
            Guid planoId,
            DateTimeOffset dataInicio,
            CancellationToken cancellationToken);

        Task CancelarAsync(Guid contratacaoId, CancellationToken cancellationToken);

        Task<ContratacaoResponseDto> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken);

    }
}