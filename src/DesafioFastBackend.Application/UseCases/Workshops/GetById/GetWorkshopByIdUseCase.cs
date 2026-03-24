using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.GetById;

public class GetWorkshopByIdUseCase(
    IWorkshopRepository repository,
    IValidator<GetWorkshopByIdInputDto> validator) : IGetWorkshopByIdUseCase
{
    public async Task<WorkshopOutputDto?> ExecuteAsync(GetWorkshopByIdInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var workshop = await repository.GetByIdAsync(input.Id);
        if (workshop is null)
        {
            return null;
        }

        return new WorkshopOutputDto
        {
            Id = workshop.Id,
            Nome = workshop.Nome,
            Descricao = workshop.Descricao,
            DataRealizacao = workshop.DataRealizacao
        };
    }
}
