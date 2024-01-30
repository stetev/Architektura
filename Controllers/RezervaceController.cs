using Microsoft.AspNetCore.Mvc;
using RestauraceApp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;


namespace RestauraceApp.Controllers
{
	public class RezervaceController : Controller
	{

		private LiteDatabase _db = new LiteDatabase(@"Data.db");

		public IActionResult Index()
		{
			return View();

		}

		public ActionResult AllReservationen()
		{
			var rezervace = _db.GetCollection<Rezervace>("rezervace");
			var vsechnyRezervace = rezervace.FindAll().ToList();
			return View("AllReservationen", vsechnyRezervace);
		}



		//Vytvoření nové rezervace - GET
		public ActionResult FormCreate()
		{
			return View();
		}

		// Vytvoření nové rezervace - POST
		[HttpPost]
		public ActionResult FormCreate(Rezervace form)
		{
			if (ModelState.IsValid)
			{
				var rezervace = _db.GetCollection<Rezervace>("rezervace");
				// ID bude automaticky vygenerováno LiteDB
				rezervace.Insert(form);
				return RedirectToAction("Thekju");
			}
			return View(form);
		}



        //Editace rezervace - GET
        public ActionResult FormEdit(int id)
        {
            var rezervace = _db.GetCollection<Rezervace>("rezervace").FindById(id);
            if (rezervace == null)
            {
                // Rezervace nebyla nalezena
                return RedirectToAction("Error"); // nebo vhodná chybová stránka
            }
            return View(rezervace);
        }

        //Editace rezervace - POST
        [HttpPost]
        public ActionResult FormEdit(Rezervace form)
        {
            if (ModelState.IsValid)
            {
                var rezervace = _db.GetCollection<Rezervace>("rezervace");
                rezervace.Update(form);
                return RedirectToAction("AllReservationen");
            }
            return View(form);
        }


        //smazani rezervace -GET
        public ActionResult FormDelete(int id)
        {
            var rezervace = _db.GetCollection<Rezervace>("rezervace").FindById(id);
            if (rezervace == null)
            {
                return RedirectToAction("Error");
            }
            return View(rezervace);
        }



        //smazani rezervace -POST
        [HttpPost, ActionName("FormDelete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var rezervace = _db.GetCollection<Rezervace>("rezervace");
            var item = rezervace.FindById(id);
            if (item != null)
            {
                rezervace.Delete(id);
            }
            return RedirectToAction("AllReservationen");
        }


        public ActionResult Thekju()
		{
			return View();
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
