using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.Create;

public class CreatePresencaValidator : AbstractValidator<CreatePresencaInputDto>
{
    public CreatePresencaValidator()
    {
        RuleFor(x => x.WorkshopId)
            .GreaterThan(0).WithMessage("WorkshopId deve ser maior que zero.");

        RuleFor(x => x.ColaboradorId)
            .GreaterThan(0).WithMessage("ColaboradorId deve ser maior que zero.");

        RuleFor(x => x.DataHoraCheckIn)
            .NotEqual(default(DateTime)).WithMessage("DataHoraCheckIn é obrigatória.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status inválido.");
    }
}
