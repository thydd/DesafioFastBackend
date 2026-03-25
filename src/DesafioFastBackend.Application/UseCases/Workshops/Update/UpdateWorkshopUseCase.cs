using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using DesafioFastBackend.Domain.Models;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.Update;

public class UpdateWorkshopUseCase(
    IWorkshopRepository repository,
    IValidator<UpdateWorkshopInputDto> validator) : IUpdateWorkshopUseCase
{
    public async Task<WorkshopOutputDto?> ExecuteAsync(UpdateWorkshopInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var entity = new Workshop
        {
            Id = input.Id,
            Nome = input.Nome.Trim(),
            Descricao = input.Descricao.Trim(),
            DataRealizacao = NormalizeBusinessDateTime(input.DataRealizacao)
        };

        var updated = await repository.UpdateAsync(entity);
        if (updated is null)
        {
            return null;
        }

        return new WorkshopOutputDto
        {
            Id = updated.Id,
            Nome = updated.Nome,
            Descricao = updated.Descricao,
            DataRealizacao = updated.DataRealizacao
        };
    }

    private static DateTime NormalizeBusinessDateTime(DateTime value)
    {
        return DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
    }
}
