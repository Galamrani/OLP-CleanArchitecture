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
        User? user = await userDAO.GetUserByIdAsync(userId);
        if (user is null) throw new KeyNotFoundException($"User with ID {userId} was not found.");

        Course? course = await courseDAO.GetBasicCourseAsync(courseId);
        if (course is null) throw new KeyNotFoundException($"Course with ID {courseId} was not found.");

        user.Enroll(courseId);

        await unitOfWork.SaveChangesAsync();
        return mapper.Map<CourseDto>(course);
    }

    public async Task UnenrollToCourseAsync(Guid userId, Guid courseId)
    {
        User? user = await userDAO.GetUserByIdAsync(userId);
        if (user is null) throw new KeyNotFoundException($"User with ID {userId} was not found.");

        Enrollment enrollment = user.RemoveEnrollment(courseId);
        await userDAO.RemoveEnrollment(enrollment);
        await unitOfWork.SaveChangesAsync();
    }
}