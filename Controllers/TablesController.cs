using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Npgsql;
using CafeAPI.Models;
using CafeAPI.Repo;
using CafeAPI.Extension;

namespace CafeAPI.Controllers
{
    public class TablesController : Controller
    {
        private readonly TablesRepo _tablesRepo;

        public TablesController(IConfiguration configuration)
        {
            _tablesRepo = new TablesRepo(configuration);
        }

        public IActionResult Index()
        {            
            List<Tables> tables = (List<Tables>)_tablesRepo.FindAll();
            return View(tables);
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            List<Tables> tables = _tablesRepo.FindAll();
            return Json(new { data = tables });
        }

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

        [HttpPost]
        public IActionResult Create(Tables model)
        {
            if (ModelState.IsValid)
            {
                _tablesRepo.Add(model);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tables tables = _tablesRepo.FindByID(id.Value);
            if (tables == null)
            {
                return NotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(tables);
            }
            else
            {
                return View(tables);
            }
        }

        [HttpPost]
        public IActionResult Edit(Tables model)
        {
            if (ModelState.IsValid)
            {
                _tablesRepo.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            _tablesRepo.Remove(id.Value);
            return RedirectToAction("Index");
        }

    }
}