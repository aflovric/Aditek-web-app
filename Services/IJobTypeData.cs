using Aditek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Services
{
    public interface IJobTypeData
    {
        JobType Add(JobType model);
        bool Delete(int id);
        JobType Get(int id);
        IEnumerable<JobType> GetAll();
        JobType Update(JobType model);
        IQueryable<JobType> Query();
    }
}