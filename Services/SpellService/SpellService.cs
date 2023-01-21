using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Spell;

namespace WebAPITextRPG.Services.SpellService
{
    public class SpellService : ISpellService
    {
        private static List<Spell> spells = new List<Spell> //creating a list of spells
        {
            new Spell(), //adding a default spell to the list
            new Spell { Id = 1, Name = "Inflict Wounds", Damage = 6, School = SpellSchool.Necromancy} //adding a spell named "Inflict Wounds" with an id = 1, damage = 6 and spell school = Necromancy
        };
        private readonly IMapper _mapper;

        public SpellService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetSpellDto>>> AddSpell(AddSpellDto newSpell) //adding spell method
        {
            var serviceResponse = new ServiceResponse<List<GetSpellDto>>(); //serviceResponse variable
            var spell = _mapper.Map<Spell>(newSpell); //Spells variable
            spell.Id = spells.Max(c => c.Id) + 1; //finding the max value of id and increasing it by one whenever a new spell is added
            spells.Add(spell); //creating a new spell
            serviceResponse.Data = spells.Select(c => _mapper.Map<GetSpellDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetSpellDto>>> DeleteSpell(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSpellDto>>();
            try //whenever we try to delete an spell that doesn't exist we catch an exception and display a massage
            {
                var spell = spells.FirstOrDefault(c => c.Id == id);
                if (spell is null) //checking if spell doesn't exist 
                    throw new Exception($"Spell with Id '{id}' not found."); //throwing an exception with a custom message

                spells.Remove(spell); //deleting the spell

                serviceResponse.Data = spells.Select(c => _mapper.Map<GetSpellDto>(c)).ToList(); //mapping response to DTO
            }
            catch (Exception ex) //contents of exception
            {
                serviceResponse.Success = false; //prompt saying signalising operation wasn't a success
                serviceResponse.Message = ex.Message; //exception message
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSpellDto>>> GetAllSpells() //Returning list of spells
        {
            var serviceResponse = new ServiceResponse<List<GetSpellDto>>();
            serviceResponse.Data = spells.Select(c => _mapper.Map<GetSpellDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSpellDto>> GetSpellById(int id) //Returning a single spell by id
        {
            var serviceResponse = new ServiceResponse<GetSpellDto>();
            var character = spells.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetSpellDto>(character); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSpellDto>> UpdateSpell(UpdateSpellDto updatedSpell)
        {

            var serviceResponse = new ServiceResponse<GetSpellDto>();
            try //whenever we try to update an spell that doesn't exist we catch an exception and display a massage
            {
                var spell = spells.FirstOrDefault(c => c.Id == updatedSpell.Id);
                if (spell is null) //checking if spell doesn't exist and throwing an exception with a custom message
                    throw new Exception($"Spell with Id '{updatedSpell.Id}' not found.");

                _mapper.Map(updatedSpell, spell); //mapping updated spell to spell

                //values that are allowed to be updated
                spell.Name = updatedSpell.Name;
                spell.Damage = updatedSpell.Damage;
                spell.School = updatedSpell.School;

                serviceResponse.Data = _mapper.Map<GetSpellDto>(spell); //mapping response to DTO
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
