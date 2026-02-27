namespace PlanosSaude.API.DTOs.Planos;

public sealed record PlanoRequestDto(
    string Nome,
    string Codigo,
    decimal CustoMensal,
    string Cobertura
);