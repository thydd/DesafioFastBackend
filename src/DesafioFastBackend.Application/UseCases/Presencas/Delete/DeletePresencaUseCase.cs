using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Domain.Interfaces.Repositories;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Presencas.Delete;

public class DeletePresencaUseCase(
    IPresencaRepository repository,
    IValidator<DeletePresencaInputDto> validator) : IDeletePresencaUseCase
{
    public async Task<bool> ExecuteAsync(DeletePresencaInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);
        return await repository.DeleteAsync(input.WorkshopId, input.ColaboradorId);
    }
}
