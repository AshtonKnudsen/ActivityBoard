using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CBelt.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace CBelt.Controllers
{
    public class LoginRegController : Controller
    {
        private BeltContext dbContext; 
        public LoginRegController(BeltContext context)
        {
        dbContext = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("registerprocess")]
        public IActionResult RegisterProcess(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.email == user.email))
                {
                    ModelState.AddModelError("email", "Email is already in use");
                    return RedirectToAction("Register");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.password = Hasher.HashPassword(user, user.password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                var UserId = dbContext.Users.FirstOrDefault(u => u.userid == user.userid);
                HttpContext.Session.SetInt32("Uid", UserId.userid);
                return RedirectToAction("Allactivities", "Activity");
            }
            return View("Register");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Register");
        }
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }    
        [HttpPost]
        [Route("loginprocess")]
        public IActionResult LoginProcess(LoginUser Luser)
        {
           var thisuser = dbContext.Users.FirstOrDefault(u => u.email == Luser.email);
           if(thisuser == null)
            {
                // Add an error to ModelState and return to View!
                ModelState.AddModelError("email", "Invalid Email/Password");
                return View("Login");
            }
            var hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(Luser, thisuser.password, Luser.password);
            if(result == 0)
            {
                return RedirectToAction("Login");
            }
            HttpContext.Session.SetInt32("Uid", thisuser.userid);
            return RedirectToAction("Allactivities", "Activity");
        }
    
    }
}
