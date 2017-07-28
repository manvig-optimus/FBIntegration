using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using FacebookIntegration.Models;

namespace FacebookIntegration.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult UserInfo()
        {
            try
            {
                var userInfo = new UserInfo();

                var user = userInfo.GetUserInfo();

                FormsAuthentication.SetAuthCookie(user.Email, false);

                return View(user);
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        [HttpPost]
        public ActionResult UserInfo(FormCollection data)
        {
            try
            {
                string selectedOption = Convert.ToString(data["ViewMoreInfo"]);

                switch (selectedOption)
                {
                    case "AlbumDetails":
                        return RedirectToAction("Albums", "User");
                    case "Events":
                        return RedirectToAction("Events", "User");
                    case "Videos":
                        return RedirectToAction("Videos", "User");
                    case "Places":
                        return RedirectToAction("Places", "User");
                    case "default":
                        break;
                }

                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        [HttpGet]
        public ActionResult Albums()
        {
            try
            {
                var userAlbum = new UserAlbum();

                var albums = userAlbum.GetAlbumDetails();

                return View(albums);
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        [HttpGet]
        public ActionResult Events()
        {
            try
            {
                var userEvent = new UserEvent();

                var events = userEvent.GetUserEvents();

                return View(events);
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        [HttpGet]
        public ActionResult Videos()
        {
            try
            {
                var uploadedVideo = new UploadedVideo();

                var videos = uploadedVideo.GetUploadedVideos();

                return View(videos);
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        [HttpGet]
        public ActionResult Places()
        {
            try
            {
                var userTaggedPlace = new UserTaggedPlace();

                var places = userTaggedPlace.GetTaggedPlaces();

                return View(places);
            }
            catch
            {
                return RedirectToAction("Error", "Account");
            }
        }

        /// <summary>
        /// Exports data into Json format.
        /// </summary>
        /// <returns>Return path of exported file.</returns>
        public string ExportData()
        {
            var userInfo = new UserInfo();
            return userInfo.ExportData();
        }
    }
}
