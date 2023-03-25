using CourseShare.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseShare.Domain.Relations
{
    public class CourseInMyCourses : BaseEntity
    {
        public Guid CourseId { get; set; }
        public virtual Course CurrentCourse { get; set; }

        public Guid MyCoursesId { get; set; }
        public virtual MyCourses UserCourses { get; set; }

    }
}
