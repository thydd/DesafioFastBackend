using DesafioFastBackend.Application.UseCases.Workshops.Dtos;

namespace DesafioFastBackend.Application.UseCases.Workshops.Delete;

public interface IDeleteWorkshopUseCase
{
    Task<bool> ExecuteAsync(DeleteWorkshopInputDto input);
}
