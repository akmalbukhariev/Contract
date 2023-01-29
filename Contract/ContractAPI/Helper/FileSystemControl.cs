using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Helper
{
    public class FileSystemControl
    {
        public async static void CreateFile(string savePath, IFormFile formFile)
        {
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            using (FileStream filestream = File.Create($"{savePath}{formFile.FileName}"))
            {
                await formFile.CopyToAsync(filestream);
                filestream.Flush();
            }
        }

        public async static void CreateFile(string savePath, string fileName, IFormFile formFile)
        { 
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            using (FileStream filestream = File.Create($"{savePath}{fileName}"))
            {
                try
                {
                    await formFile.CopyToAsync(filestream);
                    filestream.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } 
        }

        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
