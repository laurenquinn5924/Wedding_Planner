using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingPlannerContext _context;

        public WeddingController(WeddingPlannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private User ActiveUser
        {
            get{ return _context.users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();}
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User user = _context.users.Where(u => u.id == HttpContext.Session.GetInt32("id")).FirstOrDefault();
            // FirtsOrdDefault means pull first value that matches what I'm asking for or the default value
            ViewBag.UserInfo = user;

         
            ViewBag.AllWeddings = _context.Weddings.Include(r => r.RSVPS ).ToList(); //u in this case is a wedding, populate my weddings RSVP
            return View("Dashboard");
        }

        [HttpGet]
        [Route("/newwedding")]
        public IActionResult NewWedding()
        {
            if(ActiveUser == null)
            return RedirectToAction("Index", "Home");

            ViewBag.UserInfo = ActiveUser;
            return View("NewWedding");
        }

        [HttpPost]
        [Route("AddWedding")]
        public IActionResult AddWedding(AddWedding wedding)
        {
            System.Console.WriteLine("Add Wedding Works!");

            if(ActiveUser == null)
                return RedirectToAction("Index", "Home");

            if(ModelState.IsValid)
            {
                Wedding Wedding = new Wedding
                {
                    UserId = ActiveUser.id,
                    WedderOne = wedding.WedderOne,
                    WedderTwo = wedding.WedderTwo,
                    DateOfEvent = wedding.DateOfEvent,
                    Address = wedding.Address,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now

                };

                _context.Weddings.Add(Wedding);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }


            ViewBag.UserInfo = ActiveUser;
            return View("NewWedding");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int WeddingId)
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            Wedding ToDelete = _context.Weddings.SingleOrDefault(ShowOne => ShowOne.WeddingId == WeddingId);
            _context.Weddings.Remove(ToDelete);
            _context.SaveChanges();

            ViewBag.UserInfo = ActiveUser;
            System.Console.WriteLine("In delete finction");
            List<Wedding> Weddings = _context.Weddings.ToList();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("wedding/{WeddingId}")]
        public IActionResult ShowWedding(int WeddingId) // once the user successfully logs in
        {
            if (ActiveUser == null)
                return RedirectToAction("Index", "Home");

            ViewBag.ShowOne = _context.Weddings.Where(o => o.WeddingId == WeddingId).Include(g => g.RSVPS).ThenInclude(u => u.User).SingleOrDefault();

            return View("ShowWedding");
        }

        [HttpPost]
        [Route("rsvpToWedding")]
        public IActionResult rsvpToWedding(int WeddingId)
        {

            RSVP addRSVP = new RSVP
            {
                UserId = (int)HttpContext.Session.GetInt32("id"),
                GuestWeddingId = WeddingId

            };

            _context.RSVPS.Add(addRSVP);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [Route("UnrsvpToWedding")]
        public IActionResult UnrsvpToWedding(int WeddingId)
        {
            RSVP RemoveRsvp = _context.RSVPS.SingleOrDefault(x => x.GuestWeddingId == WeddingId && x.UserId == (int)HttpContext.Session.GetInt32("id"));
            _context.RSVPS.Remove(RemoveRsvp);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}