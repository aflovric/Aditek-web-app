using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Aditek.Models;
using Aditek.Services;
using Aditek.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aditek.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IImageData imageData;
        private readonly IJobReferenceData jobReferenceData;
        private readonly IJobReferenceJobTypeData jobReferenceJobTypeData;
        private readonly IJobTypeData jobTypeData;
        private readonly AppDbContext appDbContext;
        public AdminController(IImageData imageData,
                            IJobReferenceData jobReferenceData,
                            IJobReferenceJobTypeData jobReference_JobTypeData,
                            IJobTypeData jobTypeData,
                            AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.imageData = imageData;
            this.jobReferenceData = jobReferenceData;
            this.jobReferenceJobTypeData = jobReference_JobTypeData;
            this.jobTypeData = jobTypeData;
        }
        [HttpGet]
        public IActionResult CreateJobType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateJobType(JobTypeCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newJobType = new JobType();
                newJobType.Name = model.Name;
                newJobType.PricePerUnitOfMeasurement = model.PricePerUnitOfMeasurement;
                newJobType = jobTypeData.Add(newJobType);
                return RedirectToAction("Services", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateJobReference()
        {
            var jobs = appDbContext.JobTypes.OrderBy(x => x.Name).ToList();
            JobReferenceCreateViewModel model = new JobReferenceCreateViewModel();
            model.JobTypes = jobs.Select(x => new CheckBoxItem()
            {
                Id = x.Id,
                Name = x.Name,
                IsChecked = false
            }).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateJobReference(JobReferenceCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                JobReference newJobReference = new JobReference();
                newJobReference.Title = model.Title;
                newJobReference = jobReferenceData.Add(newJobReference);
                foreach (var job in model.JobTypes.ToList())
                {
                    var jobType = new JobReferenceJobType();
                    if (job.IsChecked)
                    {
                        jobType.JobReferenceId = newJobReference.Id;
                        jobType.JobTypeId = job.Id;
                        jobReferenceJobTypeData.Add(jobType);
                    }
                }
                if (model.Images != null)
                {
                    for (var i = 0; i < model.Images.Length; i++)
                    {
                        var newImage = new Image();
                        newImage.FileName = model.Title + i + ".jpg";
                        newImage.JobId = newJobReference.Id;
                        Directory.CreateDirectory("wwwroot/Images/" + newJobReference.Title);
                        var filePath = Path.Combine("wwwroot/Images/" + newJobReference.Title, newImage.FileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Images[i].CopyToAsync(fileStream);
                        }
                        newImage = imageData.Add(newImage);
                    }
                }
                return RedirectToAction("Gallery", "Home");
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult DeleteJobReference(int id)
        {
            JobReference jobReference = jobReferenceData.Get(id);
            bool fileDeleted = jobReferenceData.Delete(id);
            if (fileDeleted)
            {
                Directory.Delete("wwwroot/Images/" + jobReference.Title, true);
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult DeleteJobType(int id)
        {
            JobType jobType = jobTypeData.Get(id);
            bool fileDeleted = jobTypeData.Delete(id);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult DeleteImage(string jobReferenceName, string fileName)
        {
            bool fileDeleted = imageData.Delete(fileName);
            if (fileDeleted)
            {
                System.IO.File.Delete("wwwroot/Images/" + jobReferenceName + '/' + fileName);
            }
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Content("Message sent!", MediaTypeNames.Text.Plain);
        }
        [HttpGet]
        public IActionResult EditJobReference(int id)
        {
            var model = jobReferenceData.Query().Where(x => x.Id == id)
                .Select(x => new JobReferenceEditViewModel
                {
                    Title = x.Title,
                    Id = x.Id,
                    ExistingImages = x.Images.Select(y => y.FileName).ToList()
                }).FirstOrDefault();
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditJobReference(JobReferenceEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                JobReference jobReference;
                jobReference = jobReferenceData.Get(model.Id);
                if (jobReference.Title != model.Title)
                {
                    Directory.Move("wwwroot/Images/" + jobReference.Title, "wwwroot/Images/" + model.Title);
                    jobReference.Title = model.Title;
                    //foreach (Image image in model.ExistingImages)
                    //{
                    //    image.FileName =;
                    //}
                }
                foreach (var jobType in model.JobTypes)
                {
                    var dbJobType = jobReferenceJobTypeData.Get(model.Id, jobType.Id);
                    if (dbJobType == null)
                    {
                        if (jobType.IsChecked)
                        {
                            JobReferenceJobType jobReferenceJobType = new JobReferenceJobType();
                            jobReferenceJobType.JobReferenceId = model.Id;
                            jobReferenceJobType.JobTypeId = jobType.Id;
                            jobReferenceJobTypeData.Add(jobReferenceJobType);
                        }
                    }
                    if (jobType.IsChecked)
                    {
                        continue;
                    }
                    else
                    {
                        jobReferenceJobTypeData.Delete(dbJobType.Id);
                    }
                }
                if (jobReference.Images != model.ExistingImages)
                {
                    //foreach (var image in jobReference.Images)
                    //{
                    //    if (image.)
                    //}
                }
                if (model.NewImages != null)
                {
                    for (var i = model.ExistingImages.Count(); i < model.NewImages.Count() + model.ExistingImages.Count(); i++)
                    {
                        var newImage = new Image();
                        newImage.FileName = model.Title + i + ".jpg";
                        newImage.JobId = model.Id;
                        Directory.CreateDirectory("wwwroot/Images/" + model.Title);
                        var filePath = Path.Combine("wwwroot/Images/" + model.Title, newImage.FileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.NewImages[(model.ExistingImages.Count() - i) * (-1)].CopyToAsync(fileStream);
                        }
                        newImage = imageData.Add(newImage);
                    }
                }
                jobReferenceData.Update(jobReference);
                return RedirectToAction("Gallery", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditJobType(int id)
        {
            var model = jobTypeData.Query().Where(x => x.Id == id)
                .Select(x => new JobTypeCreateEditViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PricePerUnitOfMeasurement = x.PricePerUnitOfMeasurement
                }).FirstOrDefault();
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult EditJobType(JobTypeCreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                JobType jobType;
                jobType = jobTypeData.Get(model.Id);
                jobType.Name = model.Name;
                jobType.PricePerUnitOfMeasurement = model.PricePerUnitOfMeasurement;
                jobTypeData.Update(jobType);
                return RedirectToAction("Services", "Home");
            }
            return View(model);
        }
    }
}