using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.Create;

public class CreateWorkshopValidator : AbstractValidator<CreateWorkshopInputDto>
{
    public CreateWorkshopValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória.")
            .MaximumLength(2000).WithMessage("Descrição deve ter no máximo 2000 caracteres.");

        RuleFor(x => x.DataRealizacao)
            .NotEqual(default(DateTime)).WithMessage("DataRealizacao é obrigatória.");
    }
}
