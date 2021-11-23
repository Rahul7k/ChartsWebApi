using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charts.web.api.Models;

namespace charts.web.api.Services
{
    public interface ITeamsService
    {
        public List<Teams> ImportTeamsData(string fileName);
        IEnumerable<Teams> GetTeamsData(); 
    }
}