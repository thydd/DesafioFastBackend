using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Create;

public interface ICreateColaboradorUseCase
{
    Task<ColaboradorOutputDto> ExecuteAsync(CreateColaboradorInputDto input);
}
