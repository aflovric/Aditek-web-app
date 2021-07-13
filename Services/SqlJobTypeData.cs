using Aditek.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Services
{
    public class SqlJobTypeData : IJobTypeData
    {
        private AppDbContext context;
        public SqlJobTypeData(AppDbContext context)
        {
            this.context = context;
        }
        public JobType Add(JobType model)
        {
            context.JobTypes.Add(model);
            context.SaveChanges();
            return model;
        }
        public bool Delete(int id)
        {
            JobType model = context.JobTypes.Find(id);
            if (model != null)
            {
                context.JobTypes.Remove(model);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public JobType Get(int id)
        {
            return context.JobTypes.FirstOrDefault(r => r.Id == id);
        }
        public IEnumerable<JobType> GetAll()
        {
            //iqueryable statement ako imas jako puno clanova u bazi
            return context.JobTypes.OrderBy(r => r.Name).ToList();
        }
        public IQueryable<JobType> Query()
        {
            return context.JobTypes.OrderBy(r => r.Name);
        }
        public JobType Update(JobType model)
        {
            context.Attach(model).State = EntityState.Modified;
            context.SaveChanges();
            return model;
        }
    }
}