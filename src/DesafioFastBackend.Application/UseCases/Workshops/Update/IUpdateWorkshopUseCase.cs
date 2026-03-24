using DesafioFastBackend.Application.UseCases.Workshops.Dtos;

namespace DesafioFastBackend.Application.UseCases.Workshops.Update;

public interface IUpdateWorkshopUseCase
{
    Task<WorkshopOutputDto?> ExecuteAsync(UpdateWorkshopInputDto input);
}
