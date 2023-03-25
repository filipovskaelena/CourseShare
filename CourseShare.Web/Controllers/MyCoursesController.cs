using CourseShare.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CourseShare.Web.Controllers
{
    public class MyCoursesController : Controller
    {
        private readonly IMyCoursesService _myCoursesService;

        public MyCoursesController(IMyCoursesService myCoursesService)
        {
            _myCoursesService = myCoursesService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._myCoursesService.getMyCoursesInfo(userId));
        }

        public IActionResult DeleteFromMyCourses(Guid id)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._myCoursesService.deleteCourseFromMyCourses(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "MyCourses");
            }
            else
            {
                return RedirectToAction("Index", "MyCourses");
            }
        }
    }
}
