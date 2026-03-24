using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.GetById;

public class GetWorkshopByIdValidator : AbstractValidator<GetWorkshopByIdInputDto>
{
    public GetWorkshopByIdValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero.");
    }
}
