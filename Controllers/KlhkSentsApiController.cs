using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using CafeAPI.Models;
using CafeAPI.Repo;

namespace CafeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator, Regular")]
    public class KlhkSentsApiController : ControllerBase
    {
        private readonly KlhkSentRepo _klhkSentRepo;

        public KlhkSentsApiController(IConfiguration configuration)
        {
            _klhkSentRepo = new KlhkSentRepo(configuration);
        }


        [HttpGet]
        public ActionResult<List<KlhkSents>> Index()
        {
            var klhkSents = _klhkSentRepo.FindAll();
            return klhkSents;
        }

        [HttpGet("{id}")]
        public ActionResult<KlhkSents> Index(int? id)
        {
            var klhkSents = _klhkSentRepo.FindByID(id.Value);

            if (klhkSents == null)
            {
                return NotFound();
            }

            return klhkSents;
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(KlhkSents model)
        {
            _klhkSentRepo.Add(model);
            //return Created("GetKlhkSents", new { id = model.ID.ToString() }, model);

            return StatusCode(201);                                                                                 // return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(KlhkSents model, int? id)
        {
            KlhkSents klhkSents = _klhkSentRepo.FindByID(id.Value);

            if (klhkSents == null)
            {
                return StatusCode(404);                                                                            // return NotFound();
            }

            _klhkSentRepo.ApiUpdate(model, id.Value);
            return Ok(model);                                                                                         // return StatusCode(200);
        }
        
        [HttpDelete("{id}")]                                                                                           // [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(int? id)
        {
            var klhkSents = _klhkSentRepo.FindByID(id.Value);

            if (klhkSents == null)
            {
                return StatusCode(404);                                                                           // return NotFound();
            }

            _klhkSentRepo.Remove(id.Value);
            return Ok();                                                                                                // return StatusCode(200);
        }
    }
}