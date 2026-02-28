using Xunit;
using PlanosSaude.API.Services;
using PlanosSaude.API.Models;
using PlanosSaude.API.Data;
using PlanosSaude.API.DTOs.Beneficiarios;
using PlanosSaude.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using PlanosSaude.API.DTOs.Planos;
using PlanosSaude.API.Errors.Exceptions;

public class ContratacaoServiceTests
{
    private static PlanosSaudeDbContext CriarContext()
    {
        var options = new DbContextOptionsBuilder<PlanosSaudeDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        return new PlanosSaudeDbContext(options);
    }

    [Fact]
    public async Task ContratarBeneficiario_Valido_RetornaContratacao()
    {
        var context = CriarContext();
        var serviceBenef = new BeneficiarioService(context);
        var servicePlano = new PlanoService(context);
        var service = new ContratacaoService(context);

        var beneficiario = await serviceBenef.CriarAsync(
            new CriarBeneficiarioDto(
                "João Silva", "12345678909", new DateOnly(2000, 1, 1),
                "joao@email.com", "5581912345678", true
            ), default);

        var plano = await servicePlano.CriarAsync(
            new PlanoRequestDto("Plano Teste", "PRT001", 200m, PlanosSaude.API.Models.Enums.Cobertura.Ambulatorial), default);

        var result = await service.ContratarAsync(
            beneficiario.Id, plano.Id, DateTime.UtcNow.AddDays(1), default);

        Assert.NotNull(result);
        Assert.True(result.Ativa);
    }

    [Fact]
    public async Task ContratarBeneficiario_Menor18_DisparaExcecao()
    {
        var context = CriarContext();
        var serviceBenef = new BeneficiarioService(context);
        var servicePlano = new PlanoService(context);
        var service = new ContratacaoService(context);

        var beneficiario = await serviceBenef.CriarAsync(
            new CriarBeneficiarioDto(
                "Lucas Junior", "98765432100", DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-16)),
                "lucas@email.com", "5581912345678", true
            ), default);

        var plano = await servicePlano.CriarAsync(
            new PlanoRequestDto("Plano Teste", "PRT001", 200m, PlanosSaude.API.Models.Enums.Cobertura.Ambulatorial), default);

        await Assert.ThrowsAsync<BusinessException>(() =>
            service.ContratarAsync(beneficiario.Id, plano.Id, DateTime.UtcNow.AddDays(1), default));
    }

    [Fact]
    public async Task ContratarBeneficiario_DataInicioPassada_DisparaExcecao()
    {
        var context = CriarContext();
        var serviceBenef = new BeneficiarioService(context);
        var servicePlano = new PlanoService(context);
        var service = new ContratacaoService(context);

        var beneficiario = await serviceBenef.CriarAsync(
            new CriarBeneficiarioDto(
                "João Silva", "12345678909", new DateOnly(2000, 1, 1),
                "joao@email.com", "5581912345678", true
            ), default);

        var plano = await servicePlano.CriarAsync(
            new PlanoRequestDto("Plano Teste", "PRT001", 200m, PlanosSaude.API.Models.Enums.Cobertura.Ambulatorial), default);

        await Assert.ThrowsAsync<BusinessException>(() =>
            service.ContratarAsync(beneficiario.Id, plano.Id, DateTime.UtcNow.AddDays(-1), default));
    }

    [Fact]
    public async Task CancelarContratacao_Ativa_RetornaSemErro()
    {
        var context = CriarContext();
        var serviceBenef = new BeneficiarioService(context);
        var servicePlano = new PlanoService(context);
        var service = new ContratacaoService(context);

        var beneficiario = await serviceBenef.CriarAsync(
            new CriarBeneficiarioDto(
                "João Silva", "12345678909", new DateOnly(2000, 1, 1),
                "joao@email.com", "5581912345678", true
            ), default);

        var plano = await servicePlano.CriarAsync(
            new PlanoRequestDto("Plano Teste", "PRT001", 200m, PlanosSaude.API.Models.Enums.Cobertura.Ambulatorial), default);

        var contratacao = await service.ContratarAsync(
            beneficiario.Id, plano.Id, DateTime.UtcNow.AddDays(1), default);

        await service.CancelarAsync(contratacao.Id, default);

        var contratacaoAtual = await service.ObterPorIdAsync(contratacao.Id, default);
        Assert.False(contratacaoAtual.Ativa);
        Assert.NotNull(contratacaoAtual.DataFim);
    }

    [Fact]
    public async Task CancelarContratacao_NaoAtiva_DisparaExcecao()
    {
        var context = CriarContext();
        var serviceBenef = new BeneficiarioService(context);
        var servicePlano = new PlanoService(context);
        var service = new ContratacaoService(context);

        var beneficiario = await serviceBenef.CriarAsync(
            new CriarBeneficiarioDto(
                "João Silva", "12345678909", new DateOnly(2000, 1, 1),
                "joao@email.com", "+5581912345678", true
            ), default);

        var plano = await servicePlano.CriarAsync(
            new PlanoRequestDto("Plano Teste", "PRT001", 200m, PlanosSaude.API.Models.Enums.Cobertura.Ambulatorial), default);

        var contratacao = await service.ContratarAsync(
            beneficiario.Id, plano.Id, DateTime.UtcNow.AddDays(1), default);

        // Cancelando 1ª vez
        await service.CancelarAsync(contratacao.Id, default);

        // Cancelando 2ª vez → deve disparar exceção
        await Assert.ThrowsAsync<BusinessException>(() =>
            service.CancelarAsync(contratacao.Id, default));
    }
}