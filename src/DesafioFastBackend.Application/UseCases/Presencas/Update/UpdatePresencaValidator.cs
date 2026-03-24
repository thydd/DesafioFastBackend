using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.Update;

public class UpdatePresencaValidator : AbstractValidator<UpdatePresencaInputDto>
{
    public UpdatePresencaValidator()
    {
        RuleFor(x => x.WorkshopId)
            .GreaterThan(0).WithMessage("WorkshopId deve ser maior que zero.");

        RuleFor(x => x.ColaboradorId)
            .GreaterThan(0).WithMessage("ColaboradorId deve ser maior que zero.");

        RuleFor(x => x.DataHoraCheckIn)
            .NotEqual(default(DateTime)).WithMessage("DataHoraCheckIn é obrigatória.")
            .Must(BeWithinWorkshopWindow).WithMessage("DataHoraCheckIn deve estar entre 16:00 e 17:00.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status inválido.");
    }

    private static bool BeWithinWorkshopWindow(DateTime checkIn)
    {
        var time = checkIn.TimeOfDay;
        return time >= new TimeSpan(16, 0, 0) && time <= new TimeSpan(17, 0, 0);
    }
}
