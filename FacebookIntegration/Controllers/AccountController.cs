using System;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using FacebookIntegration.Models;
using System.Text;

namespace FacebookIntegration.Controllers
{
    public class AccountController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            AccountHandler._client.AccessToken = null;
            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            try
            {
                var accountHandler = new AccountHandler();

                var loginUrl = accountHandler.GetLoginUrl(RedirectUri.AbsoluteUri);

                return Redirect(loginUrl.AbsoluteUri);
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        public ActionResult FacebookCallback(string code)
        {
            try
            {
                var accountHandler = new AccountHandler();

                accountHandler.GetAccessToken(RedirectUri.AbsoluteUri, code);

                return RedirectToAction("UserInfo", "User");
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
