using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.GetById;

public class GetPresencaByIdUseCase(
    IPresencaRepository repository,
    IValidator<GetPresencaByIdInputDto> validator) : IGetPresencaByIdUseCase
{
    public async Task<PresencaOutputDto?> ExecuteAsync(GetPresencaByIdInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var presenca = await repository.GetByIdAsync(input.WorkshopId, input.ColaboradorId);
        if (presenca is null)
        {
            return null;
        }

        return new PresencaOutputDto
        {
            WorkshopId = presenca.WorkshopId,
            ColaboradorId = presenca.ColaboradorId,
            DataHoraCheckIn = presenca.DataHoraCheckIn,
            Status = presenca.Status
        };
    }
}
