using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace charts.web.api.Services
{
    public class BaseService : IBaseService
    {
        List<string> IBaseService.AvailableFiles()
        {
            string[] filePaths = Directory.GetFiles("./assets/excelFiles", "*.xlsx", SearchOption.TopDirectoryOnly);
            List<string> listFiles = filePaths.ToList();

            List<string> availableFiles = new List<string>();

            foreach (string str in listFiles)
            {
                string fileName = Path.GetFileName(str);
                availableFiles.Add(fileName);
            }

            return availableFiles;

        }

        void IBaseService.DeleteAvailableFiles(string fileName)
        {
            string rootFolder = "./assets/excelFiles"; 
            string fileToDelete = fileName;
            if(File.Exists(Path.Combine(rootFolder, fileToDelete)))
            {
                File.Delete(Path.Combine(rootFolder, fileToDelete));
            }
        }

    }
}