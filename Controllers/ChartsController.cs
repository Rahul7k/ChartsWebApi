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
        IBaseService _baseService;
        private IHostEnvironment _hostEnvironment;

        public ChartsController(IMedalsService medalsService, ITeamsService teamsService, IAthletesService athletesService, ICoachesService coachesService, IEntriesGenderService entriesGenderService, IBaseService baseService, IHostEnvironment hostEnvironment)
        {
            _athletesService = athletesService;
            _coachesService = coachesService;
            _entriesGenderService = entriesGenderService;
            _medalsService = medalsService;
            _teamsService = teamsService;
            _baseService = baseService;
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
        public List<Medals> ImportMedalsFile([FromBody] ApiModel fileName)
        {
            List<Medals> listOfMedals = _medalsService.ImportMedalsData(string.Format("{0}", fileName.apiData));
            //HttpContext.Session.SetComplexData("medalsRecord", listOfMedals);
            return listOfMedals;
        }

        [HttpGet("getMedalsRecord")]
        public IEnumerable<Medals> GetMedalsRecord()
        {
            return _medalsService.GetMedalsData();
        }

        [HttpPost("insertAthletesToDb")]
        public IActionResult ImportAthletesFile([FromBody] ApiModel fileName)
        {
            List<Athletes> listOfAthletes = _athletesService.ImportAthletesData(string.Format("{0}", fileName.apiData));
            return Ok(listOfAthletes);
        }

        [HttpGet("getAthletesRecord")]
        public IEnumerable<Athletes> GetAthletesRecord()
        {
            return _athletesService.GetAthletesData();
        }

        [HttpPost("insertCoachesToDb")]
        public IActionResult ImportCoachesFile([FromBody] ApiModel fileName)
        {
            List<Coaches> listOfCoaches = _coachesService.ImportCoachesData(string.Format("{0}", fileName.apiData));
            return Ok(listOfCoaches);
        }

        [HttpGet("getCoachesRecord")]
        public IEnumerable<Coaches> GetCoachesRecord()
        {
            return _coachesService.GetCoachesData();
        }

        [HttpPost("insertEntriesGenderToDb")]
        public IActionResult ImportEntriesGenderFile([FromBody] ApiModel fileName)
        {
            List<EntriesGender> listOfEntriesGender = _entriesGenderService.ImportEntriesGenderData(string.Format("{0}", fileName.apiData));
            return Ok(listOfEntriesGender);
        }

        [HttpGet("getEntriesGenderRecord")]
        public IEnumerable<EntriesGender> GetEntriesGenderRecord()
        {
            return _entriesGenderService.GetEntriesGenderData();
        }

        [HttpPost("insertTeamsToDb")]
        public IActionResult ImportTeamsFile([FromBody] ApiModel fileName)
        {
            List<Teams> listOfTeams = _teamsService.ImportTeamsData(string.Format("{0}", fileName.apiData));
            return Ok(listOfTeams);
        }

        [HttpGet("getTeamsRecord")]
        public IEnumerable<Teams> GetTeamsRecord()
        {
            return _teamsService.GetTeamsData();
        }
        
        [HttpGet("getFiles")]
        public IActionResult GetAllFiles()
        {
            return Ok(_baseService.AvailableFiles());
        }

        [HttpGet("filterMedalsByTotal")]
        public IActionResult FilterMedalsByTotal()
        {
            var data = _medalsService.FilterByMedals().AsEnumerable().Select(x=>new{
                total=x.Total,
                nation=x.Nation
            });
            return Ok(data);
        }

        [HttpGet("filterMedalsByGold")]
        public IActionResult FilterMedalsByGold()
        {
            var data = _medalsService.FilterByGold().AsEnumerable().Select(x=>new{
                gold=x.Gold,
                nation=x.Nation
            });
            return Ok(data);
        }

        [HttpGet("filterMedalsBySilver")]
        public IActionResult FilterMedalsBySilver()
        {
            var data = _medalsService.FilterBySilver().AsEnumerable().Select(x=>new{
                silver=x.Silver,
                nation=x.Nation
            });
            return Ok(data);
        }

        [HttpGet("filterMedalsByBronze")]
        public IActionResult FilterMedalsByBronze()
        {
            var data = _medalsService.FilterByBronze().AsEnumerable().Select(x=>new{
                bronze=x.Bronze,
                nation=x.Nation
            });
            return Ok(data);
        }

        [HttpGet("filterGamesByParticipation")]
        public IActionResult FilterGamesByParticipation()
        {
            var data = _entriesGenderService.FilterByGames().AsEnumerable().Select(x=>new{
                total= x.Total,
                game = x.Discipline
            });

            return Ok(data);
        }

        [HttpGet("filterGamesByParticipationMF")]
        public IActionResult FilterGamesByParticipationMF()
        {
            var data = _entriesGenderService.FilterByParticipationMF().AsEnumerable().Select(x=>new{
                total= x.Total,
                male = x.Male,
                female = x.Female,
                game = x.Discipline
            });

            return Ok(data);
        }

        [HttpGet("filterAthletesByNation")]
        public IActionResult FilterAthletesByNation()
        {
            return Ok(_athletesService.FilterByNation());
        }

    }
}