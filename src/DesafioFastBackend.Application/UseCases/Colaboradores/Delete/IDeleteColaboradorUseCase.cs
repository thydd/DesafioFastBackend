using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Delete;

public interface IDeleteColaboradorUseCase
{
    Task<bool> ExecuteAsync(DeleteColaboradorInputDto input);
}
