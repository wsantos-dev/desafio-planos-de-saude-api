using PlanosSaude.API.Models.Enums;

namespace PlanosSaude.API.DTOs.Planos;

public sealed record PlanoResponseDto(
    Guid Id,
    string Nome,
    string Codigo,
    decimal CustoMensal,
    Cobertura Cobertura
);