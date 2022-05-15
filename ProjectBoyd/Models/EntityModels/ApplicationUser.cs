using Microsoft.AspNetCore.Identity; 
using ProjectBoyd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.EntityModels {
    public class ApplicationUser : IdentityUser {

        public string RoleRequest { get; set; }



    }
}
