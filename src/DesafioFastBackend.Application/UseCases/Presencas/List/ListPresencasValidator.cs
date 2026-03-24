using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.List;

public class ListPresencasValidator : AbstractValidator<ListPresencasInputDto>
{
    public ListPresencasValidator()
    {
        RuleFor(x => x)
            .NotNull();
    }
}
