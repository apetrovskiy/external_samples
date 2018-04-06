using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using LightData.Auth.Helper;
using LightData.Auth.Settings;
using TidyManaged;

namespace LightData.Auth.Controllers
{
    [Authorize]
    public class SharedController : Controller
    {

        protected const int SearchResultValue = 20;
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName, string password, bool isPersistent = true)
        {
            var user = AuthSettings.OnGetUser(userName, password).FirstOrDefault();
            if (user == null)
                return View(0);
            FormsAuthentication.SetAuthCookie(user.UserName, isPersistent);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public void SetValue(string key, object value)
        {
            value.SessionSet(key);
        }

        [HttpPost]
        public ExternalActionResult GetValue(string key)
        {
            return key?.SessionGet()?.ToJsonResult();
        }

        [HttpPost]
        public ActionResult HtmlFormater(string html)
        {
            html = Server.UrlDecode(html);
            using (Document doc = Document.FromString(html))
            {
                doc.ShowWarnings = false;
                doc.Quiet = true;
                //doc.DocType = TidyManaged.DocTypeMode.Strict;
                //doc.DropFontTags = true;
                //doc.UseLogicalEmphasis = true;
                //doc.OutputXhtml = false;
                //doc.OutputXml = false;
                //doc.MakeClean = true;
                //doc.DropEmptyParagraphs = true;
                //doc.CleanWord2000 = true;
                //doc.QuoteAmpersands = true;
                doc.JoinStyles = true;
                doc.JoinClasses = true;
                doc.WrapAttributeValues = true;
                doc.Markup = true;
                doc.IndentSpaces = 10;
                //doc.IndentBlockElements = TidyManaged.AutoBool.Yes;
                doc.CharacterEncoding = TidyManaged.EncodingType.Utf8;
                //doc.WrapSections = false;
                //doc.WrapAttributeValues = false;
                //doc.WrapScriptLiterals = false;
                doc.WrapAt = 0;
                doc.OutputBodyOnly = AutoBool.Yes;
                doc.CleanAndRepair();
                return Content(doc.Save());

                //return new FileStreamResult(doc., "text/html");
                //parsed = doc.OutputHtml
            }
        }

        public ActionResult LoadFiles(Guid fileId)
        {
            var file = AuthSettings.GetFileById(fileId).First();
            var contentType = "text/css";
            if (file.FileType == CMS.Modules.Helper.EnumHelper.AllowedFiles.JAVASCRIPT)
                contentType = "text/javascript";
            Response.ContentType = contentType;
            return new FileStreamResult(new System.IO.MemoryStream(file.File), contentType);
        }
    }
}