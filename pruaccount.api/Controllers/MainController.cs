// <copyright file="MainController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Main Controller.
    /// </summary>
    [Route("/mainctrl")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger<MainController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">IWebHostEnvironment.</param>
        /// <param name="logger">logger.</param>
        public MainController(IWebHostEnvironment hostingEnvironment, ILogger<MainController> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
        }

        /// <summary>
        ///  Http Get.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            this.logger.LogInformation("Homecontroller -  {@EnvironmentName}", this.hostingEnvironment.EnvironmentName);
            this.logger.LogInformation("Homecontroller -  {@WebRootPath}", this.hostingEnvironment.WebRootPath);
            return this.Ok();
        }

        /// <summary>
        /// Http Post.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        public IActionResult Post()
        {
            return this.Ok();
        }
    }
}
