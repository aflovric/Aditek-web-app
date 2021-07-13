using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public class JobType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Price per unit of measurement")]
        public double PricePerUnitOfMeasurement { get; set; }
        public virtual JobReferenceJobType JobTypes { get; set; }
    }
}