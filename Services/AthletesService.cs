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
    public class AthletesService: IAthletesService
    {
        private IRepository<Athletes> _medalsRepo;
        List<Athletes> _listOfAthletes;
        public AthletesService(IRepository<Athletes> medalsRepo)
        {
            _medalsRepo = medalsRepo;
        }


        List<Athletes> IAthletesService.ImportAthletesData()
        {
            string path = "./assets/excelFiles/Athletes.xlsx";
            using (var stream = System.IO.File.OpenRead(path))
            {
                FormFile excelFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

                var listOfAthletes = new List<Athletes>();
                using (var fileStream = new MemoryStream())
                {
                    excelFile.CopyTo(fileStream);
                    using (var package = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  //taking from 0 position
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            listOfAthletes.Add(new Athletes
                            {
                                Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                Nation = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Discipline = worksheet.Cells[row, 3].Value.ToString().Trim()
                            });
                        }
                    }
                }
                _listOfAthletes = listOfAthletes;
            }
            _medalsRepo.AddData(_listOfAthletes);
            return _listOfAthletes;
            
        }


        IEnumerable<Athletes> IAthletesService.GetAthletesData()
        {
            return _medalsRepo.GetAllData();
        }
    }
}