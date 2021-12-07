using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace charts.web.api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ChartsDbContext _dbContext;
        private DbSet<T> _dataToTable;
        public Repository(ChartsDbContext dbContext)
        {
           _dbContext = dbContext;
           _dataToTable = dbContext.Set<T>(); 
        }

        /* void IRepository<T>.DeleteData()
        {
            foreach(var data in _dataToTable)
            {
                _dataToTable.Remove(data);
            }
            _dbContext.SaveChanges();
        } */


        void IRepository<T>.AddData(List<T> dataFromExcel)
        {
            if(dataFromExcel==null)
            {
                throw new ArgumentNullException("No Entries in Excel File");
            }

            foreach(var data in _dataToTable)
            {
                _dataToTable.Remove(data);
            }
            _dbContext.SaveChanges();

            foreach(var excelData in dataFromExcel)
            {
                _dataToTable.Add(excelData);
            }
            _dbContext.SaveChanges();
        }

        IEnumerable<T> IRepository<T>.GetAllData()
        {
            return _dataToTable.ToList<T>();
        }

        void IRepository<T>.DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}