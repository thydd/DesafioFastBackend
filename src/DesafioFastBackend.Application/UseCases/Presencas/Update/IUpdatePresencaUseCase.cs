using DesafioFastBackend.Application.UseCases.Presencas.Dtos;

namespace DesafioFastBackend.Application.UseCases.Presencas.Update;

public interface IUpdatePresencaUseCase
{
    Task<PresencaOutputDto?> ExecuteAsync(UpdatePresencaInputDto input);
}
