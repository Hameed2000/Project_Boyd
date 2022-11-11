using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProjectBoyd.Models.EntityModels;
using ProjectBoyd.Models.EntityModels.LabEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBoyd.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<InstructorEntity> Instructors { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<WorkOrderEntity> WorkOrders { get; set; }
        public DbSet<TipsEntity> Tips { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        }


    }
}
