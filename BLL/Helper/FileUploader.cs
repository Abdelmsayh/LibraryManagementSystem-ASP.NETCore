using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helper
{
    public static class FileUploader
    {
        public static async Task<string> UploadAsync(IFormFile file , string folder)
        {
            // 1) Get Directory
            string directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder);


            // 2) Get File Name
            string fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);


            // 3) Final File Path
            string path = Path.Combine(directory, fileName);


            // 4) Save file into final path
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
