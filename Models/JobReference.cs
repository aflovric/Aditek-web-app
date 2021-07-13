using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Models
{
    public class JobReference
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual IEnumerable<JobReferenceJobType> JobTypes { get; set; }
        public virtual IEnumerable<Image> Images { get; set; }
    }
}