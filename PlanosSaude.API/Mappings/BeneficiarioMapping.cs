using PlanosSaude.API.DTOs;
using PlanosSaude.API.Models;

namespace PlanosSaude.API.Mappings
{
    public static class BeneficiarioMapping
    {
        public static BeneficiarioResponseDto ToResponseDto(this Beneficiario b)
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
    }
}