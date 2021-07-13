using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.ViewModels
{
    public class JobTypeCreateEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Price per unit of measurement")]
        public double PricePerUnitOfMeasurement { get; set; }
    }
}