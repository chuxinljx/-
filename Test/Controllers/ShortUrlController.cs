using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Services;

namespace Test.Controllers
{
    public class ShortUrlController : Controller
    {
        static string link = "http://2794754rx4.zicp.vip/";
        // GET: ShortUrl
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetShortJson(string oldUrl) {
            AjaxRespone ar = new AjaxRespone();
          
            try
            {
                if (string.IsNullOrEmpty(oldUrl))
                {
                    throw new Exception("请输入原始链接");
                }
                var url = ShortUrlService.DataBaseShortUrl(oldUrl);
                if (string.IsNullOrEmpty(url)) {
                    throw new Exception("短链接转换为失败");
                }
                ar.ErrorCode = 0;
                ar.Result = link+url;

            }
            catch (Exception ex)
            {

                ar.ErrorCode = 1;
                ar.ErrorDesc = ex.Message;
            }
            return Json(ar);
        }

        public JsonResult GetLongJson(string newUrl)
        {
            AjaxRespone ar = new AjaxRespone();
           
            try
            {
                if (string.IsNullOrEmpty(newUrl))
                {
                    throw new Exception("请输入短链接");
                }
                var url = ShortUrlService.GetLongUrl(newUrl);
                ar.ErrorCode = 0;
                ar.Result = url;

            }
            catch (Exception ex)
            {

                ar.ErrorCode = 1;
                ar.ErrorDesc = ex.Message;
            }
            return Json(ar);
        }
        public class AjaxRespone {
            public string ErrorDesc { get; set; }
            public int ErrorCode { get; set; }
            public string Result { get; set; }
        }
    }
}