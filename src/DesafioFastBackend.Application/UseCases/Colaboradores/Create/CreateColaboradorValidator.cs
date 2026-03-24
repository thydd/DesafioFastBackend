using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Create;

public class CreateColaboradorValidator : AbstractValidator<CreateColaboradorInputDto>
{
    public CreateColaboradorValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");
    }
}
