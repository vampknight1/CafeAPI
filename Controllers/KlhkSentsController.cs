using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CafeAPI.Models;
using CafeAPI.Repo;

namespace CafeAPI.Controllers
{
    public class KlhkSentsController : Controller
    {
        private readonly KlhkSentRepo _klhkSentRepo; 

        public KlhkSentsController(IConfiguration configuration)
        {
            _klhkSentRepo = new KlhkSentRepo(configuration);
        }

        // GET: KlhkSents
        public ActionResult Index()
        {
            List<KlhkSents> klhkSents = (List<KlhkSents>)_klhkSentRepo.FindAll();

            return View(klhkSents);
        }

        // GET: KlhkSents/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: KlhkSents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KlhkSents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KlhkSents/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            KlhkSents objKlhkSents = _klhkSentRepo.FindByID(id.Value);
            if(objKlhkSents == null)
            {
                return NotFound();
            }

            return View(objKlhkSents);
        }

        // POST: KlhkSents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KlhkSents/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: KlhkSents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}