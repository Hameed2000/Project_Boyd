using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Models.EntityModels.LabEntities {
    public class ModuleEntity {

        [Key]
        public int LabId { get; set; }
        public string LabName { get; set; }
        public string LabIconAddress { get; set; }
        public string LabDescription { get; set; }
        public WorkOrderEntity WorkOrder { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
        public ICollection<TipsEntity> Tips { get; set; }

        /*
        public int WorkOrderId { get; set; }
        public int TagsList { get; set; }
        public int TipsList { get; set; }
        */
        
    }
}
