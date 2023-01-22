using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Weapon;

namespace WebAPITextRPG.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public WeaponService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetWeaponDto>>> AddWeapon(AddWeaponDto newWeapon) //adding weapon method
        {
            var serviceResponse = new ServiceResponse<List<GetWeaponDto>>(); //serviceResponse variable
            var weapon = _mapper.Map<Weapon>(newWeapon); //Weapon variable

            _context.Weapons.Add(weapon); //creating a new weapon
            await _context.SaveChangesAsync(); //writing changes to database and generating new ID for weapon
            serviceResponse.Data =
                await _context.Weapons.Select(c => _mapper.Map<GetWeaponDto>(c)).ToListAsync();
            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetWeaponDto>>> DeleteWeapon(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetWeaponDto>>();
            try //whenever we try to delete an weapon that doesn't exist we catch an exception and display a massage
            {
                var weapon = await _context.Weapons.FirstOrDefaultAsync(c => c.Id == id); //choosing the weapon by id
                if (weapon is null) //checking if weapon doesn't exist 
                    throw new Exception($"weapon with Id '{id}' not found."); //throwing an exception with a custom message

                _context.Weapons.Remove(weapon); //deleting the weapon

                await _context.SaveChangesAsync(); //writing changes to database

                serviceResponse.Data = await _context.Weapons.Select(c => _mapper.Map<GetWeaponDto>(c)).ToListAsync();
            }
            catch (Exception ex) //contents of exception
            {
                serviceResponse.Success = false; //prompt saying signalising operation wasn't a success
                serviceResponse.Message = ex.Message; //exception message
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons() //Returning list of weapons
        {
            var serviceResponse = new ServiceResponse<List<GetWeaponDto>>();
            var dbWeapons = await _context.Weapons.ToListAsync(); //getting all weapons from database
            serviceResponse.Data = dbWeapons.Select(c => _mapper.Map<GetWeaponDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetWeaponDto>> GetWeaponById(int id) //Returning a single weapon by id
        {
            var serviceResponse = new ServiceResponse<GetWeaponDto>();
            var dbWeapon = await _context.Weapons.FirstOrDefaultAsync(c => c.Id == id); //getting weapon from database
            serviceResponse.Data = _mapper.Map<GetWeaponDto>(dbWeapon); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetWeaponDto>> UpdateWeapon(UpdateWeaponDto updatedWeapon)
        {

            var serviceResponse = new ServiceResponse<GetWeaponDto>();
            try //whenever we try to update an weapon that doesn't exist we catch an exception and display a massage
            {
                var weapon =
                    await _context.Weapons.FirstOrDefaultAsync(c => c.Id == updatedWeapon.Id); //choosing the weapon by id
                if (weapon is null) // checking if weapon doesn't exist
                    throw new Exception($"Weapon with Id '{updatedWeapon.Id}' not found."); //throwing an exception with a custom message

                //values that are allowed to be updated
                weapon.Name = updatedWeapon.Name;
                weapon.Damage = updatedWeapon.Damage;
                weapon.Type = updatedWeapon.Type;

                await _context.SaveChangesAsync(); //writing changes to database
                serviceResponse.Data = _mapper.Map<GetWeaponDto>(weapon); //mapping response to DTO
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
