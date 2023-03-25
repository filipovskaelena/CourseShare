using CourseShare.Domain.Identity;
using CourseShare.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseShare.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<CourseShareApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CourseShareApplicationUser>();
        }
        public IEnumerable<CourseShareApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public CourseShareApplicationUser Get(string id)
        {
            return entities
               .Include(z => z.UserCourses)
               .Include("UserCourses.CoursesInMyCourses")
               .Include("UserCourses.CoursesInMyCourses.CurrentCourse")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(CourseShareApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(CourseShareApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(CourseShareApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
