
namespace PlanosSaude.API.DTOs;

public record CriaBeneficiarioDto(
    string Nome,
    string Cpf,
    DateOnly DataNascimento,
    string Email,
    string Telefone
);

