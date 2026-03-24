using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Update;

public interface IUpdateColaboradorUseCase
{
    Task<ColaboradorOutputDto?> ExecuteAsync(UpdateColaboradorInputDto input);
}
