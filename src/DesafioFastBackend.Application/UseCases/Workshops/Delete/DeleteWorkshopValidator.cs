using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.Delete;

public class DeleteWorkshopValidator : AbstractValidator<DeleteWorkshopInputDto>
{
    public DeleteWorkshopValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero.");
    }
}
