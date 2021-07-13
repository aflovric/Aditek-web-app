using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Aditek.Models;
using Aditek.Services;
using Aditek.ViewModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Aditek.ViewModels
{
    public class JobReferenceEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IEnumerable<string> ExistingImages { get; set; }
        public IEnumerable<ImageModel> ExistingImagess { get; set; }
        [Required]
        public IEnumerable<CheckBoxItem> JobTypes { get; set; }
        public IFormFile[] NewImages { get; set; }
        //public IActionResult OnGet(int id)
        //{
        //    JobReferenceCard = _jobReferenceData.Query().Where(x => x.Id == id)
        //        .Select(x => new JobReferenceCard
        //        {
        //            Title = x.Name,
        //            Description = x.Description,
        //            Id = x.Id,
        //            Images = x.Images.Select(y => y.FileName).ToList()
        //        }).FirstOrDefault();
        //    FolderName = JobReferenceCard.Title;
        //    Images = _imageData.GetAll();
        //    if (JobReferenceCard == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return Page();
        //}
        //public IActionResult OnPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        JobReference jobReference;
        //        jobReference = _jobReferenceData.Get(JobReferenceCard.Id);
        //        if (FolderName != JobReferenceCard.Title)
        //        {
        //            jobReference.Name = JobReferenceCard.Title;
        //            Directory.Move("Images/" + FolderName, "Images/" + jobReference.Name);
        //            //int i = 0;
        //            //foreach(Image image in jobReference.Images)
        //            //{
        //            //    image.FileName = jobReference.Name + i + ".jpg";
        //            //    _imageData.Update(image);
        //            //    i++;
        //            //}
        //        }
        //        if (jobReference.Description != jobReference.Description)
        //        {
        //            jobReference.Description = jobReference.Description;
        //        }
        //        _jobReferenceData.Update(jobReference);
        //        return RedirectToAction("Gallery", "Home");
        //    }
        //    return Page();
        //}
    }
}