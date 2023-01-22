using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Weapon;

namespace WebAPITextRPG.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
