using BigSchool.Models;
using BigSchool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Course
        public ActionResult Create()
        {

            var viewModel = new CourseViewModel()
            {
                Categories = _context.Categories.ToList(),
            };

            return View(viewModel);
        }


    }
}