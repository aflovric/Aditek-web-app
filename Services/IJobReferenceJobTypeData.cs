using Aditek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Services
{
    public interface IJobReferenceJobTypeData
    {
        JobReferenceJobType Add(JobReferenceJobType model);
        bool Delete(int id);
        JobReferenceJobType Get(int jobReferenceId, int jobTypeId);
        IEnumerable<JobReferenceJobType> GetAllJobTypesOfSameJobReferenceId(int id);
        IEnumerable<JobReferenceJobType> GetAllJobTypesOfSameJobTypeId(int id);
        JobReferenceJobType Update(JobReferenceJobType model);
        IQueryable<JobReferenceJobType> Query();
    }
}