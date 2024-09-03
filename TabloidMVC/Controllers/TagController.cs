using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using System.Collections.Generic;
using TabloidMVC.Repositories;
using System;

namespace TabloidMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _TagRepository;
        // ASP.NET will give us an instance of our tag Repository. This is called "Dependency Injection"
        public TagController(ITagRepository tagRepository)
        {
            _TagRepository = tagRepository;
        }
        // GET: TagController
        public ActionResult Index()

        {
            //Get all Tags in Tag Table and convert it to a List to pass off to the view
            List<Tag> tags = _TagRepository.GetAllTags();
            return View(tags);
        }

        // GET: TagController/Details/5
        public ActionResult Details(int id)
        {
            Tag tag = _TagRepository.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }
    }
}