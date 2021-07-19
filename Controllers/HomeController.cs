using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aditek.Models;
using Aditek.Services;
using Aditek.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Net;
using System.Net.Mime;

namespace Aditek.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private AppDbContext appDbContext { get; }
        private readonly ILogger<HomeController> logger;
        private readonly IImageData imageData;
        private readonly IJobReferenceData jobReferenceData;
        private readonly IJobReferenceJobTypeData jobReferenceJobTypeData;
        private readonly IJobTypeData jobTypeData;
        public HomeController(AppDbContext appDbContext,
                            ILogger<HomeController> logger,
                            IImageData imageData,
                            IJobReferenceData jobReferenceData,
                            IJobReferenceJobTypeData jobReferenceJobTypeData,
                            IJobTypeData jobTypeData)
        {
            this.imageData = imageData;
            this.jobReferenceData = jobReferenceData;
            this.jobReferenceJobTypeData = jobReferenceJobTypeData;
            this.jobTypeData = jobTypeData;
            this.appDbContext = appDbContext;
            this.logger = logger;
        }
        [Route("ONama")]
        public IActionResult About()
        {
            return View();
        }
        [Route("Kontakt")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("GalerijaRadova/Detalji")]
        public IActionResult Details(int id)
        {
            var model = jobReferenceData.Query().Where(x => x.Id == id)
                .Select(x => new JobReferenceCard
                {
                    Title = x.Title,
                    Id = x.Id,
                    Images = x.Images.Select(y => y.FileName).ToList(),
                    JobTypes = x.JobTypes.Select(z => z.JobTypes.Name).ToList()
                }).FirstOrDefault();
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Route("GalerijaRadova")]
        public IActionResult Gallery(string searchString, int? jobTypeId = null)
        {
            var model = new JobReferenceViewModel();
            if (searchString == null && jobTypeId == null)
            {
                model.JobReferenceCards = jobReferenceData.Query()
                    .Select(x => new JobReferenceCard
                    {
                        Title = x.Title,
                        Id = x.Id,
                        Images = x.Images.Select(y => y.FileName).ToList(),
                        JobTypes = x.JobTypes.Where(z => z.JobReferenceId == x.Id).OrderBy(z => z.JobTypes.Name).Select(z => z.JobTypes.Name).ToList()
                    }).ToList();
            }
            else if (searchString == null && jobTypeId != null)
            {
                List<JobReferenceCard> jobReferenceCardsToAdd = new List<JobReferenceCard>();
                var jobReferenceJobTypes = jobReferenceJobTypeData.Query().Where(x => x.JobTypeId == jobTypeId);
                foreach (var jobReferenceJobType in jobReferenceJobTypes.ToList())
                {
                    jobReferenceCardsToAdd.Add(jobReferenceData.Query().Where(x => x.Id == jobReferenceJobType.JobReferenceId)
                .Select(x => new JobReferenceCard
                {
                    Title = x.Title,
                    Id = x.Id,
                    Images = x.Images.Select(y => y.FileName).ToList(),
                    JobTypes = x.JobTypes.Select(z => z.JobTypes.Name).ToList()
                }).FirstOrDefault());
                }
                model.JobReferenceCards = jobReferenceCardsToAdd;
            }
            else if (jobTypeId == null && searchString != null)
            {
                model.JobReferenceCards = jobReferenceData.Query().Where(x => x.Title.Contains(searchString)).Select(x => new JobReferenceCard
                {
                    Title = x.Title,
                    Id = x.Id,
                    Images = x.Images.Select(y => y.FileName).ToList(),
                    JobTypes = x.JobTypes.Select(z => z.JobTypes.Name).ToList()
                }).ToList();
            }
            else
            {
                List<JobReferenceCard> jobReferenceCardsToAdd = new List<JobReferenceCard>();
                List<JobReferenceCard> jobReferences = new List<JobReferenceCard>();
                var jobReferenceJobTypes = jobReferenceJobTypeData.Query().Where(x => x.JobTypeId == jobTypeId);
                jobReferences.Add(jobReferenceData.Query().Where(x => x.Title.Contains(searchString))
                    .Select(x => new JobReferenceCard
                    {
                        Title = x.Title,
                        Id = x.Id,
                        Images = x.Images.Select(y => y.FileName).ToList(),
                        JobTypes = x.JobTypes.Select(z => z.JobTypes.Name).ToList()
                    }).FirstOrDefault());
                if (jobReferences[0] != null)
                {
                    foreach (var jobReference in jobReferences)
                    {
                        foreach (var jobReferenceJobType in jobReferenceJobTypes)
                        {
                            if (jobReference.Id == jobReferenceJobType.JobReferenceId)
                            {
                                jobReferenceCardsToAdd.Add(jobReference);
                            }
                        }
                    }
                }
                model.JobReferenceCards = jobReferenceCardsToAdd;
            }
            model.JobTypes = jobTypeData.GetAll().ToList();
            return View(model);
        }
        public JsonResult GalleryAutocomplete(string term)
        {
            List<string> result;
            result = appDbContext.JobReferences.Where(x => x.Title.StartsWith(term)).Select(y => y.Title).ToList();
            return Json(result);
        }
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Usluge")]
        public IActionResult Services()
        {
            var model = new JobTypeViewModel();
            model.jobTypes = jobTypeData.Query();
            return View(model);
        }
        public IActionResult SubmitForm(Email model)
        {

            EmailSender emailSender = new EmailSender();
            emailSender.sendMail(model);
            return View("Contact");
        }
    }
}