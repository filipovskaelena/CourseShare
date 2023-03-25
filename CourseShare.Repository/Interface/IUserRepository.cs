using CourseShare.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseShare.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<CourseShareApplicationUser> GetAll();
        CourseShareApplicationUser Get(string id);
        void Insert(CourseShareApplicationUser entity);
        void Update(CourseShareApplicationUser entity);
        void Delete(CourseShareApplicationUser entity);
    }
}
