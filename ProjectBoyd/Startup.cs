using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity; 
using ProjectBoyd.Models;
using Microsoft.AspNetCore.Identity.UI;
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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectBoyd.Models.EntityModels;
using ProjectBoyd.Models.ObjectModels;
using Blazored.SessionStorage;
using ProjectBoyd.Services;
using ProjectBoyd.Models.EntityModels.LabEntities;

namespace ProjectBoyd {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            //services.AddScoped<Session>();
            //services.AddScoped<InstructorEntity>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {

                //options.SignIn.RequireConfirmedAccount = false;

            }).AddDefaultUI().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
          

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions => {
                microsoftOptions.ClientId = "ac07f7f5-53a6-4f01-869a-de2f3ca768d6";//"c2ea8842-772a-4280-92a6-20db876b9e05";
                microsoftOptions.ClientSecret = "6pS8Q~fTSVxKKB73KRszZrAZch.Z5D47Mq-JJaP5";//"umm7Q~iuAjQ54lfEzTwjskiKpRekPMyhEef99";
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredSessionStorage();

            services.AddLiveReload();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

            CreateRoles(serviceProvider).Wait();
            RefillSessionInfoList(serviceProvider).Wait();

            //var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            //dbContext.Database.EnsureDeleted();

        }

        private async Task CreateRoles(IServiceProvider serviceProvider) {

            var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            ModuleEntity mod = await dbContext.Modules.Include(m => m.WorkOrder).Include(m => m.Tags).Include(m => m.Tips).Where(l => l.LabId == 15).FirstOrDefaultAsync();
            SessionInfo.SessionInfoList.Add("46", new Dictionary<string, SessionTeam>());
            SessionInfo.SessionInfoList.GetValueOrDefault("46").Add("43", new SessionTeam());
            SessionInfo.SessionInfoList.GetValueOrDefault("46").GetValueOrDefault("43").LabQueue.Add(mod);
            SessionInfo.HelpQueue.Add("46", new List<TeamEntity>());

            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Instructor", "Student", "Pending" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames) {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist) {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var SignInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var TopAdmin = await UserManager.FindByEmailAsync("boydipe@outlook.com");
            if (TopAdmin != null) {
                Console.WriteLine("FOund aawwad3");
                await UserManager.AddToRoleAsync(TopAdmin, "Admin");
                await UserManager.AddToRoleAsync(TopAdmin, "Instructor");
                await UserManager.RemoveFromRoleAsync(TopAdmin, "Pending");

                var foundInstructor = dbContext.Instructors.Where(i => i.UserId == TopAdmin.Id).FirstOrDefault();
                Console.WriteLine("1");
                if (foundInstructor == null) {
                    InstructorEntity instructorEntry = new InstructorEntity();
                    instructorEntry.UserId = TopAdmin.Id;
                    Console.WriteLine("2");
                    await dbContext.Instructors.AddAsync(instructorEntry);
                    await dbContext.SaveChangesAsync();
                }
            }


        }

        private async Task RefillSessionInfoList(IServiceProvider serviceProvider) {

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

// Hello Test

/* //Here you could create a super user who will maintain the web app
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var poweruser = new ApplicationUser {

                UserName = Configuration["AppSettings:UserName"],
                Email = Configuration["AppSettings:UserEmail"],
            };
            //Ensure you have these values in your appsettings.json file
            string userPWD = Configuration["AppSettings:UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);

            if (_user == null) {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded) {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }*/




/*
 * 
 *             var user = new ApplicationUser {
                RoleRequest = "Admin",
                UserName = "CoolGuy123",
                Email = "xxxx@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                NormalizedUserName = "OWNER",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = "adsfaqw"
            };

            var userStore = new UserStore<ApplicationUser>(dbContext);
            var resule = userStore.CreateAsync(user);
            var userr = await UserManager.FindByNameAsync("OWNER");

            var result = await UserManager.AddToRoleAsync(userr, "Pending");
 * Console.WriteLine("0");
           var user = await UserManager.FindByNameAsync("OWNER");
           Console.WriteLine(user.UserName);
           Console.WriteLine("0");
           var result = await UserManager.AddToRoleAsync(user, "Pending");
           Console.WriteLine("0");
           user = await UserManager.FindByEmailAsync("xxsssxx@example.com");
           Console.WriteLine("0");
           result = await UserManager.AddToRoleAsync(user, "Pending");
           Console.WriteLine("0");
           user = await UserManager.FindByEmailAsync("xxssssxx@example.com");
           Console.WriteLine("0");
           result = await UserManager.AddToRoleAsync(user, "Pending");
           Console.WriteLine("0");


          //Creating a bunch of members
         var user = new ApplicationUser {
              RoleRequest = "Admin",
              UserName = "CoolGuy123",
              Email = "xxxx@example.com",
              NormalizedEmail = "XXXX@EXAMPLE.COM",
              NormalizedUserName = "OWNER",
              PhoneNumber = "+111111111111",
              EmailConfirmed = true,
              PhoneNumberConfirmed = true,
              SecurityStamp = Guid.NewGuid().ToString("D"),
              PasswordHash = "adsfaqw"
          };

          var userStore = new UserStore<ApplicationUser>(dbContext);
          var resule = userStore.CreateAsync(user);

          //Creating a bunch of members
          user = new ApplicationUser {
              RoleRequest = "Instructor",
              UserName = "CoolsafsafGuy123",
              Email = "xxsssxx@example.com",
              NormalizedEmail = "XXsssXX@EXAMPLE.COM",
              NormalizedUserName = "OWadsaNER",
              PhoneNumber = "+111211111111",
              EmailConfirmed = true,
              PhoneNumberConfirmed = true,
              SecurityStamp = Guid.NewGuid().ToString("D"),
              PasswordHash = "adsfaasdsaqw"
          };

          userStore = new UserStore<ApplicationUser>(dbContext);
          resule = userStore.CreateAsync(user);  

          //Creating a bunch of members
          var user = new ApplicationUser {
              RoleRequest = "Instructor",
              UserName = "CoolsafsaaaafGuy123",
              Email = "xxssssxx@example.com",
              NormalizedEmail = "XXssssXX@EXAMPLE.COM",
              NormalizedUserName = "OWadaxzxsaNER",
              PhoneNumber = "+111211111111",
              EmailConfirmed = true,
              PhoneNumberConfirmed = true,
              SecurityStamp = Guid.NewGuid().ToString("D"),
              PasswordHash = "adsfaasasdxzdsaqw"
          };

          var userStore = new UserStore<ApplicationUser>(dbContext);
          var resule = userStore.CreateAsync(user);
        */