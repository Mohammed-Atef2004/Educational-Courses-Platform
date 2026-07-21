using EducationalPlatform.Application.Features.Courses.Commands.CreateCourse;
using FastEndpoints;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace EducationalPlatform.Api.Endpoints.Courses.CreateCourse;

public sealed class CreateCourseEndpoint : Endpoint<CreateCourseRequest, CreateCourseResponse>
{
    private readonly ISender _sender;

    public CreateCourseEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("/api/courses");

        // Temporarily 
        // Once Auth is wired up, replace this with Roles("Instructor");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateCourseRequest req, CancellationToken ct)
    {
        // For testing 
        // var instructorId = Guid.Parse(User.FindFirst("id")?.Value ?? throw new UnauthorizedAccessException());
        var instructorId = Guid.NewGuid();

        var command = new CreateCourseCommand(
            instructorId,
            req.Name,
            req.Description,
            req.Price,
            req.Currency,
            req.ImageUrl,
            req.VideoLink
        );

        var result = await _sender.Send(command, ct);

        if (result.IsFailure)
        {
            AddError(result.Error.Message);
            await Send.ErrorsAsync(400, ct);
            return;
        }
        await Send.ResponseAsync(new CreateCourseResponse(result.Value), 201, ct);
    }
}