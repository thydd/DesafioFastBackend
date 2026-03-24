using DesafioFastBackend.Application.UseCases.Auth.Login.Dtos;
using DesafioFastBackend.Domain.Interfaces.Auth;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Auth.Login;

public class LoginUseCase(
    IAuthUserRepository authUserRepository,
    ITokenService tokenService,
    IValidator<LoginInputDto> validator) : ILoginUseCase
{
    public async Task<LoginOutputDto?> ExecuteAsync(LoginInputDto input)
    {
        await validator.ValidateAndThrowAsync(input);

        var user = await authUserRepository.GetByCredentialsAsync(input.Username, input.Password);
        if (user is null)
        {
            return null;
        }

        var token = tokenService.GenerateToken(user);

        return new LoginOutputDto
        {
            AccessToken = token,
            Username = user.Username,
            Role = user.Role
        };
    }
}
