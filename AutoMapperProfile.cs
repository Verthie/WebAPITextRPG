using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Fight;
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
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Spell, GetSpellDto>();
            CreateMap<Character, HighscoreDto>();
        }
    }
}