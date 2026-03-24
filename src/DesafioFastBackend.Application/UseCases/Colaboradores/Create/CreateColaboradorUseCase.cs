using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Colaboradores.Create;

public class CreateColaboradorUseCase(
    IColaboradorRepository repository,
    IValidator<CreateColaboradorInputDto> validator) : ICreateColaboradorUseCase
{
    public async Task<ColaboradorOutputDto> ExecuteAsync(CreateColaboradorInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var entity = new Colaborador
        {
            Nome = input.Nome.Trim()
        };

        var created = await repository.CreateAsync(entity);

        return new ColaboradorOutputDto { Id = created.Id, Nome = created.Nome };
    }
}
