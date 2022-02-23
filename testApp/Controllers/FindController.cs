using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using testApp.Models;

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

          //List<GeoPosition> geoPosition = _fileService.FindIp("1.0.0.1");
         // IntervalIp intervalIp = _fileService.FindLocation("cit_Olobi Abu U");



            return View();
        }

        [HttpPost]
        public ActionResult Index(string ip,string cyti)
        {
            List<GeoPosition> geoPosition;
            IntervalIp intervalIp;


            //if (!string.IsNullOrWhiteSpace(cyti))
            //    return Content(_fileService.FindIp(cyti));

            //if (!string.IsNullOrWhiteSpace(ip))
            //    intervalIp = _fileService.FindLocation(ip);
            return View();
        }

        //[HttpPost]
        //public ActionResult AA(string city)
        //{
        //    IntervalIp intervalIp = _fileService.FindLocation(city);
        //    return Json(intervalIp);
        //}

        [HttpGet]
        [Route("ip/location")]
        public ActionResult FindCity(string ip)
        {
           List<GeoPosition> geoPosition = _fileService.FindIp(ip);
            //return Json(geoPosition);
            ViewBag.GeoPosition = (geoPosition);
           return View("Index");
          //  return PartialView();
        }

        [HttpGet]
        [Route("city/locations")]
        public ActionResult FindIp(string city)
        {
            IntervalIp intervalIp = _fileService.FindLocation(city);
            // return Json(intervalIp);
            ViewBag.IntervalIp = (intervalIp);
            return View("Index");

           // return PartialView();
        }
    }
}
