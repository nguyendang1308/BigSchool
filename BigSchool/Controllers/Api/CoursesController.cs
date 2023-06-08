using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _context { get; set; }

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancle(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _context.Courses.Single(c => c.Id == id && c.LecturerId == userId);
            if (course.IsCanceled) 
                return NotFound();
            course.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
