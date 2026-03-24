using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Delete;

public class DeleteColaboradorUseCase(
    IColaboradorRepository repository,
    IValidator<DeleteColaboradorInputDto> validator) : IDeleteColaboradorUseCase
{
    public async Task<bool> ExecuteAsync(DeleteColaboradorInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);
        return await repository.DeleteAsync(input.Id);
    }
}
