using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.Controllers
{
    [Route("/mainctrl")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<MainController> _logger;

        public MainController(IWebHostEnvironment hostingEnvironment, ILogger<MainController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Homecontroller -  {@EnvironmentName}", _hostingEnvironment.EnvironmentName);
            _logger.LogInformation("Homecontroller -  {@WebRootPath}", _hostingEnvironment.WebRootPath);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
