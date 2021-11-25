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
        List<Medals> _listOfMedals;
        public MedalsService(IRepository<Medals> medalsRepo)
        {
            _medalsRepo = medalsRepo;
        }

        /* List<Medals> IMedalsService.ImportMedalsData(IFormFile excelFile)
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
        }  */

        List<Medals> IMedalsService.ImportMedalsData(string fileName)
        {
            string path = "./assets/excelFiles/"+fileName;
            //string path = _medalsRepo.GetFilePath();
            using (var stream = System.IO.File.OpenRead(path))
            {
                FormFile excelFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

                var listOfMedals = new List<Medals>();
                using (var fileStream = new MemoryStream())
                {
                    excelFile.CopyTo(fileStream);
                    using (var package = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  //taking from 0 position
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            listOfMedals.Add(new Medals
                            {
                                Rank = Convert.ToInt32(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                Nation = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Gold = Convert.ToInt32(worksheet.Cells[row, 3].Value.ToString().Trim()),
                                Silver = Convert.ToInt32(worksheet.Cells[row, 4].Value.ToString().Trim()),
                                Bronze = Convert.ToInt32(worksheet.Cells[row, 5].Value.ToString().Trim()),
                                Total = Convert.ToInt32(worksheet.Cells[row, 6].Value.ToString().Trim()),
                                RankByTotal = Convert.ToInt32(worksheet.Cells[row, 7].Value.ToString().Trim())
                            });
                        }
                    }
                }
                _listOfMedals = listOfMedals;
            }
            _medalsRepo.AddData(_listOfMedals);
            _medalsRepo.DeleteFile(path);
            return _listOfMedals;
            
        }


        /* void IMedalsService.AddMedalsData()
        {
            _medalsRepo.AddData(_listOfMedals);
        } */

        IEnumerable<Medals> IMedalsService.GetMedalsData()
        {
            return _medalsRepo.GetAllData();
        }

        IEnumerable<Medals> IMedalsService.FilterByMedals()
        {
            return _medalsRepo.GetAllData().OrderByDescending(x=>x.Total).Take(5);
        }

        IEnumerable<Medals> IMedalsService.FilterByGold()
        {
            return _medalsRepo.GetAllData().OrderByDescending(x=>x.Gold).Take(5);
        }

        IEnumerable<Medals> IMedalsService.FilterBySilver()
        {
            return _medalsRepo.GetAllData().OrderByDescending(x=>x.Silver).Take(5);
        }

        IEnumerable<Medals> IMedalsService.FilterByBronze()
        {
            return _medalsRepo.GetAllData().OrderByDescending(x=>x.Bronze).Take(5);
        }

    }
}