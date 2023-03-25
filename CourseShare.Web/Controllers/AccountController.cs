using CourseShare.Domain.DomainModels;
using CourseShare.Domain.DTO;
using CourseShare.Domain.Identity;
using CourseShare.Domain.Relations;
using CourseShare.Repository.Interface;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseShare.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CourseShareApplicationUser> _userManager;
        private readonly SignInManager<CourseShareApplicationUser> _signInManager;
        private readonly IUserRepository _userRepository;
        public AccountController(UserManager<CourseShareApplicationUser> userManager, SignInManager<CourseShareApplicationUser> signInManager, IUserRepository userRepository)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            UserRegistrationDto model = new UserRegistrationDto();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            if (ModelState.IsValid)
            {
                bool status = true;
                var userCheck = _userManager.FindByEmailAsync(request.Email).Result;
                if (userCheck == null)
                {
                    var user = new CourseShareApplicationUser
                    {
                        FirstName = request.Name,
                        LastName = request.LastName,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = request.PhoneNumber,
                        UserCourses = new MyCourses()
                    };
                    var result = await _userManager.CreateAsync(user, request.Password);

                    status = status & result.Succeeded;

                    if (status)
                    {

                        //user.UserCourses = new MyCourses();
                        //IEnumerable<CourseShareApplicationUser> allUsers = _userRepository.GetAll();
                        //foreach(CourseShareApplicationUser item in allUsers)
                        //{
                        //    item.UserCourses = new MyCourses();
                        //}
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("message", "Email already exists.");
                return View(request);
            }
            return View(request);
        }
            

        

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            UserLoginDto model = new UserLoginDto();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await _userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
