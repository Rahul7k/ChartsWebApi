using System.Collections.Generic;
using charts.web.api.Models;

namespace charts.web.api.Services
{
    public interface IEntriesGenderService
    {
        public List<EntriesGender> ImportEntriesGenderData(string fileName);
        IEnumerable<EntriesGender> GetEntriesGenderData();
        IEnumerable<EntriesGender> FilterByGames();

        IEnumerable<EntriesGender> FilterByParticipationMF();

    }
}