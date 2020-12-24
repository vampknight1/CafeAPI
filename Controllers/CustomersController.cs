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
    public class CustomersController : Controller
    {
        private readonly CustomersRepo _customerRepo;
        private readonly AreaRepo _areaRepo;

        public CustomersController(IConfiguration configuration)
        {
            _customerRepo = new CustomersRepo(configuration);
            _areaRepo = new AreaRepo(configuration);
        }

        public IActionResult Index()
        {
            List<Customers> customers = (List<Customers>)_customerRepo.FindAll();
            return View(customers);
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            List<Customers> customers = _customerRepo.GetJoinWith();
            return Json(new { data = customers });
        }

        public IActionResult Details(int? id)
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.City = new SelectList(_areaRepo.FindAll(), "ID", "Name");    //// ID Name --> Class Model NOT from DB Field

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
        public IActionResult Create(Customers model)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.Add(model);
                return RedirectToAction("Index");
            }

            ViewBag.City = new SelectList(_areaRepo.FindAll(), "ID", "Name");    //// ID Name --> Class Model NOT from DB Field

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customers customer = _customerRepo.FindByID(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.City = new SelectList(_areaRepo.FindAll(), "ID", "Name", customer.City);    // ID Name --> Class Model NOT from DB Field

            //return View(customer);

            if (Request.IsAjaxRequest())
            {
                return PartialView(customer);
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        public IActionResult Edit(Customers model)
        {
            if (ModelState.IsValid)
            {
                _customerRepo.Update(model);
                return RedirectToAction("Index");
            }

            ViewBag.City = new SelectList(_areaRepo.FindAll(), "ID", "Name", model.City);    // ID Name --> Class Model NOT from DB Field

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            _customerRepo.Remove(id.Value);
            return RedirectToAction("Index");
        }

    }
}