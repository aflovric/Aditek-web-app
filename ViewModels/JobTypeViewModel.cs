using Aditek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.ViewModels
{
    public class JobTypeViewModel
    {
        public IEnumerable<JobType> jobTypes { get; set; }
    }
}