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
    [Route("api/[controller]")]
    [ApiController]
    public class AreasApiController : ControllerBase
    {
        private readonly AreaRepo _areaRepo;

        public AreasApiController(IConfiguration configuration)
        {
            _areaRepo = new AreaRepo(configuration);
        }

        // GET: api/AreaApi
        [HttpGet]
        public ActionResult<List<Area>> Index()
        {
            var area = _areaRepo.FindAll();

            return area;
        }

        // GET: api/AreaApi/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Area> Index(int? id)
        {
            var area = _areaRepo.FindByID(id.Value);

            if(area == null)
            {
                return NotFound(area);                               // return StatusCode(404);
            }

            return area;
        }

        // POST: api/AreaApi
        [HttpPost]
        public IActionResult Create(Area model)
        {
            _areaRepo.Add(model);

            return StatusCode(201);                                 // return Created();
        }

        // PUT: api/AreaApi/5
        [HttpPut("{id}")]
        public IActionResult Edit(Area model, int? id)
        {
            Area area = _areaRepo.FindByID(id.Value);

            if (area == null)
            {
                return NotFound(area);                               // return StatusCode(404);
            }

            _areaRepo.ApiUpdate(model, id.Value);
            return StatusCode(200);                                 // return OK();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            //Area area = _areaRepo.FindByID(id.Value);

            if (id == 0)
            {
                return NotFound(id);
            }

            _areaRepo.Remove(id.Value);
            return StatusCode(200);
        }
    }
}