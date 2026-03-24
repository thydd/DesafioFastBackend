using DesafioFastBackend.Application.UseCases.Auth.Login.Dtos;

namespace DesafioFastBackend.Application.UseCases.Auth.Login;

public interface ILoginUseCase
{
    Task<LoginOutputDto?> ExecuteAsync(LoginInputDto input);
}
