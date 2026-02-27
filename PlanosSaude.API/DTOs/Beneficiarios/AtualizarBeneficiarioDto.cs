namespace PlanosSaude.API.DTOs;

public record AtualizarBeneficiarioDto(
    string Nome,
    string Email,
    string Telefone,
    bool IsAtivo
);

