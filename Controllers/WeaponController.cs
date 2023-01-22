using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPITextRPG.Dtos.Weapon;
using WebAPITextRPG.Services.WeaponService;

namespace WebAPITextRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpGet("GetAll")] //Get method returning a list of all weapons
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> Get()
        {
            return Ok(await _weaponService.GetAllWeapons());
        }

        [HttpGet("{id}")] //Get method returnig a single weapon using the id parameter
        public async Task<ActionResult<ServiceResponse<GetWeaponDto>>> GetSingle(int id)
        {
            return Ok(await _weaponService.GetWeaponById(id)); //Returns the first weapon where the id of the weapons equals the given ID
        }

        [HttpPost] //POST method for creating a new weapon
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> Addweapon(AddWeaponDto newweapon)
        {
            return Ok(await _weaponService.AddWeapon(newweapon));
        }

        [HttpPut] //PUT method for updating a weapon
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> UpdateWeapon(UpdateWeaponDto updatedWeapon)
        {
            var response = await _weaponService.UpdateWeapon(updatedWeapon);
            if (response.Data is null) //if weapon was not found return response as notfound (404)
            {
                return NotFound(response);
            }
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> DeleteWeapon(int id)
        {
            var response = await _weaponService.DeleteWeapon(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}