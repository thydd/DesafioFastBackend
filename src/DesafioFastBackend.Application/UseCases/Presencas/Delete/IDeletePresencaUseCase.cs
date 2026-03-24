using DesafioFastBackend.Application.UseCases.Presencas.Dtos;

namespace DesafioFastBackend.Application.UseCases.Presencas.Delete;

public interface IDeletePresencaUseCase
{
    Task<bool> ExecuteAsync(DeletePresencaInputDto input);
}
