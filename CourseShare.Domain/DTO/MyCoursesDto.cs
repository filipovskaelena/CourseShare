using CourseShare.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseShare.Domain.DTO
{
    public class MyCoursesDto
    {
        public List<CourseInMyCourses> Courses { get; set; }
    }
}
