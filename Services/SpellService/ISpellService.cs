using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Spell;

namespace WebAPITextRPG.Services.SpellService
{
    public interface ISpellService
    {
        Task<ServiceResponse<List<GetSpellDto>>> GetAllSpells();
        Task<ServiceResponse<GetSpellDto>> GetSpellById(int id);
        Task<ServiceResponse<List<GetSpellDto>>> AddSpell(AddSpellDto newSpell);
        Task<ServiceResponse<GetSpellDto>> UpdateSpell(UpdateSpellDto updatedSpell);
        Task<ServiceResponse<List<GetSpellDto>>> DeleteSpell(int id);

    }
}
