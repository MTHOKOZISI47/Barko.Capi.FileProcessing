using Capi.FileProcessing.Core.Collection.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Application
{
   public static class Bootstrapper
    {
        public static void AddApplicationLayer(this IServiceCollection services, string folder)
        {
            Folder = folder;
            services.AddSingleton<IIncomingServices, IncomingServices>();
        }
        public static string Folder { get; private set; }
    }
}
