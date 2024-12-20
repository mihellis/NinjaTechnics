﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Technico.Dtos;
using Technico.Models;
using Technico.Services;

namespace Technico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly RepairService _repairService;

        public RepairController(RepairService repairService)
        {
            _repairService = repairService;
        }

        // GET: api/Repair
        [HttpGet]
        public async Task<ActionResult<List<Repair?>>> GetAll()
        {
            return await _repairService.GetAllAsync();
        }

        // GET: api/Repair/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Repair>> GetById(Guid id)
        {
            var repair = await _repairService.GetAsync(id);
            if (repair == null)
            {
                return NotFound();
            }
            return repair;
        }

        // GET: api/Repair/{id}
        [HttpGet("property/{id}")]
        public async Task<ActionResult<List<Repair?>>> GetByPropertyId(Guid id)
        {
            return await _repairService.GetRepairsByPropertyIdAsync(id);
        }

        // POST: api/Repair
        [HttpPost]
        public async Task<ActionResult<Repair>> PostRepair(RepairDTO repair)
        {
            var newRepair = await _repairService.CreateAsync(repair);
            return CreatedAtAction("GetById", new { id = newRepair.Id }, newRepair);
        }

        // PUT: api/Repair/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRepair(Guid id, RepairDTO repair)
        {
            repair.Id = id;
            var updatedRepair = await _repairService.UpdateAsync(repair);
            if (updatedRepair == null)
            {
                return NotFound();
            }

            return Ok(updatedRepair);
        }

        // DELETE: api/Repair/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepair(Guid id)
        {
            var result = await _repairService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
