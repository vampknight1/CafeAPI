using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeAPI.Extension;
using CafeAPI.Models;
using CafeAPI.Repo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace CafeAPI.Controllers
{
    public class EventCalendarController : Controller
    {
        private readonly EventCalendarRepo _eventCalendarRepo;       

        public EventCalendarController(IConfiguration configuration)
        {
            _eventCalendarRepo = new EventCalendarRepo(configuration);
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            List<EventCalendar> eventCalendar = _eventCalendarRepo.FindAll();
            return Json(eventCalendar);
        }

        // GET: EventCalendar
        public IActionResult Index()
        {
            List<EventCalendar> events = _eventCalendarRepo.FindAll();
            return View(events);
        }

        // GET: EventCalendar/Details/5
        public IActionResult Details(int? id)
        {
            return View();
        }

        // GET: EventCalendar/Create
        public ActionResult Create()
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

        // POST: EventCalendar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventCalendar model)
        {
            if (ModelState.IsValid)
            {
                try {
                    _eventCalendarRepo.Add(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex) {
                    throw ex;
                }
            }

            return View();
        }

        // GET: EventCalendar/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EventCalendar eventCalendar = _eventCalendarRepo.FindByID(id.Value);

            if (Request.IsAjaxRequest())
            {
                return PartialView(eventCalendar);
            }
            else
            {
                return View(eventCalendar);
            }
        }

        // POST: EventCalendar/Edit/5
        [HttpPost]
        public IActionResult Edit(EventCalendar model)
        {
            if (ModelState.IsValid)
            {
                _eventCalendarRepo.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // POST: EventCalendar/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == 0) {
                return NotFound();
            }

            try {
                _eventCalendarRepo.Remove(id.Value);
                return RedirectToAction("Index");
            }
            catch(Exception ex) {
                throw ex;
            }
        }
    }
}