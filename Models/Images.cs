using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public class Images
    {
        public IFormFile[] images { get; set; }
    }
}