using CourseShare.Domain.DomainModels;
using CourseShare.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseShare.Services.Interface
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();
        Course GetDetailsForCourse(Guid? id);
        void CreateNewCourse(Course p);
        void UpdeteExistingCourse(Course p);
        AddToMyCoursesDto GetMyCoursesInfo(Guid? id);
        void DeleteCourse(Guid id);
        bool AddToMyCourses(AddToMyCoursesDto item, string userID);
    }
}
