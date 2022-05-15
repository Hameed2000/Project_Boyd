using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectBoyd.Models.EntityModels;

namespace ProjectBoyd.Areas.Identity.Pages
{
    public class AccountPending : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;


        public AccountPending(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public IActionResult OnGet(){


            Console.WriteLine(User.IsInRole("Pending"));

            if (!User.IsInRole("Pending")) {

                return RedirectToPage("instructor/index");

            }

            return null;



        }
    }
}
