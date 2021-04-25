using Dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Controllers
{
    public class ShortLinkController : Controller
    {
        // GET: ShortLink
        public ActionResult Index(string code)
        {
            var url = Services.ShortUrlService.GetLongUrl(code);
            if (!string.IsNullOrEmpty(url)) {
                Response.Redirect(url);
            }
            return View();
        }
    }
}