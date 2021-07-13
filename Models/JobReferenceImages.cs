using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public class JobReferenceImages
    {
        public IEnumerable<Image> Images { get; set; }
        public IFormFile[] images { get; set; }
        public Image[] image { get; set; }
        public JobReference jobReference { get; set; }
    }
}