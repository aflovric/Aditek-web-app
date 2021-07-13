using Aditek.Models;
using System.Collections.Generic;
namespace Aditek.ViewModels
{
    public class JobReferenceCard
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<string> JobTypes { get; set; }
    }
}