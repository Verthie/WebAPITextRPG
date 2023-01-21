using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPITextRPG.Dtos.Spell;
using WebAPITextRPG.Services.SpellService;

namespace WebAPITextRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpellController : ControllerBase
    {
        private readonly ISpellService _spellService;

        public SpellController(ISpellService spellService)
        {
            _spellService = spellService;
        }

        [HttpGet("GetAll")] //Get method returning a list of all spells
        public async Task<ActionResult<ServiceResponse<List<GetSpellDto>>>> Get()
        {
            return Ok(await _spellService.GetAllSpells());
        }

        [HttpGet("{id}")] //Get method returnig a single spell using the id parameter
        public async Task<ActionResult<ServiceResponse<GetSpellDto>>> GetSingle(int id)
        {
            return Ok(await _spellService.GetSpellById(id)); //Returns the first spell where the id of the spells equals the given ID
        }

        [HttpPost] //POST method for creating a new spell
        public async Task<ActionResult<ServiceResponse<List<GetSpellDto>>>> AddSpell(AddSpellDto newSpell)
        {
            return Ok(await _spellService.AddSpell(newSpell));
        }

        [HttpPut] //PUT method for updating a spell
        public async Task<ActionResult<ServiceResponse<List<GetSpellDto>>>> UpdateSpell(UpdateSpellDto updatedSpell)
        {
            var response = await _spellService.UpdateSpell(updatedSpell);
            if (response.Data is null) //if spell was not found return response as notfound (404)
            {
                return NotFound(response);
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetSpellDto>>>> DeleteSpell(int id)
        {
            var response = await _spellService.DeleteSpell(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}