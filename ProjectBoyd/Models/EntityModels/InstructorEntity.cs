using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectBoyd.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectBoyd.Data;
using System.ComponentModel.DataAnnotations;


namespace ProjectBoyd.Models.EntityModels {

    public class InstructorEntity {

        [Key]
        public int InstructorId { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }


        [ForeignKey("SessionId")]
        public SessionEntity Session { get; set; }
        public int? SessionId { get; set; }


    }

}
