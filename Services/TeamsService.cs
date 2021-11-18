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
    public class TeamsService: ITeamsService
    {
        private IRepository<Teams> _teamsRepo;
        List<Teams> _listOfTeams;
        public TeamsService(IRepository<Teams> teamsRepo)
        {
            _teamsRepo = teamsRepo;
        }

        List<Teams> ITeamsService.ImportTeamsData()
        {
            string path = "./assets/excelFiles/Teams.xlsx";
            using (var stream = System.IO.File.OpenRead(path))
            {
                FormFile excelFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

                var listOfTeams = new List<Teams>();
                using (var fileStream = new MemoryStream())
                {
                    excelFile.CopyTo(fileStream);
                    using (var package = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];  //taking from 0 position
                        var rowcount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            listOfTeams.Add(new Teams
                            {
                                Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                Discipline = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                Nation = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                Event = worksheet.Cells[row, 4].Value.ToString().Trim()
                            });
                        }
                    }
                }
                _listOfTeams = listOfTeams;
            }
            _teamsRepo.AddData(_listOfTeams);
            return _listOfTeams;
            
        }


        IEnumerable<Teams> ITeamsService.GetTeamsData()
        {
            return _teamsRepo.GetAllData();
        }
    }
}