using CourseShare.Domain.DomainModels;
using CourseShare.Domain.DTO;
using CourseShare.Repository.Interface;
using CourseShare.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace CourseShare.Services.Implementation
{
    public class MyCoursesService : IMyCoursesService
    {
        private readonly IRepository<MyCourses> _myCoursesRepository;
        private readonly IUserRepository _userRepository;

        public MyCoursesService(IRepository<MyCourses> myCoursesRepository, IUserRepository userRepository)
        {
            _myCoursesRepository = myCoursesRepository;
            _userRepository = userRepository;
        }


        public bool deleteCourseFromMyCourses(string userId, Guid courseId)
        {
            if (!string.IsNullOrEmpty(userId) && courseId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCoursesCard = loggedInUser.UserCourses;

                var itemToDelete = userCoursesCard.CoursesInMyCourses.Where(z => z.CourseId.Equals(courseId)).FirstOrDefault();

                userCoursesCard.CoursesInMyCourses.Remove(itemToDelete);

                this._myCoursesRepository.Update(userCoursesCard);

                return true;
            }
            return false;
        }

        public MyCoursesDto getMyCoursesInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCourses;

                var allCourses = userCard.CoursesInMyCourses.ToList();

                var reuslt = new MyCoursesDto
                {
                    Courses = allCourses,
                };

                return reuslt;
            }
            return new MyCoursesDto();
        }
    }
}
