using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Bank.Models;

namespace Bank.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                dbContext.users.Add(newUser);
                dbContext.SaveChanges();
                HttpContext.Session.SetString("LoggedIn", "LoggedIn");
                HttpContext.Session.SetInt32("UserId", newUser.UserId);


                var userInDb = dbContext.users.FirstOrDefault(u => u.Email == newUser.Email);

                return RedirectToAction("AccountPage", new { id = userInDb.UserId });
            }
            else
            {
                return View("Index");
            }
        }

        [Route("AccountPage/{id}")]
        [HttpGet]
        public IActionResult AccountPage(User currUser)
        {
            if (HttpContext.Session.GetString("LoggedIn") == null)
            {
                return RedirectToAction("Index");
            }
            // var sum = 0;
            List<Transactions> AllTrans = dbContext.trans.ToList();
            double sum = 0;
            foreach (var x in AllTrans)
            {
                sum += x.Amount;

            }
            Console.WriteLine(sum);
            // // int total = Sum(AllTrans);

            // // sum += AllTrans;
            ViewBag.sum = sum;
            ViewBag.Amount = AllTrans;
            ViewBag.currUser = currUser;
            return View();
        }

        [Route("loginPage")]
        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [Route("loginPerson")]
        [HttpPost]
        public IActionResult LoginPerson(LoginUser user)
        {
            if (ModelState.IsValid)
            {

                var userInDb = dbContext.users.FirstOrDefault(u => u.Email == user.Email);
                // var currUser = dbContext.users.FirstOrDefault(u => u.UserId );
                // ViewBag.currUser = currUser;
                if (userInDb == null)
                {

                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("loginPage");
                }
                var hasher = new PasswordHasher<LoginUser>();


                var result = hasher.VerifyHashedPassword(user, userInDb.Password, user.Password);


                if (result == 0)
                {

                    ModelState.AddModelError("Password", "Invalid Email/Password");
                    return View("loginPage");

                }
                HttpContext.Session.SetString("LoggedIn", "LoggedIn");
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("AccountPage", new { id = userInDb.UserId });
            }
            else
            {
                return View("loginPage");
            }
        }

        [Route("moneyProcess")]
        [HttpPost]
        public IActionResult MoneyProcess(Transactions newTrans)
        {

            
            newTrans.UserId = (int)HttpContext.Session.GetInt32("UserId");
            newTrans.Amount = Convert.ToInt32(Request.Form["Amount"]);
            
            dbContext.trans.Add(newTrans);
            
            dbContext.SaveChanges();
            //  dbContext.SaveChanges();

            return RedirectToAction("AccountPage", new { id = (int)HttpContext.Session.GetInt32("UserId") });
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
