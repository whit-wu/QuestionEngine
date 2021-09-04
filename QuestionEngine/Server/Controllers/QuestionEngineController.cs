using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionEngine.Server.Controllers
{
    [Route("controller")]
    [ApiController]
    public class QuestionEngineController : ControllerBase
    {
        [HttpGet]
        string Get()
        {
            return "Hello there";
        }
    }
}
