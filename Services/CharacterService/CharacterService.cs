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
            new Character(),
            new Character { Id = 1, Name = "Lunk"} //adding a character named "Lunk" with an id = 1 and the rest are default values
        };

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter) //Adding character
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            characters.Add(newCharacter);
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters() //Returning list of characters
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id) //Returning a single character by id
        {
            var serviceResponse = new ServiceResponse<Character>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = character;
            return serviceResponse;
            /*
            if (character is not null) //Checking if character with the given id exists in database
                return character; //If true - returns the character

            throw new Exception("Character not found"); //If false - throws an exception
            */
        }
    }
}
