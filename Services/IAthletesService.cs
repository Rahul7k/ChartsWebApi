using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charts.web.api.Models;

namespace charts.web.api.Services
{
    public interface IAthletesService
    {
        public List<Athletes> ImportAthletesData(string fileName);
        IEnumerable<Athletes> GetAthletesData();
        IEnumerable<AthleteNation> FilterByNation();
    }
}