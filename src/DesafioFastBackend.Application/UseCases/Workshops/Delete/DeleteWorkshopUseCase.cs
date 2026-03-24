using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Workshops.Delete;

public class DeleteWorkshopUseCase(
    IWorkshopRepository repository,
    IValidator<DeleteWorkshopInputDto> validator) : IDeleteWorkshopUseCase
{
    public async Task<bool> ExecuteAsync(DeleteWorkshopInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);
        return await repository.DeleteAsync(input.Id);
    }
}
