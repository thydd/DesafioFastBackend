using DesafioFastBackend.Application.UseCases.Workshops.Dtos;

namespace DesafioFastBackend.Application.UseCases.Workshops.List;

public interface IListWorkshopsUseCase
{
    Task<IEnumerable<WorkshopOutputDto>> ExecuteAsync(ListWorkshopsInputDto input);
}
