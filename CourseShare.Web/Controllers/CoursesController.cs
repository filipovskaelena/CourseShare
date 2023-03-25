using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseShare.Domain.DomainModels;
using CourseShare.Domain.DTO;
using CourseShare.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseShare.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ILogger<CoursesController> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        // GET: Products
        public IActionResult Index()
        {
            _logger.LogInformation("User Request -> Get All courses!");
            return View(this._courseService.GetAllCourses());
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            _logger.LogInformation("User Request -> Get Details For Course");
            if (id == null)
            {
                return NotFound();
            }

            var course = this._courseService.GetDetailsForCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            _logger.LogInformation("User Request -> Get create form for Course!");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CourseName,CourseDescription,CourseLink,CourseLanguage, CourseRating, CourseQuiz, AddedByUser")] Course course)
        {
            _logger.LogInformation("User Request -> Insert Course in DataBase!");
            if (ModelState.IsValid)
            {
                course.Id = Guid.NewGuid();
                this._courseService.CreateNewCourse(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid? id)
        {
            _logger.LogInformation("User Request -> Get edit form for Course!");
            if (id == null)
            {
                return NotFound();
            }

            var product = this._courseService.GetDetailsForCourse(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,CourseName,CourseDescription,CourseLink,CourseLanguage, CourseRating, CourseQuiz, AddedByUser")] Course course)
        {
            _logger.LogInformation("User Request -> Update Course in DataBase!");

            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._courseService.UpdeteExistingCourse(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            _logger.LogInformation("User Request -> Get delete form for Course!");

            if (id == null)
            {
                return NotFound();
            }

            var product = this._courseService.GetDetailsForCourse(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _logger.LogInformation("User Request -> Delete Course in DataBase!");

            this._courseService.DeleteCourse(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddCourseToMyCourses(Guid id)
        {
            var result = this._courseService.GetMyCoursesInfo(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourseToMyCourses(AddToMyCoursesDto model)
        {

            _logger.LogInformation("User Request -> Add Course in MyCourses and save changes in database!");


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._courseService.AddToMyCourses(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Courses");
            }
            return View(model);
        }
        private bool CourseExists(Guid id)
        {
            return this._courseService.GetDetailsForCourse(id) != null;
        }
    }
}
