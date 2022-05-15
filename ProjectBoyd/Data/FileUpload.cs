using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using ProjectBoyd.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Data {
    public class FileUpload : IFileUpload {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment) {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Upload(IFileListEntry file) {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "/Assets/Images/cds_imgs/", file.Name);
            var memoryStream = new MemoryStream();
            await file.Data.CopyToAsync(memoryStream);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                memoryStream.WriteTo(fileStream);
            }
        }

    }
}
