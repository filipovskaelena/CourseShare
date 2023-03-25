using CourseShare.Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourseShare.Domain.DomainModels
{
    public class Course : BaseEntity
    {
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string CourseDescription { get; set; }
        [Required]
        public string CourseLink { get; set; }
        [Required]
        public string CourseLanguage { get; set; }
        [Required]
        public double CourseRating { get; set; }
        [Required]
        public string CourseQuiz { get; set; }
        [Required]
        public string AddedByUser { get; set; }


        public virtual ICollection<CourseInMyCourses> CoursesInMyCourses { get; set; }
    }
}
