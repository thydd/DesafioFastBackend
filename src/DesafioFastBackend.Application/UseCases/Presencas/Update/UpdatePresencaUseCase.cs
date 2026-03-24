using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.Update;

public class UpdatePresencaUseCase(
    IPresencaRepository repository,
    IValidator<UpdatePresencaInputDto> validator) : IUpdatePresencaUseCase
{
    public async Task<PresencaOutputDto?> ExecuteAsync(UpdatePresencaInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var entity = new Presenca
        {
            WorkshopId = input.WorkshopId,
            ColaboradorId = input.ColaboradorId,
            DataHoraCheckIn = input.DataHoraCheckIn,
            Status = input.Status
        };

        var updated = await repository.UpdateAsync(entity);
        if (updated is null)
        {
            return null;
        }

        return new PresencaOutputDto
        {
            WorkshopId = updated.WorkshopId,
            ColaboradorId = updated.ColaboradorId,
            DataHoraCheckIn = updated.DataHoraCheckIn,
            Status = updated.Status
        };
    }
}
