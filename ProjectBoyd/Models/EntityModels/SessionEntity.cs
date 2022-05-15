using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ProjectBoyd.Models;
using Microsoft.AspNetCore.Identity;
using ProjectBoyd.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace ProjectBoyd.Models.EntityModels {

    // To-Do:
    // Change student list to teams list
    // Edit teams class
    // Consider anything else that needs to be done to session
    // Create session test web page
    // Test session generation
    // Do student login


    public partial class SessionEntity {

        [Key]
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string JoinCode { get; set; }
        public bool Active { get; set; }
        public string PrimaryInstructor { get; set; }
        public string InstructorList { get; set; }
        public string TeamsList { get; set; } = "";
        public DateTime Created { get; set; }
        public DateTime Closed { get; set; }

    }


    public partial class SessionEntity {
        public EntityEntry objectEntity;

        public SessionEntity() {

        }


    }

}

