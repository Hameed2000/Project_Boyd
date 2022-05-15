using System;
using System.Collections.Generic;
using System.Linq;
using ProjectBoyd.Models.EntityModels;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.ObjectModels {
    public class Team {

        public TeamEntity teamEntity { get; set; } = new TeamEntity();
        public List<Student> memberRealList = new List<Student>();

        public Team(string sessionId, string teamName) {
            teamEntity.SessionId = sessionId;
            teamEntity.InSession = true;
            teamEntity.TeamName = teamName;
        }

        public void addMember(Student student) {
            memberRealList.Add(student);
            teamEntity.MemberIds = teamEntity.MemberIds + " " + student.studentEntity.StudentId;
            teamEntity.Students.Add(student.studentEntity);
        }

        public int count() {
            return memberRealList.Count();
        }

        public void printAll() {
            foreach (Student member in memberRealList) {
                Console.WriteLine(member.studentEntity.FirstName);
            }
        }

    }

}



/*
namespace ProjectBoyd.Models.ObjectModels {

    public static class TeamTest {

        public static Dictionary<string, Team> testDict = new Dictionary<string, Team>();


    }

}
*/