using System.Collections.Generic;

namespace charts.web.api.Services
{
    public interface IBaseService
    {
        List<string> AvailableFiles();
        void DeleteAvailableFiles(string fileName);
    }
}