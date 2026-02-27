using Xunit;
using PlanosSaude.API.Validators;

public class CpfValidatorTests
{
    [Theory]
    [InlineData("123.456.789-09", true)]
    [InlineData("111.111.111-11", false)]
    [InlineData("000.000.000-00", false)]
    public void ValidarCpf_Testes(string cpf, bool esperado)
    {
        var resultado = CpfValidator.IsValid(cpf);
        Assert.Equal(esperado, resultado);
    }
}