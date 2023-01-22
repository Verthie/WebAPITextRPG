using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();

            CreateMap<Spells, GetSpellDto>();
            CreateMap<AddSpellDto, Spells>();
            CreateMap<UpdateSpellDto, Spells>();

            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<UpdateWeaponDto, Weapon>();
        }
    }
}