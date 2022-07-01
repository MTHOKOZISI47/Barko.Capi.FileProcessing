using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Core.Collection.Application
{
   public interface IOutgoingServices
    {
        Task<List<string>> Outgoing();
    }
}
