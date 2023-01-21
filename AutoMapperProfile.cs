using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Item;

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
        }
    }
}