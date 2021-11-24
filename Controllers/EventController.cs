using Hyvinvointisovellus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hyvinvointisovellus.Controllers
{
    public class EventController : Controller
    {

        //Tietokantayhteys esitellään kertaalleen Controllerin päätasolla? miksi
        private readonly HyvinvointiDBEntities1 db = new HyvinvointiDBEntities1();


        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                //var hymynaama = db.Events.Include(e => e.KayttajaID);
                //return View(hymynaama.ToList());

                return View();
            }

        }

        // Palauttaa tapahtumien datan kun jQueryllä toteutettu ajax pyyntö tulee näkymästä
        public JsonResult GetEvents()
        {
            var events = db.Event.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // Käytetään sekä uuden luontiin että olemassaolevan tapahtuman muokkaukseen
        [HttpPost]
        public JsonResult SaveEvent(Event ev)
        {
            bool status = false;

            if (ev.EventID > 0) // Jos ev.EventID tieto löytyy, kyse on olemassaolevasta kalenterimerkinnästä, jota siis muokataan uusilla arvoilla
            {
                //Muutettu existing --> existingEventInDB JSO 20.09.2021
                var existingEventInDB = db.Event.Where(ex => ex.EventID == ev.EventID).FirstOrDefault();
                if (existingEventInDB != null) // Jos id:tä vastaava rivi löytyy kannasta, päivitetään kyseisen eventin tiedot 
                {
                    existingEventInDB.Subject = ev.Subject;
                    existingEventInDB.Start = ev.Start;
                    existingEventInDB.End = ev.End;
                    existingEventInDB.Description = ev.Description;
                    existingEventInDB.IsFullDay = ev.IsFullDay;
                    existingEventInDB.ThemeColor = ev.ThemeColor;
                }
            }
            else //Jos taasen ev.EventID = 0 (nolla), on kyseessä uuden kalenterimerkinnän lisääminen
            {
                db.Event.Add(ev);
            }

            db.SaveChanges();
            status = true;

            return new JsonResult { Data = new { status = status } };

        }

        // Tapahtuman poisto
        [HttpPost]
        public JsonResult DeleteEvent(int id)
        {
            var status = false;

            {
                var ev = db.Event.Where(e => e.EventID == id).FirstOrDefault();
                if (ev != null)
                {
                    db.Event.Remove(ev);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}