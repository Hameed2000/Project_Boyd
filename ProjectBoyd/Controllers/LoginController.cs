using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBoyd.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Controllers {
    public class LoginController : Controller {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger) {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StudentLogin() {
            return View();
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult InstructorLogin() {
            return View();
        }*/

    }

}
