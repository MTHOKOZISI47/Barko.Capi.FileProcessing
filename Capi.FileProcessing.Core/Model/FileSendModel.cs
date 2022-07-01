using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Core.Model
{
   public  class FileSendModel
    {
        [JsonProperty]
        public string FileName { get; set; }
        [JsonProperty]
        public string Content { get; set; }
    }
}
