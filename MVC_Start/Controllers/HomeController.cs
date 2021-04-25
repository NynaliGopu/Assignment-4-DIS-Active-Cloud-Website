using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Start.Models;
using Newtonsoft.Json;
using System.Net.Http;
using MVC_Start.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MVC_Start.Controllers
{
    public class HomeController : Controller
    {

        public ApplicationDbContext dbContext;

        private static List<SchoolData> schoolsInformation;
        private static List<SchoolData> filteredInformation;
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        HttpClient httpClient;
        HttpClient httpClient2;



        static string BASE_URL = "https://api.data.gov/ed/collegescorecard/v1/";
        static string API_KEY = "9dbWmdA3TMdPpeGvydxxHox4QPt3cTtUSLrWEait";

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();


        }

        public string replaceString(string responseString)
        {
            string responseStringModified = responseString;
            responseStringModified = responseStringModified.Replace("school.name", "schoolname");
            responseStringModified = responseStringModified.Replace("school.city", "schoolcity");
            responseStringModified = responseStringModified.Replace("school.state", "schoolstate");
            responseStringModified = responseStringModified.Replace("school.zip", "schoolzip");
            responseStringModified = responseStringModified.Replace("latest.programs.cip_4_digit", "data");

            return responseStringModified;
        }


        public IActionResult Searchuniv()
        {


            var schoolDataList = new List<SchoolData>();
            if (!dbContext.Schools.Any())
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string SCHOOL_DATA_API_PATH = BASE_URL + "schools?fields=id,ope6_id,school.name,school.city,school.state,school.zip";

                string schooldata = "";



                Schools schools = null;

                httpClient.BaseAddress = new Uri(SCHOOL_DATA_API_PATH);

                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(SCHOOL_DATA_API_PATH).GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        schooldata = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        schooldata = replaceString(schooldata);
                    }

                    if (!schooldata.Equals(""))
                    {

                        schools = JsonConvert.DeserializeObject<Schools>(schooldata);
                        schools.results.ToList().ForEach(s =>
                        {
                            var school = new SchoolData()
                            {
                                //id = s.id,
                                ope6_id = s.ope6_id,


                                schoolcity = s.schoolcity,
                                schoolname = s.schoolname,
                                schoolstate = s.schoolstate,
                                schoolzip = s.schoolzip


                            };

                            dbContext.Schools.Add(school);
                        });
                        dbContext.SaveChanges();
                    }
                    schoolDataList = dbContext.Schools.ToList();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                schoolDataList = dbContext.Schools.ToList();
            }
            ViewBag.Schools = schoolDataList;
            schoolsInformation = schoolDataList;
            if (!dbContext.Programs.Any())
            {
                dbContext.Database.EnsureCreated();
                ProgramData[] plist = new ProgramData[]
                    {


                     new ProgramData{code="1", title="Management Sciences and Quantitative Methods", ope6_id="001002", unit_id=1 },
                     new ProgramData { code = "2", title = "Journalism", ope6_id = "001002", unit_id = 2 },
                     new ProgramData { code = "3", title = "Entrepreneurial and Small Business Operations", ope6_id = "001002", unit_id = 3 },
                     new ProgramData { code = "4", title = "Public Relations, Advertising, and Applied Communication", ope6_id = "001003", unit_id = 4 },
                     new ProgramData { code = "5", title = "Geological and Earth Sciences/Geosciences", ope6_id = "001003", unit_id = 5 },
                     new ProgramData { code = "6", title = "International Business", ope6_id = "001003", unit_id = 6 },
                     new ProgramData { code = "7", title = "Communication and Media Studies", ope6_id = "041872", unit_id = 7 },
                     new ProgramData { code = "8", title = "Computer/Information Technology Administration and Management", ope6_id = "041872", unit_id = 8 },
                     new ProgramData { code = "9", title = "Food Science and Technology", ope6_id = "041872", unit_id = 9 },
                     new ProgramData { code = "10", title = "Parks, Recreation and Leisure Facilities Management", ope6_id = "001017", unit_id = 10 },
                     new ProgramData { code = "11", title = "Family and Consumer Sciences/Human Sciences, General", ope6_id = "001017", unit_id = 11 },
                     new ProgramData { code = "12", title = "Business, Management, Marketing, and Related Support Services, Other", ope6_id = "001017", unit_id = 12 },
                     new ProgramData { code = "13", title = "Computer Systems Networking and Telecommunicationss", ope6_id = "001060", unit_id = 13 },
                     new ProgramData { code = "14", title = "Computer Software and Media Applications", ope6_id = "001060", unit_id = 14 },
                     new ProgramData { code = "15", title = "Vehicle Maintenance and Repair Technologies", ope6_id = "001060", unit_id = 15 },
                     new ProgramData { code = "16", title = "Management Sciences and Quantitative Methods", ope6_id = "001015", unit_id = 16 },
                     new ProgramData { code = "17", title = "Journalism", ope6_id = "001015", unit_id = 17 },
                     new ProgramData { code = "18", title = "Entrepreneurial and Small Business Operations", ope6_id = "001015", unit_id = 18 },
                     new ProgramData { code = "19", title = "Public Relations, Advertising, and Applied Communication", ope6_id = "013039", unit_id = 19 },
                     new ProgramData { code = "20", title = "Geological and Earth Sciences/Geosciences", ope6_id = "013039", unit_id = 20 },
                     new ProgramData { code = "21", title = "International Business", ope6_id = "013039", unit_id = 21 },
                     new ProgramData { code = "22", title = "Communication and Media Studies", ope6_id = "012182", unit_id = 22 },
                     new ProgramData { code = "23", title = "Computer/Information Technology Administration and Management", ope6_id = "012182", unit_id = 23 },
                     new ProgramData { code = "24", title = "Food Science and Technology", ope6_id = "012182", unit_id = 24 },
                     new ProgramData { code = "25", title = "Parks, Recreation and Leisure Facilities Management", ope6_id = "001012", unit_id = 25 } ,
                     new ProgramData { code = "26", title = "Family and Consumer Sciences/Human Sciences, General", ope6_id = "001012", unit_id = 26 },
                     new ProgramData { code = "27", title = "Business, Management, Marketing, and Related Support Services, Other", ope6_id = "001012", unit_id = 27 },
                     new ProgramData { code = "28", title = "Computer Systems Networking and Telecommunicationss", ope6_id = "001009", unit_id = 28 },
                     new ProgramData { code = "29", title = "Computer Software and Media Applications", ope6_id = "001009", unit_id = 29 },
                     new ProgramData { code = "30", title = "Vehicle Maintenance and Repair Technologies", ope6_id = "001009", unit_id = 30 },
                     new ProgramData { code = "31", title = "Management Sciences and Quantitative Methods", ope6_id = "008310", unit_id = 31 },
                     new ProgramData { code = "32", title = "Journalism", ope6_id = "008310", unit_id = 32 },
                     new ProgramData { code = "33", title = "Entrepreneurial and Small Business Operations", ope6_id = "008310", unit_id = 33 },
                     new ProgramData { code = "34", title = "Public Relations, Advertising, and Applied Communication", ope6_id = "001008", unit_id = 34 },
                     new ProgramData { code = "35", title = "Geological and Earth Sciences/Geosciences", ope6_id = "001008", unit_id = 35 },
                     new ProgramData { code = "36", title = "International Business", ope6_id = "001008", unit_id = 36 },
                     new ProgramData { code = "37", title = "Communication and Media Studies", ope6_id = "001007", unit_id = 37 },
                     new ProgramData { code = "38", title = "Computer/Information Technology Administration and Management", ope6_id = "001007", unit_id = 38 },
                     new ProgramData { code = "39", title = "Food Science and Technology", ope6_id = "001007", unit_id = 39 },
                     new ProgramData { code = "40", title = "Parks, Recreation and Leisure Facilities Management", ope6_id = "001051", unit_id = 40 },
                     new ProgramData { code = "41", title = "Family and Consumer Sciences/Human Sciences, General", ope6_id = "001051", unit_id = 41 },
                     new ProgramData { code = "42", title = "Business, Management, Marketing, and Related Support Services, Other", ope6_id = "001051", unit_id = 42 },
                     new ProgramData { code = "43", title = "Computer Systems Networking and Telecommunicationss", ope6_id = "001005", unit_id = 43 },
                     new ProgramData { code = "44", title = "Computer Software and Media Applications", ope6_id = "001005", unit_id = 44 },
                     new ProgramData { code = "45", title = "Vehicle Maintenance and Repair Technologies", ope6_id = "001005", unit_id = 45 },
                     new ProgramData { code = "46", title = "Management Sciences and Quantitative Methods", ope6_id = "001055", unit_id = 46 },
                     new ProgramData { code = "47", title = "Journalism", ope6_id = "001055", unit_id = 47 },
                     new ProgramData { code = "48", title = "Entrepreneurial and Small Business Operations", ope6_id = "001055", unit_id = 48 },
                     new ProgramData { code = "49", title = "Public Relations, Advertising, and Applied Communication", ope6_id = "025034", unit_id = 49 },
                     new ProgramData { code = "50", title = "Geological and Earth Sciences/Geosciences", ope6_id = "025034", unit_id = 50 },
                     new ProgramData { code = "51", title = "International Business", ope6_id = "025034", unit_id = 51 },
                     new ProgramData { code = "52", title = "Communication and Media Studies", ope6_id = "001052", unit_id = 52 },
                     new ProgramData { code = "53", title = "Computer/Information Technology Administration and Management", ope6_id = "001052", unit_id = 53 },
                     new ProgramData { code = "54", title = "Food Science and Technology", ope6_id = "001052", unit_id = 54 },
                     new ProgramData { code = "55", title = "Parks, Recreation and Leisure Facilities Management", ope6_id = "001018", unit_id = 55 },
                     new ProgramData { code = "56", title = "Family and Consumer Sciences/Human Sciences, General", ope6_id = "001018", unit_id = 56 },
                     new ProgramData { code = "57", title = "Business, Management, Marketing, and Related Support Services, Other", ope6_id = "001018", unit_id = 57 },
                     new ProgramData { code = "58", title = "Computer Systems Networking and Telecommunicationss", ope6_id = "007871", unit_id = 58 },
                     new ProgramData { code = "59", title = "Computer Software and Media Applications", ope6_id = "007871", unit_id = 59 },
                     new ProgramData { code = "60", title = "Vehicle Maintenance and Repair Technologies", ope6_id = "007871", unit_id = 60 },

            };
                foreach (ProgramData p in plist)
                {
                    dbContext.Programs.Add(p);
                }
                dbContext.SaveChanges();

            }

            return View(schoolDataList);
        }

        [HttpPost]
        public void SearchResults(string id)
        {
            filteredInformation = schoolsInformation.Where(x => x.schoolname.ToLower().Contains(id.ToLower())).ToList();

            return;
        }

        public IActionResult SearchResults()
        {
            ViewBag.Schools = filteredInformation;
            return View();
        }

        public IActionResult Programdetails(string id)
        {

            var programDataList = new List<ProgramData>();


            TempData["ope6Id"] = id;
            programDataList = dbContext.Programs.ToList();
            ViewBag.Programs = programDataList.Where(x => x.ope6_id == id).ToList();
            return View(programDataList);
        }


        public IActionResult Update(string id)
        {

            ProgramData datatoupdate = dbContext.Programs.Where(x => x.code == id).FirstOrDefault();

            ProgramData update = new ProgramData()
            {

                ID = datatoupdate.ID,
                code = datatoupdate.code,
                title = datatoupdate.title,
                ope6_id = datatoupdate.ope6_id,
                unit_id = datatoupdate.unit_id

            };


            return View(update);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(string id, [Bind("code,title,ope6_id,unit_id")] ProgramData newupdated)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    ProgramData programtobeupdated = dbContext.Programs
                                  .Where(p => p.code == id)
                                    .FirstOrDefault();

                    programtobeupdated.title = newupdated.title;

                    dbContext.Update(programtobeupdated);
                    dbContext.SaveChanges();
                    //await _dbcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Thanks), new { id = "Thanks! The record has been edited." });
                }
            }
            catch (DbUpdateException /* ex */)
            {
                // Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes");
            }

            return View();
        }


        public async Task<IActionResult> Delete(string id)
        {
            ProgramData programtobedeleted = dbContext.Programs
                          .Where(p => p.code == id)
                            .FirstOrDefault();
            if (programtobedeleted != null)
            {
                dbContext.Programs.Remove(programtobedeleted);
                dbContext.SaveChanges();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleteconfirmed(string id)
        {
            ProgramData programtobedeleted = dbContext.Programs
                          .Where(p => p.code == id)
                            .FirstOrDefault();
            dbContext.Programs.Remove(programtobedeleted);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Thanks), new { id = "The record has been deleted." });
        }

        public IActionResult Thanks(string id)
        {
            ViewBag.message = id;
            return View();
        }

        public IActionResult Createnew()
        {
            return View();
        }

        [HttpPost]
        public void Createnew(string id)
        {
            int data = dbContext.Programs.OrderByDescending(x => x.unit_id).First().unit_id;
            ProgramData pg = new ProgramData
            {
                ope6_id = TempData["ope6Id"].ToString(),
                title = id,
                code = (data + 1).ToString(),
                unit_id = data + 1
            };
            dbContext.Programs.Add(pg);
            dbContext.SaveChanges();
          
        }

    }
}