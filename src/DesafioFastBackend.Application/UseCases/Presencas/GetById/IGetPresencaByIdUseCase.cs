using DesafioFastBackend.Application.UseCases.Presencas.Dtos;

namespace DesafioFastBackend.Application.UseCases.Presencas.GetById;

public interface IGetPresencaByIdUseCase
{
    Task<PresencaOutputDto?> ExecuteAsync(GetPresencaByIdInputDto input);
}
