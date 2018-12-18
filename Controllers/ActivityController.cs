using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CBelt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CBelt.Controllers
{
    public class ActivityController : Controller
    {
        private BeltContext dbContext; 
        public ActivityController(BeltContext context)
        {
        dbContext = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("allactivities")]
        public IActionResult Allactivities()
        {
            var userid = HttpContext.Session.GetInt32("Uid");
            ViewBag.userid = userid;
            if( userid == null)
            {
                return RedirectToAction("Register", "LoginReg");
            }
            List<Activity> allactivities = dbContext.Activities
                .Include(i => i.myuser)
                .Include(i => i.RSVPS)
                    .ThenInclude(j => j.user)
                .ToList();
            return View(allactivities);
        }
        [HttpGet]
        [Route("join/{activityid}")]
        public IActionResult Join(int activityid)
        {
            RSVP rsvp = new RSVP();
            var uid = HttpContext.Session.GetInt32("Uid");
            Activity aobj = dbContext.Activities.FirstOrDefault(i => i.activityid == activityid);
            User userobj = dbContext.Users.FirstOrDefault(i => i.userid == uid);
            rsvp.userid = (int)uid;
            rsvp.activityid = activityid;
            rsvp.activity = aobj;
            rsvp.user = userobj;
            dbContext.Add(rsvp);
            dbContext.SaveChanges();
            return RedirectToAction("Allactivities");
        }
        [HttpGet]
        [Route("addactivity")]
        public IActionResult Addactivity()
        {
            var userid = HttpContext.Session.GetInt32("Uid");
            if( userid == null)
            {
                return RedirectToAction("Register", "LoginReg");
            }
            return View();
        }
        [HttpPost]
        [Route("processactivity")]
        public IActionResult Processactivity(Activity newactivity)
        {
            var uid = HttpContext.Session.GetInt32("Uid");
            User userinsession = dbContext.Users.FirstOrDefault(i => i.userid == uid);
            newactivity.myuser = userinsession;
            if(ModelState.IsValid)
            {
                newactivity.date = newactivity.date + newactivity.time;
                dbContext.Add(newactivity);
                dbContext.SaveChanges();
                return RedirectToAction("Allactivities");
            }
            return View("Addactivity");
        }
        [HttpGet]
        [Route("viewone/{activityid}")]
        public IActionResult Viewone(int activityid)
        {
            var userid = HttpContext.Session.GetInt32("Uid");
            if( userid == null)
            {
                return RedirectToAction("Register", "LoginReg");
            }
            var attending = dbContext.Activities.Include(i => i.myuser).Include(i => i.RSVPS).ThenInclude(j => j.user).FirstOrDefault(a => a.activityid == activityid);
            return View(attending);
        }
        [HttpGet]
        [Route("removersvp/{activtyid}")]
        public IActionResult Removersvp(int activtyid)
        {
            var userid = HttpContext.Session.GetInt32("Uid");
            RSVP thisrsvp = dbContext.RSVPs.FirstOrDefault(i => i.userid == userid && i.activityid == activtyid);
            dbContext.RSVPs.Remove(thisrsvp);
            dbContext.SaveChanges();
            return RedirectToAction("Allactivities");
        }
        [HttpGet]
        [Route("removeactivity/{activityid}")]
        public IActionResult Removeactivity(int activityid)
        {
            Activity thisact = dbContext.Activities.FirstOrDefault(i => i.activityid == activityid);
            dbContext.Activities.Remove(thisact);
            dbContext.SaveChanges();
            return RedirectToAction("Allactivities");
        }
    }
}
