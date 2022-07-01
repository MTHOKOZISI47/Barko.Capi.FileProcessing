using Capi.FileProcessing.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Core.Collection.Application
{
   public interface IIncomingServices
    {
      void Incoming(FileSendModel model);
        Task<string> GetAccessToken();
    }
}
