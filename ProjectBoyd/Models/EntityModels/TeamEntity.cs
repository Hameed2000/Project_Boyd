using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectBoyd.Models.ObjectModels;

namespace ProjectBoyd.Models.EntityModels {
    public class TeamEntity {

        [Key]
        public int TeamId { get; set; }
        public string MemberIds { get; set; } = "";
        public ICollection<StudentEntity> Students { get; set; } = new List<StudentEntity>();
        public string SessionId { get; set; }
        public string AccountId { get; set; }
        public string TeamName { get; set; }
        public bool InSession { get; set; } = false;

        [NotMapped]
        public SessionTeam sessionTeam {get;set; }

    }

}
