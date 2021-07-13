using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public class JobReferenceJobType
    {
        public int Id { get; set; }
        public int JobReferenceId { get; set; }
        public int JobTypeId { get; set; }
        [ForeignKey("JobReferenceId")]
        public virtual JobReference JobReferences { get; set; }
        [ForeignKey("JobTypeId")]
        public virtual JobType JobTypes { get; set; }
    }
}