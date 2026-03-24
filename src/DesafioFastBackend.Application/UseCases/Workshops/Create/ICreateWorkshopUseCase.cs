using DesafioFastBackend.Application.UseCases.Workshops.Dtos;

namespace DesafioFastBackend.Application.UseCases.Workshops.Create;

public interface ICreateWorkshopUseCase
{
    Task<WorkshopOutputDto> ExecuteAsync(CreateWorkshopInputDto input);
}
