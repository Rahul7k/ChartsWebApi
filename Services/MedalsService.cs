using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using charts.web.api.Models;
using charts.web.api.Repository;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace charts.web.api.Services
{
    public class MedalsService : IMedalsService
    {
        private IRepository<Medals> _medalsRepo;
        public MedalsService(IRepository<Medals> medalsRepo)
        {
            _medalsRepo = medalsRepo;
        }

        List<Medals> IMedalsService.ImportMedalsData(IFormFile excelFile)
        {
            var listOfMedals = new List<Medals>();
            using(var stream = new MemoryStream())
            {
                excelFile.CopyToAsync(stream);
                using(var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  //taking from 0 position
                    var rowcount = worksheet.Dimension.Rows;
                    for(int row=2; row <= rowcount; row++)
                    {
                        listOfMedals.Add(new Medals{
                            Rank = Convert.ToInt32(worksheet.Cells[row,1].Value.ToString().Trim()),
                            Nation = worksheet.Cells[row,2].Value.ToString().Trim(),
                            Gold = Convert.ToInt32(worksheet.Cells[row,3].Value.ToString().Trim()),
                            Silver = Convert.ToInt32(worksheet.Cells[row,4].Value.ToString().Trim()),
                            Bronze = Convert.ToInt32(worksheet.Cells[row,5].Value.ToString().Trim()),
                            Total = Convert.ToInt32(worksheet.Cells[row,6].Value.ToString().Trim()),
                            RankByTotal = Convert.ToInt32(worksheet.Cells[row,7].Value.ToString().Trim())
                        });
                    }
                }
            }
            return listOfMedals;
        }
        void IMedalsService.AddMedalsData(List<Medals> listOfMedals)
        {
            _medalsRepo.AddData(listOfMedals);
        }

        IEnumerable<Medals> IMedalsService.GetMedalsData()
        {
            return _medalsRepo.GetAllData();
        }

    }
}