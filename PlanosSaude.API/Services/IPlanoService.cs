using PlanosSaude.API.DTOs.Planos;

namespace PlanosSaude.API.Services
{
    public interface IPlanoService
    {
        Task<PlanoResponseDto> CriarAsync(PlanoRequestDto dto, CancellationToken cancellationToken);
        Task<PlanoResponseDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<PlanoResponseDto>> ListarAsync(CancellationToken cancellationToken);
        Task AtualizarAsync(Guid id, PlanoRequestDto dto, CancellationToken cancellationToken);
        Task RemoverAsync(Guid id, CancellationToken cancellationToken);
    }
}