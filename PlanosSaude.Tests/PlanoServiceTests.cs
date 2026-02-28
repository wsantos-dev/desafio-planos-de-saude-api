using PlanosSaude.API.Data;
using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.DTOs.Planos;
using PlanosSaude.API.Errors.Exceptions;
using PlanosSaude.API.Models.Enums;

public class PlanoServiceTests
{
    private static PlanosSaudeDbContext CriarContext()
    {
        var options = new DbContextOptionsBuilder<PlanosSaudeDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        return new PlanosSaudeDbContext(options);
    }

    [Fact]
    public async Task CriarPlano_Valido_RetornaPlano()
    {
        var context = CriarContext();
        var service = new PlanoService(context);

        var dto = new PlanoRequestDto("Plano Teste", "PRT001", 200m, Cobertura.Ambulatorial);

        var result = await service.CriarAsync(dto, default);

        Assert.NotNull(result);
        Assert.Equal(dto.Nome, result.Nome);
    }

    [Fact]
    public async Task ObterPlano_Inexistente_DisparaNotFound()
    {
        var context = CriarContext();
        var service = new PlanoService(context);

        await Assert.ThrowsAsync<NotFoundException>(() => service.ObterPorIdAsync(Guid.NewGuid(), default));
    }

    [Fact]
    public async Task RemoverPlano_Inexistente_DisparaNotFound()
    {
        var context = CriarContext();
        var service = new PlanoService(context);

        await Assert.ThrowsAsync<NotFoundException>(() => service.RemoverAsync(Guid.NewGuid(), default));
    }
}