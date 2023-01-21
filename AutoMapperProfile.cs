using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Item;
using WebAPITextRPG.Dtos.Spell;

namespace WebAPITextRPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();

            CreateMap<Item, GetItemDto>();
            CreateMap<AddItemDto, Item>();
            CreateMap<UpdateItemDto, Item>();

            CreateMap<Spell, GetSpellDto>();
            CreateMap<AddSpellDto, Spell>();
            CreateMap<UpdateSpellDto, Spell>();
        }
    }
}