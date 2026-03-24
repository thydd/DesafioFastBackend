using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Delete;

public class DeleteColaboradorValidator : AbstractValidator<DeleteColaboradorInputDto>
{
    public DeleteColaboradorValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id deve ser maior que zero.");
    }
}
