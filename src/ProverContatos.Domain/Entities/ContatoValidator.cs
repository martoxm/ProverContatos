using FluentValidation;

namespace ProverContatos.Domain.Entities;

public class ContatoValidator : AbstractValidator<Contato>
{
    public ContatoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .Must(Contato.DataNascimentoEhValida)
                .WithMessage("A data de nascimento não pode ser maior que a data atual.")
            .DependentRules(() =>
            {
                RuleFor(x => x.DataNascimento)
                    .Must(Contato.EhMaiorDeIdade)
                        .WithMessage("O contato deve ser maior de idade (mínimo 18 anos).")
                    .Must(Contato.IdadeEhDiferenteDeZero)
                        .WithMessage("A idade do contato não pode ser zero.");
            });

        RuleFor(x => x.Sexo)
            .IsInEnum().WithMessage("Sexo inválido.");
    }
}