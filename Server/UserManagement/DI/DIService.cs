using Server.Applications.UserManagement;
using Server.Applications.UserManagement.Abstract;
using Server.UserManagement.Domain.Abstract;
using Server.UserManagement.Application;
using Server.UserManagement.Domain;
using Server.UserManagement.Domain.User;

namespace Server.UserManagement.DI;

public static class DIService
{
    public static IServiceCollection AddUserManagement(this IServiceCollection services)
    {
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<IDtoAssembler, DtoAssembler>();
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserFactory, UserFactory>();

        return services;
    }
}