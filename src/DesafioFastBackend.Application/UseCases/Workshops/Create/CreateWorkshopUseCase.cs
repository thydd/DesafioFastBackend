using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.Create;

public class CreateWorkshopUseCase(
    IWorkshopRepository repository,
    IValidator<CreateWorkshopInputDto> validator) : ICreateWorkshopUseCase
{
    public async Task<WorkshopOutputDto> ExecuteAsync(CreateWorkshopInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var entity = new Workshop
        {
            Nome = input.Nome.Trim(),
            Descricao = input.Descricao.Trim(),
            DataRealizacao = NormalizeBusinessDateTime(input.DataRealizacao)
        };

        var created = await repository.CreateAsync(entity);

        return new WorkshopOutputDto
        {
            Id = created.Id,
            Nome = created.Nome,
            Descricao = created.Descricao,
            DataRealizacao = created.DataRealizacao
        };
    }

    private static DateTime NormalizeBusinessDateTime(DateTime value)
    {
        return DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
    }
}
