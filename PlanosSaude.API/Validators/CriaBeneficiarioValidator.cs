using System.Text.RegularExpressions;
using FluentValidation;
using PlanosSaude.API.DTOs;

namespace PlanosSaude.API.Validators;

public class CriaBeneficiarioValidator : AbstractValidator<CriaBeneficiarioDto>
{

    public CriaBeneficiarioValidator()
    {

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(ValidarCpf).WithMessage("CPF em formato inválido ou inexistente.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("Telefone inválido. Use o formato (11) 91234-5678.");

        RuleFor(x => x.DataNascimento)
            .Must(Tem18Anos).WithMessage("O beneficiário deve ter pelo menos 18 anos.");

    }

    public static bool ValidarCpf(string cpf)
    {
        cpf = Regex.Replace(cpf, "[^0-9]", "");

        if (cpf.Length != 11)
            return false;

        if (cpf.All(c => c == cpf[0]))
            return false;

        int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf[..9];
        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        var digito = resto.ToString();
        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }

    private bool Tem18Anos(DateOnly dataNascimento)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Now);
        var idade = hoje.Year - dataNascimento.Year;

        if (dataNascimento > hoje.AddYears(-idade)) idade--;

        return idade >= 18;
    }
}