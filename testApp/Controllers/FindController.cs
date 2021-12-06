using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApp.Models.DbData;
using testApp.Services;

namespace testApp.Controllers
{
    [Controller]
    public class FindController : Controller
    {
        IFileReader _fileService;

        public FindController(IFileReader fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AA(string city)
        {
            IntervalIp intervalIp = _fileService.FindLocation(city);
            return Json(intervalIp);
        }

        [HttpGet]
        [Route("ip/location")]
        public ActionResult FindCity(string ip)
        {
           List<GeoPosition> geoPosition = _fileService.FindIp(ip);
            return Json(geoPosition);
        }

        [HttpGet]
        [Route("city/locations")]
        public ActionResult FindIp(string city)
        {
            IntervalIp intervalIp = _fileService.FindLocation(city);
            return Json(intervalIp);
        }

       
    }
}
