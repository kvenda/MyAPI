using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MyAPI.Models;
using System.Net;
using System.Web.Mvc;
using MyAPI.Services;


namespace MyAPI.Controllers
{
    [ApiController]

    // The exact wording of this file / class before the word Controller (in this case WebAPIController defines the name of the endpoint: WebAPI)

    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class WebAPIController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
      
        private readonly ILogger<WebAPIController> _logger;
        private readonly APIService _apiService;

        public WebAPIController(APIService apiService, ILogger<WebAPIController> logger)
        {
            _logger = logger;
            _apiService = apiService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<List<DataFields>> Get()
        {
            _logger.LogInformation("WebAPIController - In HttpGET Get to get all records");
            var foundData =  _apiService.Get();
            return foundData; // calling program tests ActionResult for null value or data
        }
        [Microsoft.AspNetCore.Mvc.Route("id/{id?}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<DataFields> Get(string id)
        {
            _logger.LogInformation("WebAPIController - In HttpGET Get with overload of string id");
             var foundData = _apiService.Get(id);
            return foundData;  // calling program tests ActionResult for null value or data
        }

        [Microsoft.AspNetCore.Mvc.Route("field/{fullname?}")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<DataFields> GetField(string fullname)
        {
            _logger.LogInformation("WebAPIController - In HttpGET Get with overload of string fullname");
            var foundData = _apiService.GetField(fullname);
            return foundData;  // calling program tests ActionResult for null value or data
        }

        [Microsoft.AspNetCore.Mvc.Route("[controller]")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public System.Web.Mvc.ActionResult JsonAction([FromBody] DataFields jsonData)
        {
            _logger.LogInformation("WebAPIController - In HttpPost with overload of FromBody DataFields");
            DataFields newData = new()
            {
                LastModifiedDate = DateTime.UtcNow,
                FullName = jsonData.FullName,
                MyText = jsonData.MyText,
                Summary = jsonData.Summary
            };  //calling program will convert UTC to local time as needed
            try
            {
                _apiService.Create(newData);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]

        // this is the update method
        public System.Web.Mvc.ActionResult JsonActionPut([FromBody] DataFields jsonData)
        {
            _logger.LogInformation("WebAPIController - In HttpPut with overload of Guid id and FromBody DataFields");
            DataFields newData = new()
            {
                LastModifiedDate = DateTime.UtcNow,
                FullName = jsonData.FullName,
                MyText = jsonData.MyText,
                Summary = jsonData.Summary
            };  //calling program will convert UTC to local time as needed
            try
            {
                _apiService.Update(jsonData.Id, newData);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]  
        // url syntax is ...WebAPI/080808  no quotes, question marks or equal signs
        public System.Web.Mvc.ActionResult Delete(string id)
        {
            _logger.LogInformation("WebAPIController - In HttpDelete with overload of Guid id ");
            try
            {
                _apiService.Remove(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

    }
}
