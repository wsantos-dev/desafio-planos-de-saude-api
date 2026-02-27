using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.Data;
using PlanosSaude.API.DTOs.Planos;
using PlanosSaude.API.Errors.Exceptions;
using PlanosSaude.API.Mappings;
using PlanosSaude.API.Models;
using PlanosSaude.API.Models.Enums;
using PlanosSaude.API.Services;

public class PlanoService : IPlanoService
{
    private readonly PlanosSaudeDbContext _context;

    public PlanoService(PlanosSaudeDbContext context)
    {
        _context = context;
    }

    public async Task<PlanoResponseDto> CriarAsync(PlanoRequestDto dto, CancellationToken cancellationToken)
    {
        var cobertura = Enum.Parse<Cobertura>(dto.Cobertura);
        var plano = new Plano(dto.Nome, dto.Codigo, dto.CustoMensal, cobertura);
        _context.Planos.Add(plano);
        await _context.SaveChangesAsync(cancellationToken);
        return plano.ToResponseDto();
    }

    public async Task<PlanoResponseDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var plano = await _context.Planos.FindAsync(new object[] { id }, cancellationToken);
        if (plano is null)
            throw new NotFoundException("Plano não encontrado.");

        return plano.ToResponseDto();
    }

    public async Task<IReadOnlyCollection<PlanoResponseDto>> ListarAsync(CancellationToken cancellationToken)
    {
        return await _context.Planos
            .AsNoTracking()
            .Select(p => p.ToResponseDto())
            .ToListAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Guid id, PlanoRequestDto dto, CancellationToken cancellationToken)
    {
        var plano = await _context.Planos.FindAsync(new object[] { id }, cancellationToken);
        if (plano is null)
            throw new NotFoundException("Plano não encontrado.");

        // Atualiza propriedades
        var cobertura = Enum.Parse<Cobertura>(dto.Cobertura);
        plano = new Plano(dto.Nome, dto.Codigo, dto.CustoMensal, cobertura) { Id = id };

        _context.Planos.Update(plano);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken)
    {
        var plano = await _context.Planos.FindAsync(new object[] { id }, cancellationToken);
        if (plano is null)
            throw new NotFoundException("Plano não encontrado.");

        _context.Planos.Remove(plano);
        await _context.SaveChangesAsync(cancellationToken);
    }
}