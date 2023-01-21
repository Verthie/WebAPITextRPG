using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> //creating a list of characters
        {
            new Character(), //adding a default character to the list
            new Character { Id = 1, Name = "Lunk"} //adding a character named "Lunk" with an id = 1 and the rest are default values
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter) //adding character method
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>(); //serviceResponse variable
            var character = _mapper.Map<Character>(newCharacter); //Character variable
            character.Id = characters.Max(c => c.Id) + 1; //finding the max value of id and increasing it by one whenever a new character is added
            characters.Add(character); //creating a new character
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try //whenever we try to delete a character that doesn't exist we catch an exception and display a massage
            {
                var character = characters.FirstOrDefault(c => c.Id == id);
                if (character is null) //checking if character doesn't exist 
                    throw new Exception($"Chracter with Id '{id}' not found."); //throwing an exception with a custom message

                characters.Remove(character); //deleting the character

                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); //mapping response to DTO
            }
            catch (Exception ex) //contents of exception
            {
                serviceResponse.Success = false; //prompt saying signalising operation wasn't a success
                serviceResponse.Message = ex.Message; //exception message
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters() //Returning list of characters
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList(); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id) //Returning a single character by id
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {

            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try //whenever we try to update a character that doesn't exist we catch an exception and display a massage
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if (character is null) //checking if character doesn't exist and throwing an exception with a custom message
                    throw new Exception($"Chracter with Id '{updatedCharacter.Id}' not found.");

                _mapper.Map(updatedCharacter, character); //mapping updated character to character

                //values that are allowed to be updated
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

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