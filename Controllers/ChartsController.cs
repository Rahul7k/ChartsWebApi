using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charts.web.api.Models;
using charts.web.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace charts.web.api.Controllers
{
    //Extending Session Complexiety
    public static class SessionExtensions
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
    }

    [ApiController]
    [Route("[controller]")]
    public class ChartsController: ControllerBase
    {
        IMedalsService _medalsService;
        public ChartsController(IMedalsService medalsService)
        {
            _medalsService = medalsService;
        }

        [HttpPost("insertMedalsFile")]
        public void ImportMedalsFile(IFormFile excelFile)
        {
            List<Medals> listOfMedals = _medalsService.ImportMedalsData(excelFile);
            HttpContext.Session.SetComplexData("medalsRecord", listOfMedals);
        }

        [HttpPost("addMedalsRecord")]
        public void AddMedalsRecord()
        {
            _medalsService.AddMedalsData(HttpContext.Session.GetComplexData<List<Medals>>("medalsRecord"));
        }

        [HttpGet("getMedalsRecord")]
        public IEnumerable<Medals> GetMedalsRecord()
        {
            return _medalsService.GetMedalsData();
        }
    }
}