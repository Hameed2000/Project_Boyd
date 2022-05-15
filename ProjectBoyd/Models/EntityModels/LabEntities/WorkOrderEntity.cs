using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.EntityModels.LabEntities {
    public class WorkOrderEntity {

        [Key]
        public int WorkOrderId { get; set; }
        //public int LabId { get; set; }
        public string Description { get; set; }
        public string Equipment { get; set; }
        public string Tasks { get; set; }

        public int LabId { get; set; }
        public ModuleEntity Lab { get; set; }

    }
}

