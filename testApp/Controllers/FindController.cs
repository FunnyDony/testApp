using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApp.Services;

namespace testApp.Controllers
{
    [Controller]
    public class FindController : Controller
    {
        IFileSevice _fileService;

        [HttpGet]
        [Route("ip/location")]
        public ActionResult FindCity(string ip)
        {
            _fileService.FindIp(ip);
            return Json(_fileService.FindIp(ip));
        }

        [HttpGet]
        [Route("city/locations")]
        public string FindIp(string city)
        {
            return $"Hello {city}";
        }
    }
}
