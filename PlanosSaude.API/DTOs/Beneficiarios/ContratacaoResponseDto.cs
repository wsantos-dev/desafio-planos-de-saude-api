namespace PlanosSaude.API.DTOs.Beneficiarios;

public record ContratacaoResponseDto(
    Guid Id,
    Guid BeneficiarioId,
    string BeneficiarioNome,
    Guid PlanoId,
    string PlanoNome,
    DateTimeOffset DataInicio,
    DateTimeOffset? DataFim,
    bool Ativa
);
