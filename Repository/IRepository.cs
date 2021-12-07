using System.Collections.Generic;

namespace charts.web.api.Repository
{
    public interface IRepository<T> where T: class
    {
        //void DeleteData();
        void AddData(List<T> dataFromExcel);
        IEnumerable<T> GetAllData();

        void DeleteFile(string filePath);
    }
}