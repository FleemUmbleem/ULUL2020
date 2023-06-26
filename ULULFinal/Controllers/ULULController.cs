using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using ULULFinal.Models;

namespace ULULFinal.Controllers
{
    public class ULULController : Controller
    {
        // GET: ULUL
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(Clip model)
        {
            // Save url to database
            if (ModelState.IsValid)
            {
                string a = "https://clips.twitch.tv/embed?clip=";
                string b = model.Code; // 4/5/ words from url
                string c = "&parent=localhost&autoplay=false&muted=false";

                string src = a + b + c;

                var table = new TwitchEntitiesEntities();

                table.Clips.Add(new Clip
                {
                    Code = src
                });
                table.SaveChanges();
                ViewData["Message"] = "Video Submitted";
            }
            else
            {
                ViewData["Message"] = "ModelState.IsValid Error";
            }

            // Clear the textbox



            return View(model);
        }

        [HttpGet]
        public ActionResult Watch()
        {
            // get src
            if (ModelState.IsValid)
            {
                var entities = new TwitchEntitiesEntities();

                int count = entities.Clips.Count();

                // apply to iframe
                return View(entities.Clips.ToList());
            }
            else
            {
                ViewBag["Message"] = "error with modelstate";
                return RedirectToAction("Submit", "ULUL");
            }

        }
    }
}