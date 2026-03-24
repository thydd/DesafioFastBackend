using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Update;

public class UpdateColaboradorUseCase(
    IColaboradorRepository repository,
    IValidator<UpdateColaboradorInputDto> validator) : IUpdateColaboradorUseCase
{
    public async Task<ColaboradorOutputDto?> ExecuteAsync(UpdateColaboradorInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var entity = new Colaborador
        {
            Id = input.Id,
            Nome = input.Nome.Trim()
        };

        var updated = await repository.UpdateAsync(entity);
        if (updated is null)
        {
            return null;
        }

        return new ColaboradorOutputDto { Id = updated.Id, Nome = updated.Nome };
    }
}
