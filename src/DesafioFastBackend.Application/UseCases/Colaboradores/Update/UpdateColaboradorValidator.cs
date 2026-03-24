using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Update;

public class UpdateColaboradorValidator : AbstractValidator<UpdateColaboradorInputDto>
{
    public UpdateColaboradorValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");
    }
}
