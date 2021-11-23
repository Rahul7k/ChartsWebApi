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
    public class CoachesService: ICoachesService
    {
        private IRepository<Coaches> _coachesRepo;
        List<Coaches> _listOfCoaches;
        public CoachesService(IRepository<Coaches> coachesRepo)
        {
            _coachesRepo = coachesRepo;
        }

        List<Coaches> ICoachesService.ImportCoachesData(string fileName)
        {
            string path = "./assets/excelFiles/"+fileName;
            using (var stream = System.IO.File.OpenRead(path))
            {
                FormFile excelFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

                var listOfCoaches = new List<Coaches>();
                using (var fileStream = new MemoryStream())
                {
                    excelFile.CopyTo(fileStream);
                    using (var package = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  //taking from 0 position
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            listOfCoaches.Add(new Coaches
                            {
                                Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                Nation = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Discipline = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                Event = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                SNo = Convert.ToInt32(worksheet.Cells[row, 5].Value.ToString().Trim())
                            });
                        }
                    }
                }
                _listOfCoaches = listOfCoaches;
            }
            _coachesRepo.AddData(_listOfCoaches);
            _coachesRepo.DeleteFile(path);
            return _listOfCoaches;
            
        }



        IEnumerable<Coaches> ICoachesService.GetCoachesData()
        {
            return _coachesRepo.GetAllData();
        }
    }
}