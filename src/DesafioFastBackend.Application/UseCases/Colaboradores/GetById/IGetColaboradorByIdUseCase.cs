using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.GetById;

public interface IGetColaboradorByIdUseCase
{
    Task<ColaboradorOutputDto?> ExecuteAsync(GetColaboradorByIdInputDto input);
}
