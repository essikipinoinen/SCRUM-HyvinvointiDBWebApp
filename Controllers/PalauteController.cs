﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hyvinvointisovellus;
using Hyvinvointisovellus.Models;

namespace Hyvinvointisovellus.Controllers
{
    public class PalauteController : Controller
    {
        private HyvinvointiDBEntities db = new HyvinvointiDBEntities();

        //public PalauteController(HyvinvointiDBEntities db)
        //{
        //    this.db = db;
        //}
        
        // GET: Palaute
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
                if (Session["Admin"] != null)
                {
                    var palaute = db.Palaute.Include(p => p.Kayttajat).Include(p => p.Kommentit);
                    return View(palaute.ToList());
                }
                else
                {
                    var kommentit = db.Palaute.Include(p => p.Kommentit);
                    var kayttajaId = (int)Session["UserId"];
                    var palaute = db.Palaute.Include(p => p.Kayttajat).
                    Where(x => x.KayttajaID == kayttajaId); //Vaan käyttäjän omat palauteet
                    return View(palaute.ToList());
                }
            }


            //tämä esiin kun kirjautuminen halutaan käyttöön ->

            //if (Session["Käyttäjätunnus"] == null)
            //{
            //    return RedirectToAction("Kirjautuminen", "Home");
            //}
            //else
            //{
            //    ViewBag.Loggedstatus = "Olet kirjautunut sisään ";
            //    return View(palaute.ToList());
            //}
        }

        // GET: Palaute/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palaute palaute = db.Palaute.Find(id);
            if (palaute == null)
            {
                return HttpNotFound();
            }
            return View(palaute);
        }

        // GET: Palaute/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            var kayttajaId = (int)Session["UserId"];

            var omatTiedot = db.Kayttajat.Include(k => k.Kirjautuminen).Include(k => k.Postitoimipaikat).
                Where(x => x.KayttajaID == kayttajaId);

            ViewBag.KayttajaID = new SelectList(omatTiedot, "KayttajaID", "Etunimi");
            return View();
            throw new NotImplementedException();
        }

        public ActionResult _ModalCreate()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            var kayttajaId = (int)Session["UserId"];

            var omatTiedot = db.Kayttajat.Include(k => k.Kirjautuminen).Include(k => k.Postitoimipaikat).
                Where(x => x.KayttajaID == kayttajaId);

            ViewBag.KayttajaID = new SelectList(omatTiedot, "KayttajaID", "Etunimi");
            return PartialView();
            throw new NotImplementedException();
        }

        // POST: Palaute/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PalauteID,KayttajaID,Palaute1")] Palaute palaute)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            if (ModelState.IsValid)
            {
                db.Palaute.Add(palaute);
                palaute.Pvm = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TyontekijaID = new SelectList(db.Kayttajat, "KayttajaID", "Etunimi", palaute.KayttajaID);
            return View(palaute);
        }

        // GET: Palaute/Edit/5


        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palaute palaute = db.Palaute.Find(id);
            if (palaute == null)
            {
                return HttpNotFound();
            }
            ViewBag.KayttajaID = new SelectList(db.Kayttajat, "KayttajaID", "Etunimi", palaute.KayttajaID);
            return View(palaute);
        }

        // POST: Palaute/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PalauteID,KayttajaID,Palaute1")] Palaute palaute)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            if (ModelState.IsValid)
            {
                db.Entry(palaute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TyontekijaID = new SelectList(db.Kayttajat, "KayttajaID", "Etunimi", palaute.KayttajaID);
            return View(palaute);
        }

        // GET: Palaute/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Palaute palaute = db.Palaute.Find(id);
            if (palaute == null)
            {
                return HttpNotFound();
            }
            return View(palaute);
        }

        // POST: Palaute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            Palaute palaute = db.Palaute.Find(id);
            db.Palaute.Remove(palaute);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // POST: Palaute/Kommentoi
        [HttpPost]
        public ActionResult Kommentoi(Kommentit obj)
        {
            Kommentit k = new Kommentit();
            k.KayttajaID = (int)Session["UserId"];
            k.Pvm = DateTime.Now;
            k.Kommentti = obj.Kommentti;
            k.PalauteID = obj.KommenttiID;
            db.Kommentit.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
