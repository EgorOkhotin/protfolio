using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using protfolio.Models;
using protfolio.Services;
using Microsoft.AspNetCore.Mvc;

namespace protfolio.Controllers
{
    public class AccountController : Controller
    {
        AuthenticateService _auth;
        public AccountController(AuthenticateService auth)
        {
            _auth = auth;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profile");

            ViewBag.IsWorng = false as bool?;
            return View("Login");

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var res = await _auth.Authentificate(model.Emali, model.Password);
            if (res == null)
            {
                ViewBag.IsWrongLogin = true as bool?;
                return View("Login");
            }

            
            //add set identity
            return RedirectToPage("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.IsWorng = false as bool?;
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _auth.Registrate(model);
            if(result == null)
            {
                ViewBag.IsWorng = true as bool?;
                return View("Register");
            }
            
            //add set indetity
            return RedirectToPage("Profile");
        }
    }
}