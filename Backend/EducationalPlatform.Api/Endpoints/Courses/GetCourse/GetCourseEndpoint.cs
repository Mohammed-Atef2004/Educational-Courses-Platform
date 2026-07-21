// 1. استيرادات نظيفة وصحيحة
using EducationalPlatform.Api.Endpoints.Courses.GetCourse;
using EducationalPlatform.Application.Features.Courses.Queries.GetCourseCatalog;
using FastEndpoints;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalPlatform.Api.Endpoints.Courses.GetCourse
{

    public class GetCourseEndpoint : Endpoint<GetCourseRequest, GetCourseResponse>
    {
        private readonly ISender _sender;

        public GetCourseEndpoint(ISender sender)
        {
            _sender = sender;
        }

        public override void Configure()
        {
            Get("/api/courses/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetCourseRequest req, CancellationToken ct)
        {
            var query = new GetCourseQuery(req.Id);
            var result = await _sender.Send(query, ct);

            if (result.IsFailure)
            {
                if (result.Error.Code == "Course.NotFound")
                {
                    await Send.NotFoundAsync(ct);
                    return;
                }

                AddError(result.Error.Message);
                await Send.ErrorsAsync(400, ct);
                return;
            }

            var apiResponse = new GetCourseResponse(
                result.Value.InstructorId,
                result.Value.Name,
                result.Value.Description,
                result.Value.Price,
                result.Value.ImageUrl,
                result.Value.VideoLink,
                result.Value.CourseStatus
            );

            await Send.OkAsync(apiResponse, ct);
        }
    }
}