using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string FileName { get; set; }
        [ForeignKey("JobId")]
        public virtual JobReference JobReferences { get; set; }
    }
}