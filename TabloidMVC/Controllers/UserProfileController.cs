using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
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
        public IActionResult Index()
        {
            var userProfiles = _userProfileRepository.GetAllUserProfiles();
            var activeProfiles = userProfiles.Where(p => p.IsActive).ToList();

            return View(activeProfiles);
        }

        public IActionResult Details(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(id);
            return View(userProfile);
        }

        public ActionResult Edit(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfile userProfile)
        {
            try
            {
                _userProfileRepository.SwapActivationStatus(userProfile);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
