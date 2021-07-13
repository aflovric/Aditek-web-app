using Aditek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Services
{
    public interface IJobReferenceData
    {
        JobReference Add(JobReference model);
        bool Delete(int id);
        JobReference Get(int id);
        IEnumerable<JobReference> GetAll();
        JobReference Update(JobReference model);
        IQueryable<JobReference> Query();
    }
}