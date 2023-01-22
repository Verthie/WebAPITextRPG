using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Weapon;

namespace WebAPITextRPG.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons();
        Task<ServiceResponse<GetWeaponDto>> GetWeaponById(int id);
        Task<ServiceResponse<List<GetWeaponDto>>> AddWeapon(AddWeaponDto newWeapon);
        Task<ServiceResponse<GetWeaponDto>> UpdateWeapon(UpdateWeaponDto updatedWeapon);
        Task<ServiceResponse<List<GetWeaponDto>>> DeleteWeapon(int id);

    }
}
