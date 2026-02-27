namespace PlanosSaude.API.DTOs.Contratacao;

public sealed record ContratarRequestDto(
    Guid BeneficiarioId,
    Guid PlanoId,
    DateTime DataInicio
);
