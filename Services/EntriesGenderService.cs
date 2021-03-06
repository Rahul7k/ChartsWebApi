using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using charts.web.api.Models;
using charts.web.api.Repository;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace charts.web.api.Services
{
    public class EntriesGenderService: IEntriesGenderService
    {
        private IRepository<EntriesGender> _entriesGenderRepo;
        List<EntriesGender> _listOfEntriesGender;
        public EntriesGenderService(IRepository<EntriesGender> entriesGenderRepo)
        {
            _entriesGenderRepo = entriesGenderRepo;
        }

        List<EntriesGender> IEntriesGenderService.ImportEntriesGenderData(string fileName)
        {
            string path = "./assets/excelFiles/"+fileName;
            using (var stream = System.IO.File.OpenRead(path))
            {
                FormFile excelFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

                var listOfEntriesGender = new List<EntriesGender>();
                using (var fileStream = new MemoryStream())
                {
                    excelFile.CopyTo(fileStream);
                    using (var package = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  //taking from 0 position
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            listOfEntriesGender.Add(new EntriesGender
                            {
                                Discipline = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                Female = Convert.ToInt32(worksheet.Cells[row, 2].Value.ToString().Trim()),
                                Male = Convert.ToInt32(worksheet.Cells[row, 3].Value.ToString().Trim()),
                                Total = Convert.ToInt32(worksheet.Cells[row, 4].Value.ToString().Trim())
                            });
                        }
                    }
                }
                _listOfEntriesGender = listOfEntriesGender;
            }
            _entriesGenderRepo.AddData(_listOfEntriesGender);
            _entriesGenderRepo.DeleteFile(path);
            return _listOfEntriesGender;
            
        }

        IEnumerable<EntriesGender> IEntriesGenderService.GetEntriesGenderData()
        {
            return _entriesGenderRepo.GetAllData();
        }

        IEnumerable<EntriesGender> IEntriesGenderService.FilterByGames()
        {
            IEnumerable<EntriesGender> topFive = _entriesGenderRepo.GetAllData().OrderByDescending(x=>x.Total).Take(5);
            return topFive;
            
        }

        IEnumerable<EntriesGender> IEntriesGenderService.FilterByParticipationMF()
        {
            return _entriesGenderRepo.GetAllData().OrderByDescending(x=>x.Total).Take(5);
        }
    }
}