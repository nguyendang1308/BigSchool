using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                Heading = "Add Course"
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            //HeHe
            //Hehe
            //Hehe
            if(!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course()
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var courses = _context.Attendances.Where(a => a.AttendeeId == userId).Select(a => a.course).Include(l => l.Lecturer).Include(l => l.Category).ToList();

            var viewModel = new CourseViewModel
            {
                UpcomingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var course = _context.Courses.Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now).Include(l => l.Lecturer).Include(c => c.Category).ToList();
            return View(course);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _context.Courses.Single(c => c.Id == id && c.LecturerId == userId);

            var viewModel = new CourseViewModel
            {
                Categories = _context.Categories.ToList(),
                Date = course.DateTime.ToString("dd/M/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.CategoryId,
                Place = course.Place,
                Heading = "Edit Course",
                Id = course.Id,
            };
            return View("Create", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("Create", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _context.Courses.Single(c => c.Id == viewModel.Id && c.LecturerId == userId);
            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.CategoryId = viewModel.Category;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}