// Author: Hameed Awwad
//
// Description
// The Startup file is used to basically add configurations to the project
// A lot of it is boilerplate and isn't important to understand
// You can also use Startup to run some code you want before the project starts up, for example populating the database, or creating a super account
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectBoyd.Data;
using Westwind.AspNetCore.LiveReload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectBoyd.Models.EntityModels;
using ProjectBoyd.Models.ObjectModels;
using Blazored.SessionStorage;
using ProjectBoyd.Models.EntityModels.LabEntities;

namespace ProjectBoyd {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // This adds the database context as a service to the project that way we can access the database
            // It sets the Connection String to "DefaultConnection"
            // "DefaultConnection" can be found and configured to your machine in appsettings.json
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                contextLifetime: ServiceLifetime.Transient,
                optionsLifetime: ServiceLifetime.Singleton);

            // DB Factory
            services.AddDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Scoped);

            services.AddDatabaseDeveloperPageExceptionFilter();

            // Adds the Identity service, used for authenticating user accounts
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {

                //options.SignIn.RequireConfirmedAccount = false;

            }).AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();


            // Adds Microsoft login authentication
            // I got the client ID and the client secret from https://go.microsoft.com/fwlink/?linkid=2083908
            // I created an account for this project, and it already has this information set up
            // We might have to work with Blaine to create an official Emerson account, but as of now I've set up a temporary account
            /*services.AddAuthentication().AddMicrosoftAccount(microsoftOptions => {
                microsoftOptions.ClientId = "b391d2c7-379b-4b18-896e-3e8211925fcf";
                microsoftOptions.ClientSecret = "ylz8Q~WZGjp9J8SrnHpRyZixyQpcwiIaO-SkwbE3";
            });*/

            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions => {
                microsoftOptions.ClientId = "b6c5eac7-808a-4b24-a67e-c100cb9c1a4b";//"ac07f7f5-53a6-4f01-869a-de2f3ca768d6";//"c2ea8842-772a-4280-92a6-20db876b9e05";
                microsoftOptions.ClientSecret = "Qnk8Q~NMqattEsAVQVJigoeA.TqCgeA5sVY4gato";//"6pS8Q~fTSVxKKB73KRszZrAZch.Z5D47Mq-JJaP5";//"umm7Q~iuAjQ54lfEzTwjskiKpRekPMyhEef99";
            });

            // Add any other services you'd like here
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredSessionStorage();
            services.AddLiveReload();
            services.AddScoped<ApplicationDbContext, ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseLiveReload();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            // Add any code you'd like to run here in a similar format
            // Create the methods below this method
            CreateRoles(serviceProvider).Wait();
            CloseActiveSessions(serviceProvider).Wait();


            // Whenever you need to reset the database for any reason follow these steps:
            // 1: Uncomment the two lines below
            // 2: Delete the values in migration folder
            // 3: Run the application
            // 4: Recomment the two lines below
            // 5: Run these commands in Package Manager Console: "Add-Migration InitialCreate" and "Update-Database"

            //var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());            
            //dbContext.Database.EnsureDeleted();

        }


        // Function used to create roles
        // It loops through the roles that exist, if any of the roles are missing it will create them
        // I also use this function to add the initial account
        // Configure the "TopAdmin" variable to your emerson email that way when you try to log in it approves your account
        // When you first log in you will need to register your account, then close the program, and re-open the program
        // Eventually you'll need to create something that automatically aproves the TopAdmin account without needing to
        // restart the application
        private async Task CreateRoles(IServiceProvider serviceProvider) {

            var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            //dbContext.Database.MigrateAsync();

            // Initializing roles 
            // Add any roles you'd like to add in "roleNames"
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Instructor", "Student", "Pending" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames) {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist) {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Approving the TopAdmin account
            var SignInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var TopAdmin = await UserManager.FindByEmailAsync("boydipe@outlook.com");
            if (TopAdmin != null) {
                await UserManager.AddToRoleAsync(TopAdmin, "Admin");
                await UserManager.AddToRoleAsync(TopAdmin, "Instructor");
                await UserManager.RemoveFromRoleAsync(TopAdmin, "Pending");

                var foundInstructor = dbContext.Instructors.Where(i => i.UserId == TopAdmin.Id).FirstOrDefault();

                if (foundInstructor == null) {
                    InstructorEntity instructorEntry = new InstructorEntity();
                    instructorEntry.UserId = TopAdmin.Id;
                    await dbContext.Instructors.AddAsync(instructorEntry);
                    await dbContext.SaveChangesAsync();
                }
            }


        }

        // Function used to close any active sessions on StartUp
        // The reason I made this is because the ActiveSessionList is stored on the RAM so when the application turns off the memory is wiped
        // Because that list is empty I decided to close any active sessions in the database
        // The other option and the better option is to just refill the ActiveSessionList with any sessions that should still be active
        // and closing any sessions that need to be closed
        private async Task CloseActiveSessions(IServiceProvider serviceProvider) {

            var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            var activeSessions = dbContext.Sessions.Where(s => s.Active == true).ToList();

            if (activeSessions != null) {
                foreach (var session in activeSessions) {
                    session.Active = false;
                    session.Closed = DateTime.Now;
                    dbContext.Sessions.Update(session);

                    var instructor = await dbContext.Instructors.FirstOrDefaultAsync(i => i.SessionId == session.SessionId);
                    instructor.SessionId = null;
                    dbContext.Instructors.Update(instructor);

                    await dbContext.SaveChangesAsync();
                }
            }

            return;

        }

    }

}