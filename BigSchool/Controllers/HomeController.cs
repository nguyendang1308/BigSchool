using BigSchool.Models;
using BigSchool.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var upComingCourse = _context.Courses.Include(c => c.Lecturer).Include(c => c.Category).Where(c => c.DateTime > DateTime.Now);

            var viewModel = new CourseViewModel
            {
                UpcomingCourses = upComingCourse,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}