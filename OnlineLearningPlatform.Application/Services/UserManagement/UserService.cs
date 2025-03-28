using AutoMapper;
using OnlineLearningPlatform.Application.DTOs;
using OnlineLearningPlatform.Application.Interfaces;
using OnlineLearningPlatform.Domain.Entities;

namespace OnlineLearningPlatform.Application.Services.UserManagement;

public class UserService(IUserDAO userDAO, ICourseDAO courseDAO, IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    private readonly IUserDAO userDAO = userDAO;
    private readonly ICourseDAO courseDAO = courseDAO;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<CourseDto> EnrollToCourseAsync(Guid userId, Guid courseId)
    {
        User user = await userDAO.GetUserByIdAsync(userId);
        if (user.Enrollments.Any(e => e.CourseId == courseId)) throw new InvalidOperationException("User is already enrolled in this course.");

        Course? course = await courseDAO.GetBasicCourseAsync(courseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        Enrollment enrollment = new Enrollment
        {
            UserId = userId,
            CourseId = courseId
        };

        user.Enrollments.Add(enrollment);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<CourseDto>(course);
    }

    public async Task UnenrollToCourseAsync(Guid userId, Guid courseId)
    {
        User user = await userDAO.GetUserByIdAsync(userId);
        Enrollment? enrollment = user.Enrollments.FirstOrDefault(e => e.CourseId == courseId);

        if (enrollment is null) throw new InvalidOperationException("User is not enrolled to this course.");

        user.Enrollments.Remove(enrollment);
        await unitOfWork.SaveChangesAsync();
    }
}