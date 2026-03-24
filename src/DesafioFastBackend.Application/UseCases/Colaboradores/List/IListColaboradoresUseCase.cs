using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.List;

public interface IListColaboradoresUseCase
{
    Task<IEnumerable<ColaboradorOutputDto>> ExecuteAsync(ListColaboradoresInputDto input);
}
