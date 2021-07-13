using Aditek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aditek.Services
{
    public interface IImageData
    {
        Image Add(Image image);
        bool Delete(string fileName);
        IEnumerable<Image> GetAll();
        Image Update(Image image);
    }
}