using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aditek.Models;

namespace Aditek.ViewModels
{
    public class JobReferenceViewModel
    {
        public IEnumerable<JobReferenceCard> JobReferenceCards { get; set; }
        public List<JobType> JobTypes { get; set; }
    }
}