using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // Existing action to display the list of user profiles
        public IActionResult Index()
        {
            var userProfiles = _userProfileRepository.GetAllUserProfiles();
            return View(userProfiles);
        }

        // GET: UserProfile/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: UserProfile/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserProfileCreateViewModel vm)
        {
            try
            {
                var userProfile = new UserProfile
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    DisplayName = vm.DisplayName,
                    Email = vm.Email,
                    CreateDateTime = DateTime.Now,
                    UserTypeId = 2 // Assuming 2 is the ID for 'Author'
                };

                _userProfileRepository.Add(userProfile);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(vm);
            }
        }
    }
}

