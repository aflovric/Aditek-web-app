using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Aditek.Models
{
    public class Email
    {
        public List<IFormFile> attachment { get; set; }
        public string address { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string subject { get; set; }
    }
}