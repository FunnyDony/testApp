using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        [Route("ip/location")]
        public ActionResult FindCity(string ip)
        {
            try
            {
                List<GeoPosition> geoPosition = _fileService.FindIp(ip);
                ViewBag.GeoPosition = (geoPosition); // Данные для отображеня в представлении на сервере
                return Json(geoPosition);
            }
            catch (System.Exception ex)
            {
                return Json(ex.Message);
            }
          
        }

        [HttpGet]
        [Route("city/locations")]
        public ActionResult FindIp(string city)
        {
            try
            {
                IntervalIp intervalIp = _fileService.FindLocation(city);
                ViewBag.IntervalIp = (intervalIp); // Данные для отображеня в представлении на сервере
                return Json(intervalIp);
            }
            catch (System.Exception ex)
            {
                return Json(ex.Message);
            }
           
        }
    }
}
