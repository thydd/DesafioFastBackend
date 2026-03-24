using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.List;

public class ListWorkshopsUseCase(
    IWorkshopRepository repository,
    IValidator<ListWorkshopsInputDto> validator) : IListWorkshopsUseCase
{
    public async Task<IEnumerable<WorkshopOutputDto>> ExecuteAsync(ListWorkshopsInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var workshops = await repository.GetAllAsync();

        return workshops.Select(x => new WorkshopOutputDto
        {
            Id = x.Id,
            Nome = x.Nome,
            Descricao = x.Descricao,
            DataRealizacao = x.DataRealizacao
        }).ToList();
    }
}
