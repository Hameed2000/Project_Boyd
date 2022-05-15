using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBoyd.Services {
    public interface IFileUpload {
        Task Upload(IFileListEntry file);
    }
}
