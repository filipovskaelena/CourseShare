using CourseShare.Domain.Identity;
using CourseShare.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseShare.Domain.DomainModels
{
    public class MyCourses: BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual CourseShareApplicationUser Owner { get; set; }

        public virtual ICollection<CourseInMyCourses> CoursesInMyCourses { get; set; }

        public MyCourses()
        {
        }
    }
}
