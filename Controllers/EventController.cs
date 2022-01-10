using Hyvinvointisovellus.Models;
using Hyvinvointisovellus.ViewModel;
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
        private readonly HyvinvointiDBEntities db = new HyvinvointiDBEntities();


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
                return View();
            }

        }

        // Palauttaa tapahtumien datan kun jQueryllä toteutettu ajax pyyntö tulee näkymästä
        public JsonResult GetEvents()
        {
            List<FiilisModel1> fiilikset = new List<FiilisModel1>();

            var events = db.Event.ToList();
            foreach (var i in events)
            {
                if (Session["UserName"].ToString() == "Tiina")
                {
                    FiilisModel1 f = new FiilisModel1();
                    f.EventID = i.EventID; //lisätty
                    f.Subject = i.Subject;
                    f.Description = i.Description;
                    f.Start = i.Start;
                    f.End = i.End;
                    f.ThemeColor = i.ThemeColor;
                    f.IsFullDay = i.IsFullDay;
                    f.KayttajaID = i.KayttajaID;
                    f.HymynaamaID = i.HymynaamaID;
                    fiilikset.Add(f);
                }
                if (i.KayttajaID == Convert.ToInt32(Session["UserId"]))
                {
                    FiilisModel1 f = new FiilisModel1();
                    f.EventID = i.EventID;
                    f.Subject = i.Subject;
                    f.Description = i.Description;
                    f.Start = i.Start;
                    f.End = i.End;
                    f.ThemeColor = i.ThemeColor;
                    f.IsFullDay = i.IsFullDay;
                    f.KayttajaID = i.KayttajaID;
                    f.HymynaamaID = i.HymynaamaID;
                    fiilikset.Add(f);
                }

            }
            return new JsonResult { Data = fiilikset, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
                    existingEventInDB.Subject = "";
                    existingEventInDB.Start = ev.Start;
                    existingEventInDB.End = ev.End;
                    existingEventInDB.Description = "";
                    existingEventInDB.IsFullDay = ev.IsFullDay;
                    existingEventInDB.ThemeColor = ev.ThemeColor;
                    existingEventInDB.KayttajaID = Convert.ToInt32(Session["UserId"]);
                    existingEventInDB.HymynaamaID = ev.HymynaamaID;
                }
            }
            else //Jos taasen ev.EventID = 0 (nolla), on kyseessä uuden kalenterimerkinnän lisääminen
            {

                ev.KayttajaID = Convert.ToInt32(Session["UserId"]);
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