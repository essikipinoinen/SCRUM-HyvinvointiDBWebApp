using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Hyvinvointisovellus.Models;
using System.Net;

namespace Hyvinvointisovellus.Controllers
{
    public class HomeController : Controller
    {
        //
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return View("Kirjautuminen");
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            return View();
            throw new NotImplementedException();
        }
        public ActionResult IndexTyonantaja()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return View("Kirjautuminen");
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            return View();
            throw new NotImplementedException();
        }
        public ActionResult IndexTyontekija()
        {

            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return View("Kirjautuminen");
            }
            else ViewBag.LoggedStatus = "Kirjautunut";

            return View();
            throw new NotImplementedException();

        }

        public ActionResult About()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Kirjautuminen()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            return View();
        }

        private HyvinvointiDBEntities db = new HyvinvointiDBEntities();


        public ActionResult OmattiedotTyontekija()
        {
            var kayttajaId = (int)Session["UserId"];

            var omatTiedot = db.Kayttajat.Include(k => k.Kirjautuminen).Include(k => k.Postitoimipaikat).
                Where(x => x.KayttajaID == kayttajaId);
            return View(omatTiedot.ToList());
            throw new NotImplementedException();
        }


        public ActionResult OmattiedotTyonantaja()
        {
            var kayttajaId = (int)Session["UserId"];

            var omatTiedot = db.Kayttajat.Include(k => k.Kirjautuminen).Include(k => k.Postitoimipaikat).
                Where(x => x.KayttajaID == kayttajaId);
            return View(omatTiedot.ToList());
            throw new NotImplementedException();
        }

        //OMIEN TIETOJEN MUOKKAUS
        // GET: Tyontekijat/Edit/5
        public ActionResult Muokkaa(int? id)
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

            Kayttajat omattiedot = db.Kayttajat.Find(id);
            if (omattiedot == null)
            {
                return HttpNotFound();
            }
            return View(omattiedot);
        }
        // POST: Tyontekijat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Muokkaa([Bind(Include = "KayttajaID,Etunimi,Sukunimi,Osoite,Postinumero,Postitoimipaikka, Kayttajatunnus, Salasana")] Kayttajat omattiedot)
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            if (ModelState.IsValid)
            {
                db.Entry(omattiedot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OmattiedotTyontekija");
            }
            return View(omattiedot);
        }


        [HttpPost]
        public ActionResult Authorize(Kirjautuminen LoginModel)
        {
            HyvinvointiDBEntities db = new HyvinvointiDBEntities();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Kirjautuminen.SingleOrDefault(x => x.Kayttajatunnus == LoginModel.Kayttajatunnus && x.Salasana == LoginModel.Salasana);



            if (LoggedUser != null)
            {
                //ViewBag.LoginMessage = "Kirjautuminen onnistui!";
                ViewBag.LoggedStatus = "Kirjautunut";
                Session["UserName"] = LoggedUser.Kayttajatunnus;
                Session["UserId"] = LoggedUser.KayttajaID;
                Session["PassWord"] = LoggedUser.Salasana;

                if (LoggedUser.Kayttajatunnus == "Tiina")
                {
                    Session["Admin"] = LoggedUser.Kayttajatunnus;
                    return RedirectToAction("IndexTyonantaja", "Home");
                }
                else
                {
                    return RedirectToAction("IndexTyontekija", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
                }
            }
            else
            {
                ViewBag.LoginMessage = "Kirjautuminen epäonnistui!";
                ViewBag.LoggedStatus = "Ei kirjautunut";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana. Yritä uudelleen!";
                return View("Kirjautuminen", LoginModel);
            }
        }

        public ActionResult Rekisterointi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TallennaRekisterointi(Kayttajat rekisterointiTiedot)
        {
            //We check if the model state is valid or not. We have used DataAnnotation attributes.
            //If any form value fails the DataAnnotation validation the model state becomes invalid.
            if (ModelState.IsValid)
            {
                //create database context using Entity framework 
                using (var hyvinvointidb = new HyvinvointiDBEntities())
                {
                    //If the model state is valid i.e. the form values passed the validation then we are storing the User's details in DB.
                    Kayttajat rekkay = new Kayttajat();

                    //Save all details in RegitserUser object

                    rekkay.Etunimi = rekisterointiTiedot.Etunimi;
                    rekkay.Sukunimi = rekisterointiTiedot.Sukunimi;
                    rekkay.Osoite = rekisterointiTiedot.Osoite;
                    rekkay.Postinumero = rekisterointiTiedot.Postinumero;
                    rekkay.Kayttajatunnus = rekisterointiTiedot.Kayttajatunnus;
                    rekkay.Salasana = rekisterointiTiedot.Salasana;


                    //Calling the SaveDetails method which saves the details.
                    hyvinvointidb.Kayttajat.Add(rekkay);
                    hyvinvointidb.SaveChanges();
                }

                ViewBag.Message = "Rekisteröinti onnistui!";
                return View("Rekisterointi");
            }
            else
            {
                //If the validation fails, we are returning the model object with errors to the view, which will display the error messages.
                ViewBag.Message = "Rekisteröinti epäonnistui!";
                return View("Rekisterointi", rekisterointiTiedot);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Ei kirjautunut";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }



    }



}