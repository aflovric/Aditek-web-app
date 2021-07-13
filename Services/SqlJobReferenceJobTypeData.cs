using Aditek.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Services
{
    public class SqlJobReferenceJobTypeData : IJobReferenceJobTypeData
    {
        private AppDbContext context;
        public SqlJobReferenceJobTypeData(AppDbContext context)
        {
            this.context = context;
        }
        public JobReferenceJobType Add(JobReferenceJobType model)
        {
            context.JobReferenceJobTypes.Add(model);
            context.SaveChanges();
            return model;
        }
        public bool Delete(int id)
        {
            JobReferenceJobType model = context.JobReferenceJobTypes.Find(id);
            if (model != null)
            {
                context.JobReferenceJobTypes.Remove(model);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public JobReferenceJobType Get(int jobReferenceId, int jobTypeId)
        {
            return context.JobReferenceJobTypes.FirstOrDefault(r => r.JobReferenceId == jobReferenceId && r.JobTypeId == jobTypeId);
        }
        public IEnumerable<JobReferenceJobType> GetAllJobTypesOfSameJobReferenceId(int id)
        {
            //iqueryable statement ako imas jako puno clanova u bazi
            return context.JobReferenceJobTypes.Where(r => r.JobReferenceId == id);
        }
        public IEnumerable<JobReferenceJobType> GetAllJobTypesOfSameJobTypeId(int id)
        {
            //iqueryable statement ako imas jako puno clanova u bazi
            return context.JobReferenceJobTypes.Where(r => r.JobTypeId == id);
        }
        public IQueryable<JobReferenceJobType> Query()
        {
            return context.JobReferenceJobTypes.OrderBy(r => r.JobTypeId);
        }
        public JobReferenceJobType Update(JobReferenceJobType model)
        {
            context.Attach(model).State = EntityState.Modified;
            context.SaveChanges();
            return model;
        }
    }
}
