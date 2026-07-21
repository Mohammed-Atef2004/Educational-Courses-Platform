using Domain.Users;
using EducationalPlatform.Application.Features.Courses.Commands.CreateCourse;
using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Interfaces.Repositories;
using EducationalPlatform.Infrastructure.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Presistence.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using EducationalPlatform.Infrastructure;

namespace EducationalPlatform.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Database
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddInfrastructure();

        //FastEndpoints & Swagger
        builder.Services.AddFastEndpoints();
        builder.Services.SwaggerDocument(o =>
        {
            o.ShortSchemaNames = true;
        });

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCourseCommand).Assembly));

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseFastEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerGen(); 
        }

        app.Run();
    }
}