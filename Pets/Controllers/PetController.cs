using System;
using Microsoft.AspNetCore.Mvc;
using Pets.Services;
using Pets.Models;

namespace Pets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
	{
        private readonly PetService _petService;

        public PetController(PetService service)
        {
            this._petService = service;
        }

        [HttpGet]
        public async Task<List<Pet>> Get()
        {
            return await this._petService.Get();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Pet>> GetById(string id)
        {
            var item = await this._petService.GetById(id);
            if (item is null) return NotFound();

            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pet item)
        {
            await _petService.Create(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPatch("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Pet itemModel)
        {
            var item = await this._petService.GetById(id);
            if (item is null) return NotFound();

            itemModel.Id = item.Id;

            await this._petService.Patch(id, itemModel);
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await this._petService.GetById(id);
            if (item is null) return NotFound();

            await this._petService.DeleteById(id);

            return NoContent();
        }
    }
}

