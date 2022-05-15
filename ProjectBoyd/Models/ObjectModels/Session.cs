using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ProjectBoyd.Models;
using ProjectBoyd.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProjectBoyd.Data;
using ProjectBoyd.Models.ObjectModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace ProjectBoyd.Models.ObjectModels {
    public class Session {

        //private readonly UserManager<ApplicationUser> _userManager;
        //private ApplicationDbContext dbContext;
        RandomNumberGenerator rng = RandomNumberGenerator.Create();

        public SessionEntity sessionEntity { get; set; } = new SessionEntity { };


        public Session(string SessionName, InstructorEntity PrimaryInstructor, string InstructorList) {

            sessionEntity.SessionName = SessionName;
            Console.WriteLine(PrimaryInstructor.InstructorId);
            sessionEntity.PrimaryInstructor = PrimaryInstructor.InstructorId.ToString();
            sessionEntity.InstructorList = InstructorList;
            //sessionEntity.SecondaryInstructor = SecondaryInstructor.InstructorId;

            sessionEntity.Created = DateTime.Now;
            sessionEntity.JoinCode = generateJoinCode();
            sessionEntity.Active = true;

            PrimaryInstructor.Session = this.sessionEntity;
            PrimaryInstructor.SessionId = sessionEntity.SessionId;

        }

        public Session(SessionEntity entity) {

            sessionEntity = entity;

        }

        private string generateJoinCode() {

            Random rnd = new Random();
            string joinCode = rnd.Next(0, 10).ToString() + Convert.ToChar(rnd.Next(97, 123)) + rnd.Next(0, 10).ToString() + Convert.ToChar(rnd.Next(97, 123)) + rnd.Next(0, 10).ToString();

            return joinCode;
            //Byte[] bytes = new Byte[8];
            //rng.GetBytes(bytes);
            //return (BitConverter.ToUInt32(bytes, 0) % 100000000).ToString();
        }


        /*public async Task ScheduledEndSession(ApplicationDbContext dbContext) {
            await Task.Delay(28800000); // Delays for 8 hours (in milliseconds)
            EndSession(dbContext);
        }*/

        public async void EndSession(UserManager<ApplicationUser> userManager) {

            SessionInfo.SessionInfoList.Remove(this.sessionEntity.SessionId.ToString());

            string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ProjectBoyd-31B34D1B-9898-4A91-906F-2656B4EFDFEA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

            //using var dbContext = new ApplicationDbContext(contextOptions);

            using (var dbContext = new ApplicationDbContext(contextOptions)) {
                sessionEntity = await dbContext.Sessions.FirstOrDefaultAsync<SessionEntity>(s => s.SessionId == sessionEntity.SessionId);
                if (sessionEntity.Active) {
                    sessionEntity.Closed = DateTime.Now;
                    sessionEntity.Active = false;

                    InstructorEntity ent = dbContext.Instructors.Where(i => i.InstructorId.ToString() == sessionEntity.PrimaryInstructor).FirstOrDefault<InstructorEntity>();
                    ent.Session = null;
                    ent.SessionId = null;


                    List<ApplicationUser> teamAccounts = new List<ApplicationUser>();

                    string teamsIds = sessionEntity.TeamsList;

                    string[] teamsIdsList = teamsIds.Split(' ');

                    foreach (var id in teamsIdsList) {
                        var team = dbContext.Teams.FirstOrDefault<TeamEntity>(t => t.TeamId.ToString() == id);
                        Console.WriteLine("Bool Test");
                        Console.WriteLine(id);
                        if (team != null) {
                            Console.WriteLine(team.InSession);
                            string accountId = team.AccountId;
                            team.InSession = false;
                            dbContext.SaveChanges();
                            ApplicationUser account = await userManager.FindByIdAsync(accountId);
                            //ApplicationUser account = dbContext.Users.Where(u => u.Id.ToString() == accountId).FirstOrDefault<ApplicationUser>();
                            if (account != null) {
                                await userManager.FindByIdAsync(account.Id);
                                await userManager.DeleteAsync(account);
                            }
                        }
                    }

                    dbContext.SaveChanges();

                }
            }
        }

        public List<ApplicationUser> GetTeamAccounts() {
            
            List<ApplicationUser> teamAccounts = new List<ApplicationUser>();

            string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ProjectBoyd-31B34D1B-9898-4A91-906F-2656B4EFDFEA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

            using (var dbContext = new ApplicationDbContext(contextOptions)) {

                string teamsIds = sessionEntity.TeamsList;
                if (teamsIds == null) {
                    return teamAccounts;
                }

                string[] teamsIdsList = teamsIds.Split(' ');

                foreach (var id in teamsIdsList) {
                    TeamEntity team = dbContext.Teams.Where(t => t.TeamId.ToString() == id).FirstOrDefault<TeamEntity>();
                    if (team != null) {
                        string accountId = team.AccountId;
                        ApplicationUser account = dbContext.Users.Where(u => u.Id.ToString() == accountId).FirstOrDefault<ApplicationUser>();
                        if (account != null) {
                            teamAccounts.Add(account);
                        }
                    }
                }

            }

            return teamAccounts;

        }

        public List<StudentEntity> GetStudents(ApplicationDbContext dbContext) {

            List<StudentEntity> studentEntities = new List<StudentEntity>();

            string teamsIds = sessionEntity.TeamsList;

            if (teamsIds == null) {
                return studentEntities;
            }

            string[] teamsIdsList = teamsIds.Split(' ');

            foreach (var id in teamsIdsList) {
                TeamEntity team = dbContext.Teams.Where(t => t.TeamId.ToString() == id).FirstOrDefault<TeamEntity>();
                if (team != null) {
                    string studentIds = team.MemberIds;
                    string[] studentIdsList = studentIds.Split(' ');
                    foreach (var studentId in studentIdsList) {
                        StudentEntity student = dbContext.Students.Where(s => s.StudentId.ToString() == studentId).FirstOrDefault<StudentEntity>();
                        if (student != null) {
                            studentEntities.Add(student);
                        }
                    }
                }
            }

            return studentEntities;

        }

        public int StudentCount(ApplicationDbContext dbContext) {

            string teamsIds = sessionEntity.TeamsList;

            if (teamsIds == null) {
                return 0;
            }

            int counter = 0;

            string[] teamsIdsList = teamsIds.Split(' ');

            foreach (var id in teamsIdsList) {
                TeamEntity team = dbContext.Teams.Where(t => t.TeamId.ToString() == id).FirstOrDefault<TeamEntity>();
                if (team != null) {
                    string studentIds = team.MemberIds;
                    string[] studentIdsList = studentIds.Split(' ');
                    foreach (var studentId in studentIdsList) {
                        StudentEntity student = dbContext.Students.Where(s => s.StudentId.ToString() == studentId).FirstOrDefault<StudentEntity>();
                        if (student != null) {
                            counter++;
                        }
                    }
                }
            }

            return counter;
        }

    }

}


namespace ProjectBoyd.Models.ObjectModels {

    public static class SessionMethods {

        public static async Task ScheduleSessionEnd(UserManager<ApplicationUser> userManager, Session session) {

            await Task.Delay(28800000); // Delays for 8 hours (in milliseconds) 28800000
            session.EndSession(userManager);

        }


    }

}
