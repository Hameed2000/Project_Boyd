using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectBoyd.Models.EntityModels;
using ProjectBoyd.Models.EntityModels.LabEntities;

namespace ProjectBoyd.Models.ObjectModels {
    public class SessionTeam {
        public SessionTeam() {

        }

        public bool CompletedFirstRun { get; set; } = false;
        public bool AwaitingApproval { get; set; } = false;
        public bool NeedHelp { get; set; } = false;
        public string Status { get; set; } = "Awaiting Lab";
        public List<ModuleEntity> LabQueue { get; set; } = new List<ModuleEntity>();
        //Search through the static list when assigning a tag, if the tag is used in its session, select the next tag and search through the list, if you reach the end of the list assign them the first tag
        public string[] DataTableValues { get; set; } = Enumerable.Repeat("", 25).ToArray();
        //Once instructor assigns module to student, tag is assigned
        public TagEntity TagAssigned { get; set; }

        public Dictionary<string, bool> EquipmentList { get; set; } = new Dictionary<string, bool>();
        public Dictionary<string, bool> TaskList { get; set; } = new Dictionary<string, bool>();

        public List<string> EquipmentSelected { get; set; } = new List<string>();
        public List<string> TasksSelected { get; set; } = new List<string>();

        public List<TipsEntity> TipsLiked { get; set; } = new List<TipsEntity>();
        public List<TipsEntity> TipsDisliked { get; set; } = new List<TipsEntity>();

        public Action StateHasChangedDelegate { get; set; }
        public string Message { get; set; } = "";

    }

}

namespace ProjectBoyd.Models.ObjectModels {

    public static class SessionInfo {

        // First key is the session id, second key is the team id
        // Values are added once a team joins a session
        public static Dictionary<string, Dictionary<string, SessionTeam>> SessionInfoList = new Dictionary<string, Dictionary<string, SessionTeam>>();

        public static void PopulateValues(string sessionId, string teamId) {
            SessionTeam entry = SessionInfo.SessionInfoList.GetValueOrDefault(sessionId).GetValueOrDefault(teamId);
            ModuleEntity lab = entry.LabQueue.ElementAt(0);
            
            List<string> equipmentList = lab.WorkOrder.Equipment.Split(", ").ToList();
            equipmentList.Remove("");
            entry.EquipmentList = new Dictionary<string, bool>();
            foreach (var equipment in equipmentList) {
                entry.EquipmentList.Add(equipment, false);
            }

            List<string> taskList = lab.WorkOrder.Tasks.Split(", ").ToList();
            taskList.Remove("");
            entry.TaskList = new Dictionary<string, bool>();
            foreach (var task in taskList) {
                entry.TaskList.Add(task, false);
            }

            AssignTag(sessionId, teamId);

        }


        public static void AssignTag(string sessionId, string teamId) {
            Dictionary<string, SessionTeam> sessionTeams = SessionInfo.SessionInfoList.GetValueOrDefault(sessionId);
            SessionTeam entry = sessionTeams.GetValueOrDefault(teamId);
            ModuleEntity lab = entry.LabQueue.ElementAt(0);
            ICollection<TagEntity> tagsList = lab.Tags.ToList();
            
            foreach (var tag in tagsList) {
                bool tagUsed = false;
                foreach(var team in sessionTeams) {
                    if (team.Key != teamId) {
                        ModuleEntity otherTeamsLab = team.Value.LabQueue.ElementAtOrDefault(0);
                        if (otherTeamsLab != null && otherTeamsLab == lab && tag == team.Value.TagAssigned) {
                            tagUsed = true;
                            break;
                        }
                    }
                }
                if (!tagUsed) {
                    entry.TagAssigned = tag;
                    break;
                }
            }

        }

        public static Dictionary<string, List<TeamEntity>> HelpQueue = new Dictionary<string, List<TeamEntity>>();

    }

}

// To:Do Read these comments, continue filling student instructions page