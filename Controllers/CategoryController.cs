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
    public class CategoryController : Controller
    {
        private readonly CategoryRepo _categoryRepo;

        public CategoryController(IConfiguration configuration)
        {
            _categoryRepo = new CategoryRepo(configuration);
        }

        public IActionResult Index()
        {
            List<Category> category = (List<Category>)_categoryRepo.FindAll();
            return View(category);
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            List<Category> category = _categoryRepo.FindAll();
            return Json(new { data = category });
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
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(model);
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

            Category category = _categoryRepo.FindByID(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView(category);
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(model);
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
            _categoryRepo.Remove(id.Value);
            return RedirectToAction("Index");
        }

    }
}