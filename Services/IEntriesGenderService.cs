using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using charts.web.api.Models;

namespace charts.web.api.Services
{
    public interface IEntriesGenderService
    {
        public List<EntriesGender> ImportEntriesGenderData();
        IEnumerable<EntriesGender> GetEntriesGenderData();
    }
}