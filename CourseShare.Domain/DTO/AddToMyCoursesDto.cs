using CourseShare.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseShare.Domain.DTO
{
    public class AddToMyCoursesDto
    {
        public Course SelectedCourse { get; set; }
        public Guid SelectedCourseId { get; set; }
    }
}
