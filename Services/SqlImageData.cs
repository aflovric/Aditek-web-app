using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aditek.Models;

namespace Aditek.Services
{
    public class SqlImageData : IImageData
    {
        List<Image> images;
        private AppDbContext context;
        public SqlImageData(AppDbContext context)
        {
            this.context = context;
        }
        public Image Add(Image image)
        {
            context.Images.Add(image);
            context.SaveChanges();
            return image;
        }
        public bool Delete(string fileName)
        {
            Image image = context.Images.FirstOrDefault(x=> x.FileName == fileName);
            if (image != null)
            {
                context.Images.Remove(image);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<Image> GetAll()
        {
            return context.Images;
        }
        public Image Update(Image image)
        {
            context.Attach(image).State = EntityState.Modified;
            context.SaveChanges();
            return image;
        }
    }
}