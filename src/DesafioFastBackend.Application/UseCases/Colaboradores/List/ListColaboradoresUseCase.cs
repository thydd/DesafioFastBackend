using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.List;

public class ListColaboradoresUseCase(
    IColaboradorRepository repository,
    IValidator<ListColaboradoresInputDto> validator) : IListColaboradoresUseCase
{
    public async Task<IEnumerable<ColaboradorOutputDto>> ExecuteAsync(ListColaboradoresInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var colaboradores = await repository.GetAllAsync();

        return colaboradores
            .Select(x => new ColaboradorOutputDto { Id = x.Id, Nome = x.Nome })
            .ToList();
    }
}
