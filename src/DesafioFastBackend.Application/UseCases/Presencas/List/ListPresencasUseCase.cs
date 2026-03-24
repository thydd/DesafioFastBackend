using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.List;

public class ListPresencasUseCase(
    IPresencaRepository repository,
    IValidator<ListPresencasInputDto> validator) : IListPresencasUseCase
{
    public async Task<IEnumerable<PresencaOutputDto>> ExecuteAsync(ListPresencasInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var presencas = await repository.GetAllAsync();

        return presencas.Select(x => new PresencaOutputDto
        {
            WorkshopId = x.WorkshopId,
            ColaboradorId = x.ColaboradorId,
            DataHoraCheckIn = x.DataHoraCheckIn,
            Status = x.Status
        }).ToList();
    }
}
