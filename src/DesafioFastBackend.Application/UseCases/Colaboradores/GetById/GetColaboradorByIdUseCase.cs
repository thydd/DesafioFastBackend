using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.GetById;

public class GetColaboradorByIdUseCase(
    IColaboradorRepository repository,
    IValidator<GetColaboradorByIdInputDto> validator) : IGetColaboradorByIdUseCase
{
    public async Task<ColaboradorOutputDto?> ExecuteAsync(GetColaboradorByIdInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var colaborador = await repository.GetByIdAsync(input.Id);
        if (colaborador is null)
        {
            return null;
        }

        return new ColaboradorOutputDto { Id = colaborador.Id, Nome = colaborador.Nome };
    }
}
