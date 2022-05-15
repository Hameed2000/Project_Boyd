using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.EntityModels.LabEntities {
    public class TagEntity {

        [Key]
        public string ServiceNumber { get; set; }
        //public int LabId { get; set; }
        public string TagName { get; set; }
        public int LowRange { get; set; }
        public int HighRange { get; set; }
        public string CDSImageAddress { get; set; }

        public int LabId { get; set; }
        public ModuleEntity Lab { get; set; }

    }
}
