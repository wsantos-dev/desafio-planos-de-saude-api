namespace PlanosSaude.API.Errors;

public record ErrorResponse(
   string Mensagem,
   string? Detalhe,
   DateTimeOffset DataHora
);
