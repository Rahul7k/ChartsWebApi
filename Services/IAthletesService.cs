using System.Collections.Generic;
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