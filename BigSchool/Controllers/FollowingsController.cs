using BigSchool.DTO;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace BigSchool.Controllers
{
    public class FollowingsController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Followings
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if(_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
                return BadRequestResult("Following already exists!");
            

            var folowing = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId,
            };

            _context.Followings.Add(folowing);
            _context.SaveChanges();

            return Ok();
        }

        private IHttpActionResult Ok()
        {
            throw new NotImplementedException();
        }

        private IHttpActionResult BadRequestResult(string v)
        {
            throw new NotImplementedException();
        }
    }

}