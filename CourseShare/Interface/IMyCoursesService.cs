using CourseShare.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseShare.Services.Interface
{
    public interface IMyCoursesService
    {
        MyCoursesDto getMyCoursesInfo(string userId);
        bool deleteCourseFromMyCourses(string userId, Guid courseId);
    }
}
