using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace charts.web.api.Repository
{
    public interface IRepository<T> where T: class
    {
        //void DeleteData();
        void AddData(List<T> dataFromExcel);
        IEnumerable<T> GetAllData();
    }
}