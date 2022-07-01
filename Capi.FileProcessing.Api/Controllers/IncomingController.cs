using Capi.FileProcessing.Core.Collection.Application;
using Capi.FileProcessing.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capi.FileProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomingController : ControllerBase
    {
        private readonly IIncomingServices _services;
        public IncomingController(IIncomingServices fileSendServices)
        {
            _services = fileSendServices;
        }

        [HttpPost]   
        [Route("Incoming")]
        public IActionResult Incoming(FileSendModel model)
        {
            try
            {
               _services.Incoming(model);
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
          
        }

    }
}
