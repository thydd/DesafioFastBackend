using DesafioFastBackend.Application.UseCases.Workshops.Dtos;

namespace DesafioFastBackend.Application.UseCases.Workshops.GetById;

public interface IGetWorkshopByIdUseCase
{
    Task<WorkshopOutputDto?> ExecuteAsync(GetWorkshopByIdInputDto input);
}
