using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.Data;
using PlanosSaude.API.DTOs;
using PlanosSaude.API.Models;
using PlanosSaude.API.Validators;

namespace PlanosSaude.API.Services
{
    public class BeneficiarioService : IBeneficiarioService
    {
        private readonly PlanosSaudeDbContext _context;

        public BeneficiarioService(PlanosSaudeDbContext context)
        {
            _context = context;
        }

        public async Task<BeneficiarioResponseDto?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var beneficiário = await _context.Beneficiarios
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            return beneficiário is null ? null : MapToResponse(beneficiário);
        }

        public async Task<IReadOnlyCollection<BeneficiarioResponseDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await _context.Beneficiarios
                    .AsNoTracking()
                    .Select(b => MapToResponse(b))
                    .ToListAsync(cancellationToken);
        }

        public async Task<BeneficiarioResponseDto> CriarAsync(CriarBeneficiarioDto dto, CancellationToken cancellationToken)
        {
            if (!CpfValidator.IsValid(dto.Cpf))
                throw new ArgumentException("CPF inválido.");

            if (!EmailValido(dto.Email))
                throw new ArgumentException("Email inválido.");

            var cpfExiste = await _context.Beneficiarios
                .AnyAsync(b => b.Cpf == dto.Cpf, cancellationToken);

            if (cpfExiste)
                throw new InvalidOperationException("Já existe beneficiário com esse CPF.");

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

            return MapToResponse(beneficiario);

        }
        public async Task<bool> AtualizarAsync(Guid id, AtualizarBeneficiarioDto dto, CancellationToken cancellationToken)
        {
            var beneficiario = await _context.Beneficiarios
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (beneficiario is null)
                return false;

            if (!EmailValido(dto.Email))
                throw new ArgumentException("Email inválido.");

            beneficiario.Nome = dto.Nome;
            beneficiario.Email = dto.Email;
            beneficiario.Telefone = dto.Telefone;
            beneficiario.IsAtivo = dto.IsAtivo;
            beneficiario.DataAtualizacao = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> RemoverAsync(Guid id, CancellationToken cancellationToken)
        {
            var beneficiario = await _context.Beneficiarios
           .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (beneficiario is null)
                return false;

            _context.Beneficiarios.Remove(beneficiario);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        private static BeneficiarioResponseDto MapToResponse(Beneficiario b)
        {
            return new BeneficiarioResponseDto(
                b.Id,
                b.Nome,
                b.Cpf,
                b.DataNascimento,
                b.Email,
                b.Telefone,
                b.IsAtivo
            );
        }



        public static bool EmailValido(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}