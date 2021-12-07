using System.Collections.Generic;
using charts.web.api.Models;

namespace charts.web.api.Services
{
    public interface ICoachesService
    {
        public List<Coaches> ImportCoachesData(string fileName);
        IEnumerable<Coaches> GetCoachesData();
    }
}