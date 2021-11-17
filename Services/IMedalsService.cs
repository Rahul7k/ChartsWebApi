using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charts.web.api.Models;
using Microsoft.AspNetCore.Http;

namespace charts.web.api.Services
{
    public interface IMedalsService
    {
        public List<Medals> ImportMedalsData();
        //void AddMedalsData();
        IEnumerable<Medals> GetMedalsData();
    }
}