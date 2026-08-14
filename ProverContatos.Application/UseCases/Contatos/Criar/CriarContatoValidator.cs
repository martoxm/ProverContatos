using FluentValidation;
using ProverContatos.Communication.Requests;

namespace ProverContatos.Application.UseCases.Contatos.Criar;

public class CriarContatoValidator : AbstractValidator<RequestCriarContatoJson>
{
    public CriarContatoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .Must(data => data < DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("A data de nascimento não pode ser maior que a data atual.")
            .Must(data => data != DateOnly.MinValue)
                .WithMessage("Data de nascimento inválida.");

        RuleFor(x => x.DataNascimento)
            .Must(data =>
            {
                var hoje = DateOnly.FromDateTime(DateTime.Today);
                var idade = hoje.Year - data.Year;
                if (data > hoje.AddYears(-idade)) idade--;
                return idade >= 18;
            })
            .WithMessage("O contato deve ser maior de idade (mínimo 18 anos).");

        RuleFor(x => x.Sexo)
            .IsInEnum().WithMessage("Sexo inválido.");
    }
}