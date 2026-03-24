using DesafioFastBackend.Application.UseCases.Presencas.Dtos;

namespace DesafioFastBackend.Application.UseCases.Presencas.List;

public interface IListPresencasUseCase
{
    Task<IEnumerable<PresencaOutputDto>> ExecuteAsync(ListPresencasInputDto input);
}
