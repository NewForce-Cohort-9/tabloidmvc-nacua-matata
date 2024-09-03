﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Repositories;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Models;
using System.Security.Claims;
using System.Web;
using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Claims;


namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }

        // GET: CommentController
        public ActionResult Index(int id)
        {
            var comments = _commentRepo.GetAllPostComments(id);

            return View(comments);
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Comment comment)
        {
            try
            {
                comment.UserProfileId = GetCurrentUserProfileId();
                comment.PostId = (Url.Action()[Url.Action().Length - 1]) - 48;
                comment.CreateDateTime = DateTime.Now;

                _commentRepo.Add(comment);

                string url = HttpUtility.UrlDecode($"Index/{comment.PostId}");

                return RedirectToAction("Index", "Comment", new { id = comment.PostId });
            }
            catch (Exception ex)
            {
                return View(comment);

            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            Comment comment = _commentRepo.GetCommentById(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                _commentRepo.EditComment(comment);

                return RedirectToAction("Index", "Comment", new { id = comment.PostId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            var userId = GetCurrentUserProfileId();

            var comment = _commentRepo.GetUserCommentById(id, userId);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: CommentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var userId = GetCurrentUserProfileId();

                var comment = _commentRepo.GetUserCommentById(id, userId);

                _commentRepo.Delete(comment.Id);

                return RedirectToAction("Index", "Comment", new { id = comment.PostId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
