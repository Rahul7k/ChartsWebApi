using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using charts.web.api.Models;
using charts.web.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace charts.web.api.Controllers
{
    //Extending Session Complexiety
    /* public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    } */

    [ApiController]
    [Route("[controller]")]
    public class ChartsController : ControllerBase
    {
        IMedalsService _medalsService;
        ITeamsService _teamsService;
        IAthletesService _athletesService;
        ICoachesService _coachesService;
        IEntriesGenderService _entriesGenderService;
        private IHostEnvironment _hostEnvironment;

        public ChartsController(IMedalsService medalsService, ITeamsService teamsService, IAthletesService athletesService, ICoachesService coachesService, IEntriesGenderService entriesGenderService, IHostEnvironment hostEnvironment)
        {
            _athletesService = athletesService;
            _coachesService = coachesService;
            _entriesGenderService = entriesGenderService;
            _medalsService = medalsService;
            _teamsService = teamsService;
            _hostEnvironment = hostEnvironment;
        }

        
        [HttpPost("uploadFiles"), DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "assets/excelFiles";
                string webRootPath = _hostEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if(!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok("Upload Successful");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }



        [HttpPost("insertMedalsToDb")]
        public IActionResult ImportMedalsFile()
        {
            List<Medals> listOfMedals = _medalsService.ImportMedalsData();
            //HttpContext.Session.SetComplexData("medalsRecord", listOfMedals);
            return Ok(listOfMedals);
        }

        [HttpGet("getMedalsRecord")]
        public IEnumerable<Medals> GetMedalsRecord()
        {
            return _medalsService.GetMedalsData();
        }

        [HttpPost("insertAthletesToDb")]
        public IActionResult ImportAthletesFile()
        {
            List<Athletes> listOfAthletes = _athletesService.ImportAthletesData();
            return Ok(listOfAthletes);
        }

        [HttpGet("getAthletesRecord")]
        public IEnumerable<Athletes> GetAthletesRecord()
        {
            return _athletesService.GetAthletesData();
        }

        [HttpPost("insertCoachesToDb")]
        public IActionResult ImportCoachesFile()
        {
            List<Coaches> listOfCoaches = _coachesService.ImportCoachesData();
            return Ok(listOfCoaches);
        }

        [HttpGet("getCoachesRecord")]
        public IEnumerable<Coaches> GetCoachesRecord()
        {
            return _coachesService.GetCoachesData();
        }

        [HttpPost("insertEntriesGenderToDb")]
        public IActionResult ImportEntriesGenderFile()
        {
            List<EntriesGender> listOfEntriesGender = _entriesGenderService.ImportEntriesGenderData();
            return Ok(listOfEntriesGender);
        }

        [HttpGet("getEntriesGenderRecord")]
        public IEnumerable<EntriesGender> GetEntriesGenderRecord()
        {
            return _entriesGenderService.GetEntriesGenderData();
        }

        [HttpPost("insertTeamsToDb")]
        public IActionResult ImportTeamsFile()
        {
            List<Teams> listOfTeams = _teamsService.ImportTeamsData();
            return Ok(listOfTeams);
        }

        [HttpGet("getTeamsRecord")]
        public IEnumerable<Teams> GetTeamsRecord()
        {
            return _teamsService.GetTeamsData();
        }
    }
}