﻿using Hyvinvointisovellus.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Hyvinvointisovellus.Controllers
{
    public class TyontekijatController : Controller
    {
        private HyvinvointiDBEntities db = new HyvinvointiDBEntities();

        // GET: Tyontekijat
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
                return View(db.Kayttajat.ToList());
            }

            //tämä esiin kun kirjautuminen halutaan käyttöön ->

            //if (Session["Käyttäjätunnus"] == null)
            //{
            //    return RedirectToAction("Kirjautuminen", "Home");
            //}
            //else
            //{
            //    ViewBag.Loggedstatus = "Olet kirjautunut sisään ";
            //    return View(db.Tyontekijat.ToList());
            //}
        }

        // GET: Tyontekijat/Details/5
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
            Kayttajat tyontekijat = db.Kayttajat.Find(id);
            if (tyontekijat == null)
            {
                return HttpNotFound();
            }
            return View(tyontekijat);
        }

        public ActionResult _ModalDetails(int? id)
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
            Kayttajat tyontekijat = db.Kayttajat.Find(id);
            if (tyontekijat == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDetails", tyontekijat);
        }

        // GET: Tyontekijat/Create
        public ActionResult Create()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            return View();
        }

        public ActionResult _ModalCreate()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            return PartialView();
        }

        // POST: Tyontekijat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Etunimi,Sukunimi,Osoite,Postinumero,Kayttajatunnus,Salasana")] Kayttajat tyontekijat)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            Postitoimipaikat p = db.Postitoimipaikat.Find(tyontekijat.Postinumero);
            tyontekijat.Postitoimipaikat = p;
            if (ModelState.IsValid)
            {
                db.Kayttajat.Add(tyontekijat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //return View(tyontekijat);
            return RedirectToAction("Index", "Tyontekijat");
        }

        // GET: Tyontekijat/Edit/5
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
            Kayttajat tyontekijat = db.Kayttajat.Find(id);
            if (tyontekijat == null)
            {
                return HttpNotFound();
            }
            return View(tyontekijat);
        }

        public ActionResult _ModalEdit(int? id)
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
            Kayttajat tyontekijat = db.Kayttajat.Find(id);
            if (tyontekijat == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalEdit", tyontekijat);
        }

        // POST: Tyontekijat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KayttajaID,Etunimi,Sukunimi,Osoite,Postinumero,Kayttajatunnus,Salasana")] Kayttajat tyontekijat)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            Postitoimipaikat p = db.Postitoimipaikat.Find(tyontekijat.Postinumero);
            tyontekijat.Postitoimipaikat = p;
            if (ModelState.IsValid)
            {
                db.Entry(tyontekijat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //return View(tyontekijat);
            return RedirectToAction("Index", "Tyontekijat");
        }

        // GET: Tyontekijat/Delete/5
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
               Kayttajat tyontekijat = db.Kayttajat.Find(id);
            if (tyontekijat == null)
            {
                return HttpNotFound();
            }
            return View(tyontekijat);
        }

        // POST: Tyontekijat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            Kayttajat tyontekijat = db.Kayttajat.Find(id);
            db.Kayttajat.Remove(tyontekijat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult _ModalDelete(int? id)
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
            Kayttajat tyontekijat = db.Kayttajat.Find(id);
            if (tyontekijat == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDelete", tyontekijat);
        }

        [HttpPost, ActionName("_ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalDeleteConfirmed(int id)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            Kayttajat tyontekijat = db.Kayttajat.Find(id);
            db.Kayttajat.Remove(tyontekijat);
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
