using Domain.Interfaces.Services;
using EducationalPlatform.Application.Features.Users.Services;
using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Interfaces.Repositories;
using EducationalPlatform.Domain.Users;
using EducationalPlatform.Infrastructure.Repositories;
using EducationalPlatform.Infrastructure.Services.Token;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Shared;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EducationalPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register  repositories 
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        services.AddScoped<ITotpService, TotpService>();
        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<ITokenService, TokenService>();


        return services;
    }
}