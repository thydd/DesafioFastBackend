using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.Delete;

public class DeletePresencaValidator : AbstractValidator<DeletePresencaInputDto>
{
    public DeletePresencaValidator()
    {
        RuleFor(x => x.WorkshopId)
            .GreaterThan(0).WithMessage("WorkshopId deve ser maior que zero.");

        RuleFor(x => x.ColaboradorId)
            .GreaterThan(0).WithMessage("ColaboradorId deve ser maior que zero.");
    }
}
