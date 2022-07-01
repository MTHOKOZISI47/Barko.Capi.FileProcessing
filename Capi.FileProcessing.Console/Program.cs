using Capi.FileProcessing.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;

namespace Capi.FileProcessing.Console
{
    public class Program
    {
        
        public Program()
        {
      
        }
        static void Main(string[] args)
        {          
            var app = new FileSendServices();
            var items =  app.GetAndConvertFile();
            //try
            //{
            //    Timer t = new Timer(ElapsedTime, null, 0, 10000);
            //}catch(Exception e)
            //{
            //}           
        }

        static async void ElapsedTime(Object o)
        {         
            System.Console.WriteLine($"Time Elapsed {DateTime.Now}");
            try
            {
                IConfiguration config=null;
                var app = new FileSendServices(config);
                var items = await app.GetAndConvertFile();

                foreach (var i in items)
                {                 
                    System.Console.WriteLine(i);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
          
        }
    }
}
