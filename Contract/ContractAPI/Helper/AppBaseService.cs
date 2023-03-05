using ContractAPI.DataAccess;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Helper
{
    public abstract class AppBaseService
    {
        protected ContractMakerContext dataBase { get; set; }

        protected AppBaseService(ContractMakerContext db)
        {
            dataBase = db;
        }

        protected AppBaseService()
        {
            
        }

        protected async Task CreateFile(string savePath, IFormFile formFile)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            if (formFile.Length <= 0) return;
             
            using (var filestream = File.Create($"{savePath}{formFile.FileName}"))
            {
                //try
                {
                    //Console.WriteLine($"FileSystemControl Start 1: ");
                    //Console.WriteLine($"formFile.Length: {formFile.Length}");
                    await formFile.CopyToAsync(filestream);
                    //Console.WriteLine($"FileSystemControl End 1: ");
                    //filestream.Flush();
                }
                //catch (Exception ex)
                {
                    //Console.WriteLine($"FileSystemControl: {ex.Message}");
                }
            }
        }

        protected async Task CreateFile(string savePath, string fileName, IFormFile formFile)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            if (formFile.Length <= 0) return;

            using (var filestream = File.Create($"{savePath}{fileName}"))
            {
                //try
                {
                    //Console.WriteLine($"FileSystemControl Start 2: ");
                    //Console.WriteLine($"formFile.Length: {formFile.Length}");
                    await formFile.CopyToAsync(filestream);
                    //Console.WriteLine($"FileSystemControl End 2: ");
                    //filestream.Flush();
                }
                //catch (Exception ex)
                {
                    //Console.WriteLine($"FileSystemControl: {ex.Message}");
                }
            }
        }

        protected void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
