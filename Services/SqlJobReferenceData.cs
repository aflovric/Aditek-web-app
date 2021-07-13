using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aditek.Models;
using Microsoft.EntityFrameworkCore;

namespace Aditek.Services
{
    public class SqlJobReferenceData : IJobReferenceData
    {
        private AppDbContext context;
        public SqlJobReferenceData(AppDbContext context)
        {
            this.context = context;
        }
        public JobReference Add(JobReference model)
        {
            context.JobReferences.Add(model);
            context.SaveChanges();
            return model;
        }
        public bool Delete(int id)
        {
            JobReference model = context.JobReferences.Find(id);
            if (model != null)
            {
                context.JobReferences.Remove(model);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public JobReference Get(int id)
        {
            return context.JobReferences.FirstOrDefault(r => r.Id == id);
        }
        public IEnumerable<JobReference> GetAll()
        {
            //iqueryable statement ako imas jako puno clanova u bazi
            return context.JobReferences.OrderBy(r => r.Id).ToList();
        }
        public IQueryable<JobReference> Query()
        {
            return context.JobReferences.OrderBy(r => r.Id);
        }
        public JobReference Update(JobReference model)
        {
            context.Attach(model).State = EntityState.Modified;
            context.SaveChanges();
            return model;
        }
    }
}