using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Spell;

namespace WebAPITextRPG.Services.SpellService
{
    public class SpellService : ISpellService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SpellService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ServiceResponse<List<GetSpellDto>>> AddSpell(AddSpellDto newSpell) //adding spell method
        {
            var serviceResponse = new ServiceResponse<List<GetSpellDto>>(); //serviceResponse variable
            var spell = _mapper.Map<Spell>(newSpell); //Spells variable

            _context.Spells.Add(spell); //creating a new spell
            await _context.SaveChangesAsync(); //writing changes to database and generating new ID for spell
            serviceResponse.Data =
                await _context.Spells.Select(c => _mapper.Map<GetSpellDto>(c)).ToListAsync();
            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetSpellDto>>> DeleteSpell(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSpellDto>>();
            try //whenever we try to delete a spell that doesn't exist we catch an exception and display a massage
            {
                var spell = await _context.Spells.FirstOrDefaultAsync(c => c.Id == id); //choosing the spell by id
                if (spell is null) //checking if spell doesn't exist 
                    throw new Exception($"Spell with Id '{id}' not found."); //throwing an exception with a custom message

                _context.Spells.Remove(spell); //deleting the spell

                await _context.SaveChangesAsync(); //writing changes to database

                serviceResponse.Data = await _context.Spells.Select(c => _mapper.Map<GetSpellDto>(c)).ToListAsync();
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
            var dbSpells = await _context.Spells.ToListAsync(); //getting all spells from database
            serviceResponse.Data = dbSpells.Select(c => _mapper.Map<GetSpellDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSpellDto>> GetSpellById(int id) //Returning a single spell by id
        {
            var serviceResponse = new ServiceResponse<GetSpellDto>();
            var dbSpell = await _context.Spells.FirstOrDefaultAsync(c => c.Id == id); //getting a spell from database
            serviceResponse.Data = _mapper.Map<GetSpellDto>(dbSpell); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSpellDto>> UpdateSpell(UpdateSpellDto updatedSpell)
        {

            var serviceResponse = new ServiceResponse<GetSpellDto>();
            try //whenever we try to update an spell that doesn't exist we catch an exception and display a massage
            {
                var spell = await _context.Spells.FirstOrDefaultAsync(c => c.Id == updatedSpell.Id); //choosing the spell by id
                if (spell is null) //checking if spell doesn't exist 
                    throw new Exception($"Spell with Id '{updatedSpell.Id}' not found."); //throwing an exception with a custom message

                spell.Name = updatedSpell.Name; //updating the spell
                spell.Damage = updatedSpell.Damage;
                spell.School = updatedSpell.School;

                await _context.SaveChangesAsync(); //writing changes to database
                serviceResponse.Data = _mapper.Map<GetSpellDto>(spell);
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
