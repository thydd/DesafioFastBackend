using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Domain.Exceptions;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.Create;

public class CreatePresencaUseCase(
    IPresencaRepository repository,
    IValidator<CreatePresencaInputDto> validator) : ICreatePresencaUseCase
{
    public async Task<PresencaOutputDto> ExecuteAsync(CreatePresencaInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var existing = await repository.GetByIdAsync(input.WorkshopId, input.ColaboradorId);
        if (existing is not null)
        {
            throw new ConflictException("Já existe presença registrada para este colaborador neste workshop.");
        }

        var entity = new Presenca
        {
            WorkshopId = input.WorkshopId,
            ColaboradorId = input.ColaboradorId,
            DataHoraCheckIn = input.DataHoraCheckIn,
            Status = input.Status
        };

        var created = await repository.CreateAsync(entity);

        return new PresencaOutputDto
        {
            WorkshopId = created.WorkshopId,
            ColaboradorId = created.ColaboradorId,
            DataHoraCheckIn = created.DataHoraCheckIn,
            Status = created.Status
        };
    }
}
