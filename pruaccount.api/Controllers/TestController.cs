using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pruaccount.api.HttpClients.AuthValidationClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruaccount.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IValidateUserTokenClient _validateUserTokenClient;
        private readonly ILogger<TestController> _logger;

        public TestController(IValidateUserTokenClient validateUserTokenClient, ILogger<TestController> logger)
        {
            _validateUserTokenClient = validateUserTokenClient;
            _logger = logger;
        }

        [HttpPost("TestAccess")]
        public IActionResult TestAccess()
        {
            try
            {
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
