using DesafioFastBackend.API.Contracts;
using DesafioFastBackend.Application.UseCases.Auth.Login;
using DesafioFastBackend.Application.UseCases.Auth.Login.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioFastBackend.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(ILoginUseCase loginUseCase) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginOutputDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LoginOutputDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<LoginOutputDto>>> LoginAsync([FromBody] LoginInputDto input)
    {
        var output = await loginUseCase.ExecuteAsync(input);
        if (output is null)
        {
            return Unauthorized(ApiResponse<LoginOutputDto>.Fail("Usuário ou senha inválidos."));
        }

        return Ok(ApiResponse<LoginOutputDto>.Ok(output, "Autenticação realizada com sucesso."));
    }
}
