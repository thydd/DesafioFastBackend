using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.Update;

public class UpdatePresencaValidator : AbstractValidator<UpdatePresencaInputDto>
{
    private static readonly TimeZoneInfo WorkshopTimeZone = ResolveWorkshopTimeZone();

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
        var inputTime = checkIn.TimeOfDay;
        if (IsWithinWorkshopWindow(inputTime))
        {
            return true;
        }

        var normalizedTime = NormalizeToWorkshopTime(checkIn).TimeOfDay;
        return IsWithinWorkshopWindow(normalizedTime);
    }

    private static bool IsWithinWorkshopWindow(TimeSpan time)
    {
        return time >= new TimeSpan(16, 0, 0) && time <= new TimeSpan(17, 0, 0);
    }

    private static DateTime NormalizeToWorkshopTime(DateTime checkIn)
    {
        if (checkIn.Kind == DateTimeKind.Unspecified)
        {
            var assumedUtc = DateTime.SpecifyKind(checkIn, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(assumedUtc, WorkshopTimeZone);
        }

        var utc = checkIn.Kind == DateTimeKind.Utc
            ? checkIn
            : checkIn.ToUniversalTime();

        return TimeZoneInfo.ConvertTimeFromUtc(utc, WorkshopTimeZone);
    }

    private static TimeZoneInfo ResolveWorkshopTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }
    }
}
