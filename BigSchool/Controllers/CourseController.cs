﻿using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
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
    }
}