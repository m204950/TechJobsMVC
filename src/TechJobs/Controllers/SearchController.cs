using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        public IActionResult Results(string searchType, string searchTerm)
        {
 
            List<Dictionary<string, string>> jobs = new List<Dictionary<string, string>>();
            if (searchType == "all")
            {
                List<Dictionary<string, string>> allJobs;
                allJobs = JobData.FindAll();
                string[] jobKeys = new string[allJobs[0].Count];
                allJobs[0].Keys.CopyTo(jobKeys, 0);


                foreach (Dictionary<string, string> row in allJobs)
                {
                    bool found = false;
                    foreach (string myKey in jobKeys)
                    {

                        string aValue = row[myKey];

                        if (aValue.ToLower().Contains(searchTerm.ToLower()))
                        {
                            found = true;
                        }
                    }
                    if (found)
                    {
                        jobs.Add(row);
                    }
                }
            }
            else
            {
                jobs = JobData.FindByColumnAndValue(searchType, searchTerm);
            }
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            ViewBag.jobs = jobs;
            return View("/Views/Search/Index.cshtml");
        }

    }
}
