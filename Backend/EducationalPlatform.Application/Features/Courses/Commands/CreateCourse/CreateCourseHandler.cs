using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Interfaces.Repositories;
using EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Courses.Commands.CreateCourse
{
    public sealed class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Result<Guid>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCourseHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
           
            var courseResult = Course.Create(
                request.Name, request.InstructorId, request.Description,
                request.Price, request.Currency, request.ImageUrl, request.VideoLink);

            if (courseResult.IsFailure)
                return Result<Guid>.Failure(courseResult.Error);

            var course = courseResult.Value;
            var isNameTaken = await _courseRepository.ExistsByNameAsync(course.Name, cancellationToken);
            if (isNameTaken)
                return Result<Guid>.Failure(new Error("Course name is already taken.", "CourseNameTaken"));

            await _courseRepository.AddAsync(course, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return Result<Guid>.Success(course.Id.Value);
        }
    }

}
