using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View(userProfiles);
        }

        public IActionResult Details(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(id);
            return View(userProfile);
        }
    }
}
