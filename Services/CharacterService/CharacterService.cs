using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter) //adding character method
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>(); //serviceResponse variable
            var character = _mapper.Map<Character>(newCharacter); //Character variable

            _context.Characters.Add(character); //creating a new character
            await _context.SaveChangesAsync(); //writing changes to database and generating new ID for character
            serviceResponse.Data =
                await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try //whenever we try to delete a character that doesn't exist we catch an exception and display a massage
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id); //choosing the character by id
                if (character is null) //checking if character doesn't exist 
                    throw new Exception($"Chracter with Id '{id}' not found."); //throwing an exception with a custom message

                _context.Characters.Remove(character); //deleting the character

                await _context.SaveChangesAsync(); //writing changes to database

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex) //contents of exception
            {
                serviceResponse.Success = false; //prompt signalising operation wasn't a success
                serviceResponse.Message = ex.Message; //exception message
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters() //Returning list of characters
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id) //Returning a single character by id
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id); //choosing the character by id
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {

            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try //whenever we try to update a character that doesn't exist we catch an exception and display a massage
            {
                var character =
                    await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character is null) //checking if character doesn't exist and throwing an exception with a custom message
                    throw new Exception($"Chracter with Id '{updatedCharacter.Id}' not found.");

                //values that are allowed to be updated
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character); //mapping response to DTO
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