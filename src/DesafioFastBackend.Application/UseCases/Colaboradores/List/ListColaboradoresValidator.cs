using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.List;

public class ListColaboradoresValidator : AbstractValidator<ListColaboradoresInputDto>
{
    public ListColaboradoresValidator()
    {
        RuleFor(x => x)
            .NotNull();
    }
}
