using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        public IActionResult Index(int id)
        {
            SearchJobsViewModel jobsViewModel = new SearchJobsViewModel
            {
                Jobs = jobData.Jobs
            };

            foreach (var i in jobsViewModel.Jobs)
            {
                if (i.ID == id)
                {

                    return View(i);

                }
            }
            return Redirect("/Job?id=1");
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {


            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = jobData.Employers.Find(newJobViewModel.EmployerID),
                    Location = jobData.Locations.Find(newJobViewModel.LocationID),
                    CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID),
                    PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID)

                };

                jobData.Jobs.Add(newJob);
                return Redirect(string.Format("/Job?id={0}", newJob.ID));
            }

            return View(newJobViewModel);
        }
    }
}