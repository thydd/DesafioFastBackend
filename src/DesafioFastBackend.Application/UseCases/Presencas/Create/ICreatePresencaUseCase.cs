using DesafioFastBackend.Application.UseCases.Presencas.Dtos;

namespace DesafioFastBackend.Application.UseCases.Presencas.Create;

public interface ICreatePresencaUseCase
{
    Task<PresencaOutputDto> ExecuteAsync(CreatePresencaInputDto input);
}
