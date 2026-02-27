using PlanosSaude.API.Services;
using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.Data;
using PlanosSaude.API.Errors.Exceptions;
using PlanosSaude.API.DTOs;

public class BeneficiarioServiceTests
{
    private PlanosSaudeDbContext CriarContext()
    {
        var options = new DbContextOptionsBuilder<PlanosSaudeDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=planos_saude_test;Username=desenvolvedor;Password=DotNet@2026")
            .Options;
        return new PlanosSaudeDbContext(options);
    }

    [Fact]
    public async Task CriarBeneficiario_Valido_RetornaBeneficiario()
    {
        var context = CriarContext();
        var service = new BeneficiarioService(context);

        var dto = new CriarBeneficiarioDto(
            "João Silva",
            "123.456.789-09",
            new DateOnly(2000, 1, 1),
            "joao@email.com",
            "+5581912345678",
            true
        );

        var result = await service.CriarAsync(dto, default);

        Assert.NotNull(result);
        Assert.Equal(dto.Nome, result.Nome);
    }

    [Fact]
    public async Task CriarBeneficiario_CpfInvalido_DisparaExcecao()
    {
        var context = CriarContext();
        var service = new BeneficiarioService(context);

        var dto = new CriarBeneficiarioDto(
            "Maria Silva",
            "111.111.111-11",
            new DateOnly(2000, 1, 1),
            "maria@email.com",
            "+5581912345678",
            true
        );

        await Assert.ThrowsAsync<BusinessException>(() => service.CriarAsync(dto, default));
    }

    [Fact]
    public async Task CriarBeneficiario_EmailInvalido_DisparaExcecao()
    {
        var context = CriarContext();
        var service = new BeneficiarioService(context);

        var dto = new CriarBeneficiarioDto(
            "Carlos",
            "123.456.789-09",
            new DateOnly(2000, 1, 1),
            "emailinvalido",
            "+5581912345678",
            true
        );

        await Assert.ThrowsAsync<BusinessException>(() => service.CriarAsync(dto, default));
    }

    [Fact]
    public async Task ObterBeneficiario_Inexistente_DisparaNotFound()
    {
        var context = CriarContext();
        var service = new BeneficiarioService(context);

        await Assert.ThrowsAsync<NotFoundException>(() => service.ObterPorIdAsync(Guid.NewGuid(), default));
    }
}