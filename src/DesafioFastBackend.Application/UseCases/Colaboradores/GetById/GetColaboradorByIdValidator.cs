using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.GetById;

public class GetColaboradorByIdValidator : AbstractValidator<GetColaboradorByIdInputDto>
{
    public GetColaboradorByIdValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero.");
    }
}
