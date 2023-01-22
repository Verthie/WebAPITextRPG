using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Item;
using WebAPITextRPG.Dtos.Spell;
using WebAPITextRPG.Dtos.Weapon;

namespace WebAPITextRPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();

            CreateMap<Spell, GetSpellDto>();
            CreateMap<AddSpellDto, Spell>();
            CreateMap<UpdateSpellDto, Spell>();

            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<UpdateWeaponDto, Weapon>();
        }
    }
}