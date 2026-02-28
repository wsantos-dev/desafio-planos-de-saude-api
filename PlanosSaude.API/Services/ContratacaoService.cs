using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.Data;
using PlanosSaude.API.DTOs.Beneficiarios;
using PlanosSaude.API.Errors.Exceptions;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Services
{
    public class ContratacaoService : IContratacaoService
    {
        private readonly PlanosSaudeDbContext _context;

        public ContratacaoService(PlanosSaudeDbContext contex)
        {
            _context = contex;
        }

        public async Task CancelarAsync(Guid contratacaoId, CancellationToken cancellationToken)
        {
            var contratacao = await _context.Contratacoes
            .FirstOrDefaultAsync(c => c.Id == contratacaoId, cancellationToken);

            if (contratacao is null)
                throw new NotFoundException("Contratação não encontrada.");

            // Regra de negócio está dentro da entidade
            contratacao.Cancelar();
            _context.Update(contratacao);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ContratacaoResponseDto> ContratarAsync(
            Guid beneficiarioId,
            Guid planoId,
            DateTimeOffset dataInicio,
            CancellationToken cancellationToken)
        {

            var beneficiario = await _context.Beneficiarios
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == beneficiarioId, cancellationToken);

            if (beneficiario is null)
                throw new NotFoundException("Beneficiário não encontrado.");

            var plano = await _context.Planos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == planoId, cancellationToken);

            if (plano is null)
                throw new NotFoundException("Plano não encontrado.");

            var possuiAtiva = await _context.Contratacoes
                .AnyAsync(c => c.BeneficiarioId == beneficiarioId && c.DataFim == null, cancellationToken);

            if (possuiAtiva)
                throw new BusinessException("Beneficiário já possui contratação ativa.");

            if (dataInicio.Date < DateTime.UtcNow.Date)
                throw new BusinessException("Data de início não pode ser no passado.");


            var idade = Contratacao.CalcularIdade(beneficiario.DataNascimento);

            if (idade < 18)
                throw new BusinessException("Beneficiário deve ter pelo menos 18 anos.");

            var contratacao = new Contratacao(
                beneficiarioId,
                planoId,
                dataInicio,
                beneficiario.DataNascimento);


            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync(cancellationToken);

            return new ContratacaoResponseDto(
                contratacao.Id,
                contratacao.BeneficiarioId,
                beneficiario.Nome!,
                contratacao.PlanoId,
                plano.Nome!,
                contratacao.DataInicio,
                contratacao.DataFim,
                contratacao.DataFim == null);
        }

        public async Task<ContratacaoResponseDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var contratacao = await _context.Contratacoes
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new ContratacaoResponseDto(
                    c.Id,
                    c.BeneficiarioId,
                    c.Beneficiario.Nome,
                    c.PlanoId,
                    c.Plano.Nome,
                    c.DataInicio,
                    c.DataFim,
                    c.DataFim == null
                ))
                .FirstOrDefaultAsync(cancellationToken);

            if (contratacao is null)
                throw new NotFoundException("Contratação não encontrada.");

            return contratacao;
        }
    }
}