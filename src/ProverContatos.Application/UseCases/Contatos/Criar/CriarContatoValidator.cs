using FluentValidation;
using ProverContatos.Communication.Requests;
using ProverContatos.Domain.Entities;

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
            .Must(Contato.DataNascimentoEhValida)
                .WithMessage("A data de nascimento não pode ser maior que a data atual.");

        RuleFor(x => x.DataNascimento)
            .Must(Contato.EhMaiorDeIdade)
                .WithMessage("O contato deve ser maior de idade " + "(mínimo 18 anos).");

        RuleFor(x => x.Sexo)
            .IsInEnum().WithMessage("Sexo inválido.");
    }
}