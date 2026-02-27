namespace PlanosSaude.API.DTOs;

public record BeneficiarioResponseDto(
    Guid Id,
    string Nome,
    string Cpf,
    DateOnly DataNascimento,
    string Email,
    string Telefone,
    bool IsAtivo
);
