using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CafeAPI.Models;
using CafeAPI.Repo;
using CafeAPI.Extension;

namespace CafeAPI.Controllers
{
    public class AreasController : Controller
    {
        private readonly AreaRepo _areaRepo;

        public AreasController(IConfiguration configuration)
        {
            _areaRepo = new AreaRepo(configuration);
        }

        // GET: Area
        public IActionResult Index()
        {
            var result = _areaRepo.FindAll();
            return View(result);
        }

        // GET: Area/Details/5
        public IActionResult Details(int? id)
        {
            return View();
        }

        // GET: Area/Create
        public IActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            else
            {
                return View();
            }
        }

        // POST: Area/Create
        [HttpPost]
        public IActionResult Create(Area model)
        {
            if (ModelState.IsValid)
            {
                _areaRepo.Add(model);
                return RedirectToAction("Index");
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
        }

        // GET: /Area/Edit/1
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Area area = _areaRepo.FindByID(id.Value);

            if (Request.IsAjaxRequest())
            {
                return PartialView(area);
            }
            else
            {
                return View(area);
            }
        }

        // POST: /Area/Edit   
        [HttpPost]
        public IActionResult Edit(Area model)
        {
            if (ModelState.IsValid)
            {
                _areaRepo.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET:/Area/Delete/1
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _areaRepo.Remove(id.Value);
            return RedirectToAction("Index");
        }

        //var sUnit = _dappConn.Query<MasterUnit>(@"SELECT UnitID, UnitName FROM MasterUnit");
        //ViewBag.UnitID = new SelectList(sUnit.AsQueryable(), "UnitID", "UnitName");

        //var sArea = IDbCommand
    }
}