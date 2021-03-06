// <copyright file="TestController.cs" company="PrudentServices">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Pruaccount.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Pruaccount.Api.HttpClients.AuthValidationClient;

#pragma warning disable SA1402 // File may only contain a single type
    /// <summary>
    /// Sample Post.
    /// </summary>
#pragma warning disable SA1649 // File name should match first type name
    public class Post
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1402 // File may only contain a single type
    {
        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets a value indicating whether the post is Liked.
        /// </summary>
        public bool Like
        {
            get
            {
                if (this.Id % 4 == 0)
                {
                    return true;
                }

                return false;
            }
        }
    }

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
        [HttpGet("TestUserDetails/{userid}")]
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

        /// <summary>
        /// Test user details from jsonplaceholder.typicode.com.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("TestPosts")]
        public IActionResult TestPosts()
        {
            try
            {
                HttpClient http = new HttpClient();
                var data = http.GetAsync($"https://jsonplaceholder.typicode.com/posts").Result.Content.ReadAsStringAsync().Result;
                var postList = JsonConvert.DeserializeObject<List<Post>>(data);
                return this.Ok(postList);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Test user details from jsonplaceholder.typicode.com.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="searchTerm">searchTerm.</param>
        /// <param name="sort">sort.</param>
        /// <param name="orderBy">orderBy.</param>
        /// <param name="pageNumber">pageNumber.</param>
        /// <param name="rowsPerPage">rowsPerPage.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("TestServerPosts")]
        public IActionResult TestServerPosts(int userId, string searchTerm, string sort, string orderBy, int pageNumber, int rowsPerPage)
        {
            try
            {
                this.logger.LogInformation($"TestServerPosts Params - userId - {userId} searchTerm - {searchTerm} sort - {sort} orderBy - {orderBy} pageNumber - {pageNumber} rowsPerPage - {rowsPerPage}");

                HttpClient http = new HttpClient();
                var data = http.GetAsync($"https://jsonplaceholder.typicode.com/posts").Result.Content.ReadAsStringAsync().Result;

                var postList = JsonConvert.DeserializeObject<List<Post>>(data);

                if (userId > 0)
                {
                    postList = postList.Where(x => x.UserId == userId).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    postList = postList.Where(x => x.Title.Contains(searchTerm)).ToList();
                }

                if (sort.ToLower() == "id" && orderBy.ToLower() == "asc")
                {
                    postList = postList.OrderBy(x => x.Id).ToList();
                }
                else if (sort.ToLower() == "id" && orderBy.ToLower() == "desc")
                {
                    postList = postList.OrderByDescending(x => x.Id).ToList();
                }
                else if (sort.ToLower() == "userid" && orderBy.ToLower() == "asc")
                {
                    postList = postList.OrderBy(x => x.UserId).ToList();
                }
                else if (sort.ToLower() == "userid" && orderBy.ToLower() == "desc")
                {
                    postList = postList.OrderByDescending(x => x.UserId).ToList();
                }
                else if (sort.ToLower() == "title" && orderBy.ToLower() == "asc")
                {
                    postList = postList.OrderBy(x => x.Title).ToList();
                }
                else if (sort.ToLower() == "title" && orderBy.ToLower() == "desc")
                {
                    postList = postList.OrderByDescending(x => x.Title).ToList();
                }
                else if (sort.ToLower() == "body" && orderBy.ToLower() == "asc")
                {
                    postList = postList.OrderBy(x => x.Body).ToList();
                }
                else if (sort.ToLower() == "body" && orderBy.ToLower() == "desc")
                {
                    postList = postList.OrderByDescending(x => x.Body).ToList();
                }

                var pagedList = postList.Skip((pageNumber - 1) * rowsPerPage).Take(rowsPerPage).ToList();

                var result = new { Total = postList.Count, Posts = pagedList };

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
