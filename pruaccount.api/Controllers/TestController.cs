// <copyright file="TestController.cs" company="PrudentServices">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Pruaccount.Api.HttpClients.AuthValidationClient;

    /// <summary>
    /// Test Controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IValidateUserTokenClient validateUserTokenClient;
        private readonly ILogger<TestController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestController"/> class.
        /// </summary>
        /// <param name="validateUserTokenClient">validateUserTokenClient.</param>
        /// <param name="logger">logger.</param>
        public TestController(IValidateUserTokenClient validateUserTokenClient, ILogger<TestController> logger)
        {
            this.validateUserTokenClient = validateUserTokenClient;
            this.logger = logger;
        }

        /// <summary>
        /// Test Token Access.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost("TestAccess")]
        public IActionResult TestAccess()
        {
            try
            {
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Test user details from jsonplaceholder.typicode.com.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("TestUserDetails/{userid}")]
        public IActionResult TestUserDetails(int userId)
        {
            try
            {
                HttpClient http = new HttpClient();
                var data = http.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}").Result.Content.ReadAsStringAsync().Result;
                return this.Ok(data);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
