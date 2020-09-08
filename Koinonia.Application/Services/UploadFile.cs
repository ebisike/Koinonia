using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Koinonia.Application.Services
{
    public static class UploadFile
    {
        public static string FileName { get; set; }
        public static string FilePath { get; set; }
        public static string uniqueFileName { get; set; }
        public static string Upload(IFormFile file, string folder)
        {
            FileName = Guid.NewGuid() + "_" + file.FileName;
            FilePath = Path.Combine(folder, FileName);
            file.CopyTo(new FileStream(FilePath, FileMode.Create));
            return FileName;
        }
        public static bool DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            return false;
        }
    }
}
