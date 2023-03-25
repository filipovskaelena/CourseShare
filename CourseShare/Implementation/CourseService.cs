using CourseShare.Domain.DomainModels;
using CourseShare.Domain.DTO;
using CourseShare.Domain.Relations;
using CourseShare.Repository.Interface;
using CourseShare.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseShare.Services.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<CourseInMyCourses> _courseInMyCoursesRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(IRepository<Course> courseRepository, IRepository<CourseInMyCourses> courseInMyCoursesRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _courseInMyCoursesRepository = courseInMyCoursesRepository;
        }


        public bool AddToMyCourses(AddToMyCoursesDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userCoursesCard = user.UserCourses;

            if (item.SelectedCourseId != null && userCoursesCard != null)
            {
                var course = this.GetDetailsForCourse(item.SelectedCourseId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (course != null)
                {
                    CourseInMyCourses itemToAdd = new CourseInMyCourses
                    {
                        Id = Guid.NewGuid(),
                        CurrentCourse = course,
                        CourseId = course.Id,
                        UserCourses = userCoursesCard,
                        MyCoursesId = userCoursesCard.Id,
                    };

                    var existing = userCoursesCard.CoursesInMyCourses.Where(z => z.MyCoursesId == userCoursesCard.Id && z.CourseId == itemToAdd.CourseId).FirstOrDefault();

                    if (existing != null)
                    {
                        this._courseInMyCoursesRepository.Update(existing);

                    }
                    else
                    {
                        this._courseInMyCoursesRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewCourse(Course p)
        {
            this._courseRepository.Insert(p);
        }

        public void DeleteCourse(Guid id)
        {
            var course = this.GetDetailsForCourse(id);
            this._courseRepository.Delete(course);
        }

        public List<Course> GetAllCourses()
        {
            return this._courseRepository.GetAll().ToList();
        }

        public Course GetDetailsForCourse(Guid? id)
        {
            return this._courseRepository.Get(id);
        }

        public AddToMyCoursesDto GetMyCoursesInfo(Guid? id)
        {
            var course = this.GetDetailsForCourse(id);
            AddToMyCoursesDto model = new AddToMyCoursesDto
            {
                SelectedCourse = course,
                SelectedCourseId = course.Id,
            };

            return model;
        }

        public void UpdeteExistingCourse(Course p)
        {
            this._courseRepository.Update(p);
        }
    }
}
