using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApp.Services
{
    public class FileReadService
    {
        protected internal IFileReader FileReader  { get; }
        public FileReadService(IFileReader fileReader)
        {
            FileReader = fileReader;
        }
    }
}
