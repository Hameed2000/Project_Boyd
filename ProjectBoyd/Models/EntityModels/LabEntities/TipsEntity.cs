using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.EntityModels.LabEntities {
    public class TipsEntity {

        [Key]
        public int TipsId { get; set; }
        //public int LabId { get; set; }
        public string TipText { get; set; }
        public int ThumbsUp { get; set; } = 0;
        public int ThumbsDown { get; set; } = 0;

        public int LabId { get; set; }
        public ModuleEntity Lab { get; set; }

    }
}
