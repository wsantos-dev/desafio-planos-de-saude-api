using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanosSaude.API.DTOs;

namespace PlanosSaude.API.Services
{
    public interface IBeneficiarioService
    {
        Task<BeneficiarioResponseDto> CriarAsync(
        CriarBeneficiarioDto dto,
        CancellationToken cancellationToken);

        Task<BeneficiarioResponseDto?> ObterPorIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<BeneficiarioResponseDto>> ObterTodosAsync(
            CancellationToken cancellationToken);

        Task<bool> AtualizarAsync(
            Guid id,
            AtualizarBeneficiarioDto dto,
            CancellationToken cancellationToken);

        Task<bool> RemoverAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}