using Microsoft.Extensions.DependencyInjection;
using DesafioFastBackend.Application.UseCases.Colaboradores.Create;
using DesafioFastBackend.Application.UseCases.Colaboradores.Delete;
using DesafioFastBackend.Application.UseCases.Colaboradores.Dtos;
using DesafioFastBackend.Application.UseCases.Colaboradores.GetById;
using DesafioFastBackend.Application.UseCases.Colaboradores.List;
using DesafioFastBackend.Application.UseCases.Colaboradores.Update;
using DesafioFastBackend.Application.UseCases.Workshops.Create;
using DesafioFastBackend.Application.UseCases.Workshops.Delete;
using DesafioFastBackend.Application.UseCases.Workshops.Dtos;
using DesafioFastBackend.Application.UseCases.Workshops.GetById;
using DesafioFastBackend.Application.UseCases.Workshops.List;
using DesafioFastBackend.Application.UseCases.Workshops.Update;
using DesafioFastBackend.Application.UseCases.Presencas.Create;
using DesafioFastBackend.Application.UseCases.Presencas.Delete;
using DesafioFastBackend.Application.UseCases.Presencas.Dtos;
using DesafioFastBackend.Application.UseCases.Presencas.GetById;
using DesafioFastBackend.Application.UseCases.Presencas.List;
using DesafioFastBackend.Application.UseCases.Presencas.Update;
using DesafioFastBackend.Application.UseCases.Auth.Login;
using DesafioFastBackend.Application.UseCases.Auth.Login.Dtos;
using FluentValidation;

namespace DesafioFastBackend.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateColaboradorInputDto>, CreateColaboradorValidator>();
        services.AddScoped<IValidator<DeleteColaboradorInputDto>, DeleteColaboradorValidator>();
        services.AddScoped<IValidator<GetColaboradorByIdInputDto>, GetColaboradorByIdValidator>();
        services.AddScoped<IValidator<ListColaboradoresInputDto>, ListColaboradoresValidator>();
        services.AddScoped<IValidator<UpdateColaboradorInputDto>, UpdateColaboradorValidator>();
        services.AddScoped<IValidator<CreateWorkshopInputDto>, CreateWorkshopValidator>();
        services.AddScoped<IValidator<DeleteWorkshopInputDto>, DeleteWorkshopValidator>();
        services.AddScoped<IValidator<GetWorkshopByIdInputDto>, GetWorkshopByIdValidator>();
        services.AddScoped<IValidator<ListWorkshopsInputDto>, ListWorkshopsValidator>();
        services.AddScoped<IValidator<UpdateWorkshopInputDto>, UpdateWorkshopValidator>();
        services.AddScoped<IValidator<CreatePresencaInputDto>, CreatePresencaValidator>();
        services.AddScoped<IValidator<DeletePresencaInputDto>, DeletePresencaValidator>();
        services.AddScoped<IValidator<GetPresencaByIdInputDto>, GetPresencaByIdValidator>();
        services.AddScoped<IValidator<ListPresencasInputDto>, ListPresencasValidator>();
        services.AddScoped<IValidator<UpdatePresencaInputDto>, UpdatePresencaValidator>();
        services.AddScoped<IValidator<LoginInputDto>, LoginValidator>();

        services.AddScoped<ICreateColaboradorUseCase, CreateColaboradorUseCase>();
        services.AddScoped<IDeleteColaboradorUseCase, DeleteColaboradorUseCase>();
        services.AddScoped<IGetColaboradorByIdUseCase, GetColaboradorByIdUseCase>();
        services.AddScoped<IListColaboradoresUseCase, ListColaboradoresUseCase>();
        services.AddScoped<IUpdateColaboradorUseCase, UpdateColaboradorUseCase>();
        services.AddScoped<ICreateWorkshopUseCase, CreateWorkshopUseCase>();
        services.AddScoped<IDeleteWorkshopUseCase, DeleteWorkshopUseCase>();
        services.AddScoped<IGetWorkshopByIdUseCase, GetWorkshopByIdUseCase>();
        services.AddScoped<IListWorkshopsUseCase, ListWorkshopsUseCase>();
        services.AddScoped<IUpdateWorkshopUseCase, UpdateWorkshopUseCase>();
        services.AddScoped<ICreatePresencaUseCase, CreatePresencaUseCase>();
        services.AddScoped<IDeletePresencaUseCase, DeletePresencaUseCase>();
        services.AddScoped<IGetPresencaByIdUseCase, GetPresencaByIdUseCase>();
        services.AddScoped<IListPresencasUseCase, ListPresencasUseCase>();
        services.AddScoped<IUpdatePresencaUseCase, UpdatePresencaUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();

        return services;
    }
}
