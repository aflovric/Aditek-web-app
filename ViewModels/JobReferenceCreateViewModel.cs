using Aditek.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.ViewModels
{
    public class JobReferenceCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public List<CheckBoxItem> JobTypes { get; set; }
        [Required]
        public IFormFile[] Images { get; set; }
    }
}