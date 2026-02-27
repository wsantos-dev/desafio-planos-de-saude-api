
namespace PlanosSaude.API.DTOs;

public record CriarBeneficiarioDto(
    string Nome,
    string Cpf,
    DateOnly DataNascimento,
    string Email,
    string Telefone
);

