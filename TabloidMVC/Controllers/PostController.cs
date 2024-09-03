using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IUserProfileRepository userProfileRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateTime.Now;
                vm.Post.IsApproved = true; 
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            }
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        public IActionResult MyPosts()
        {
            var userId = GetCurrentUserProfileId();
            var posts = _postRepository.GetUserPostsByUserProfileId(userId);
            return View(posts);
        }

        public IActionResult Delete(int id)
        {
            var userId = GetCurrentUserProfileId();
            var post = _postRepository.GetPostById(id);

            if (post == null || (post.UserProfileId != userId && !IsAdmin()))
            {
                return Unauthorized(); // Unauthorized if the user is not the author or an Admin
            }

            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = GetCurrentUserProfileId();
            var post = _postRepository.GetPostById(id);

            if (post == null || (post.UserProfileId != userId && !IsAdmin()))
            {
                return Unauthorized(); // Unauthorized if the user is not the author or an Admin
            }

            _postRepository.Delete(id);
            return RedirectToAction(nameof(MyPosts));
        }

        public IActionResult Edit(int id)
        {
            var userId = GetCurrentUserProfileId();
            var post = _postRepository.GetPostById(id);

            if (post == null || (post.UserProfileId != userId && !IsAdmin()))
            {
                return Unauthorized(); // Unauthorized if the user is not the author or an Admin
            }

            var vm = new PostCreateViewModel
            {
                Post = post,
                CategoryOptions = _categoryRepository.GetAll()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PostCreateViewModel vm)
        {
            var userId = GetCurrentUserProfileId();
            var post = _postRepository.GetPostById(id);

            if (post == null || (post.UserProfileId != userId && !IsAdmin()))
            {
                return Unauthorized(); // Unauthorized if the user is not the author or an Admin
            }

            try
            {
                post.Title = vm.Post.Title;
                post.Content = vm.Post.Content;
                post.ImageLocation = vm.Post.ImageLocation;
                post.CategoryId = vm.Post.CategoryId;

                _postRepository.Update(post);

                return RedirectToAction("Details", new { id = post.Id });
            }
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        private bool IsAdmin()
        {
            var userId = GetCurrentUserProfileId();
            var user = _userProfileRepository.GetById(userId);
            return user != null && user.UserTypeId == 1; 
        }
    }
}
