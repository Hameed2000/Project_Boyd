using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ProjectBoyd.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using ProjectBoyd.Models.EntityModels;
using ProjectBoyd.Models.ObjectModels;
using ProjectBoyd.Models.EntityModels.LabEntities;

namespace ProjectBoyd.Controllers {
    
    
    [Authorize(Roles = "Student")]
    public class StudentController : Controller {
        private readonly ILogger<StudentController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public StudentController(ILogger<StudentController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index() {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StudentInstructions() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StudentDataTable()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StudentCalibrationSheet()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StudentTips()
        {
            return View();
        }



        // Deal with deleting session info entry later
        [HttpGet("/Student/LoginInternal")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginInternal(string userId, string sessionId, string teamId) {
            Console.WriteLine(userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) {
                return Redirect("/");
            }

            await _signInManager.SignInAsync(user, true);

            return Redirect("/Student/StudentInstructions");

        }

        [HttpGet("/Student/StudentLogOut")]
        [AllowAnonymous]
        public async Task<IActionResult> StudentLogOut(string userName) {
            
            await _signInManager.SignOutAsync();

            /*var user = await _userManager.FindByNameAsync(userName);
            if (user == null) {
                return Redirect("/");
            }*/

            return Redirect("/Login/StudentLogin");
        }


    }

}
