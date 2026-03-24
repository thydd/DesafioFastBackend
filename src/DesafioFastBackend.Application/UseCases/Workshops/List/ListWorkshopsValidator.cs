using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.List;

public class ListWorkshopsValidator : AbstractValidator<ListWorkshopsInputDto>
{
    public ListWorkshopsValidator()
    {
        RuleFor(x => x)
            .NotNull();
    }
}
