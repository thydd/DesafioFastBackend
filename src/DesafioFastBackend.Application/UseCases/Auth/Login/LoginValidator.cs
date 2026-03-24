using DesafioFastBackend.Application.UseCases.Auth.Login.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application.UseCases.Auth.Login;

public class LoginValidator : AbstractValidator<LoginInputDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username é obrigatório.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password é obrigatório.");
    }
}
