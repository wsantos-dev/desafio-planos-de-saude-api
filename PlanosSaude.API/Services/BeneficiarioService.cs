using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.Data;
using PlanosSaude.API.DTOs;
using PlanosSaude.API.Errors.Exceptions;
using PlanosSaude.API.Mappings;
using PlanosSaude.API.Models;
using PlanosSaude.API.Validators;
using ValidationException = PlanosSaude.API.Errors.Exceptions.ValidationException;

namespace PlanosSaude.API.Services
{
    public class BeneficiarioService : IBeneficiarioService
    {
        private readonly PlanosSaudeDbContext _context;

        public BeneficiarioService(PlanosSaudeDbContext context)
        {
            _context = context;
        }

        public async Task<BeneficiarioResponseDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var beneficiario = await _context.Beneficiarios
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (beneficiario is null)
                throw new NotFoundException("Beneficiário não encontrado.");

            return beneficiario.ToResponseDto();
        }

        public async Task<IReadOnlyCollection<BeneficiarioResponseDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await _context.Beneficiarios
                    .AsNoTracking()
                    .Select(b => b.ToResponseDto())
                    .ToListAsync(cancellationToken);
        }

        public async Task<BeneficiarioResponseDto> CriarAsync(CriarBeneficiarioDto dto, CancellationToken cancellationToken)
        {
            if (!CpfValidator.IsValid(dto.Cpf))
                throw new ValidationException("CPF inválido.");

            if (!EmailValidator.EmailValido(dto.Email))
                throw new ValidationException("Email inválido.");

            var cpfExiste = await _context.Beneficiarios
                .AnyAsync(b => b.Cpf == dto.Cpf, cancellationToken);

            if (cpfExiste)
                throw new BusinessException("Já existe beneficiário com esse CPF.");

            var beneficiario = new Beneficiario
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                DataNascimento = dto.DataNascimento,
                Email = dto.Email,
                Telefone = dto.Telefone,
                IsAtivo = true,
                DataCriacao = DateTime.UtcNow
            };

            _context.Beneficiarios.Add(beneficiario);
            await _context.SaveChangesAsync(cancellationToken);

            return beneficiario.ToResponseDto();

        }
        public async Task AtualizarAsync(Guid id, AtualizarBeneficiarioDto dto, CancellationToken cancellationToken)
        {
            var beneficiario = await _context.Beneficiarios
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (beneficiario is null)
                throw new NotFoundException("Beneficiário não encontrado.");

            if (!EmailValidator.EmailValido(dto.Email))
                throw new ValidationException("Email inválido.");

            beneficiario.Nome = dto.Nome;
            beneficiario.Email = dto.Email;
            beneficiario.Telefone = dto.Telefone;
            beneficiario.IsAtivo = dto.IsAtivo;
            beneficiario.DataAtualizacao = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoverAsync(Guid id, CancellationToken cancellationToken)
        {
            var beneficiario = await _context.Beneficiarios
           .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (beneficiario is null)
                throw new NotFoundException("Beneficiário não encontrado.");

            _context.Beneficiarios.Remove(beneficiario);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}