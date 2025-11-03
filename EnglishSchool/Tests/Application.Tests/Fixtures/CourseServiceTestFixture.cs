using Application.DTOs.Course;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Fixtures;

public class CourseServiceTestFixture
{
    public CourseServiceTestFixture()
    {
        CourseService = new CourseService(
            MockUnitOfWork.Object,
            MockCourseRepository.Object,
            MockMapper.Object,
            MockLogger.Object);
    }

    public Mock<IUnitOfWork> MockUnitOfWork { get; } = new();

    public Mock<ICourseRepository> MockCourseRepository { get; } = new();

    public Mock<IMapper> MockMapper { get; } = new();

    public Mock<ILogger<CourseService>> MockLogger { get; } = new();

    public CourseService CourseService { get; }

    public void ResetMocks()
    {
        MockUnitOfWork.Reset();
        MockCourseRepository.Reset();
        MockMapper.Reset();
    }

    public static Course CreateSampleCourse()
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Title = "Test Course",
            Description = "Test Description",
            Price = 99.99,
            NumberOfLessons = 10,
            Lessons = [],
        };
    }

    public static CreateCourseDto CreateSampleCreateCourseDto()
    {
        return new CreateCourseDto
        {
            Title = "New Test Course",
            Description = "New Test Description",
            Price = 149.99,
            NumberOfLessons = 15,
        };
    }

    public static CourseDto CreateSampleCourseDto()
    {
        return new CourseDto
        {
            Id = Guid.NewGuid(),
            Title = "Test Course DTO",
            Description = "Test Description DTO",
            Price = 199.99,
            NumberOfLessons = 20,
        };
    }

    public static UpdateCourseDto CreateSampleUpdateCourseDto(Guid id)
    {
        return new UpdateCourseDto
        {
            Id = id,
            Title = "Updated Test Course",
            Description = "Updated Test Description",
            Price = 249.99,
            NumberOfLessons = 25,
        };
    }
}
