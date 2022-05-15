using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using ProjectBoyd.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Controllers {
    
    
    //[Authorize(Roles = "Admin, Instructor")]
    public class InstructorController : Controller {
        private readonly ILogger<InstructorController> _logger;

        public InstructorController(ILogger<InstructorController> logger) {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AdminPanel() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult SessionGenerator() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult LabCreator()
        {
            return View();
        }

        /* Uploading an image through javascript http request
        [HttpPost("Instructor/UploadTagImage")]
        public Task<IActionResult> UploadTagImage() {
            
        }*/

    }

}
