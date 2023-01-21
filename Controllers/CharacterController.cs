using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPITextRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character> //creating a list of characters
        {
            new Character(),
            new Character { Id = 1, Name = "Lunk"} //adding a character named "Lunk" with an id = 1 and the rest are default values
        };

        [HttpGet("GetAll")] //Get method returning a list of all characters
        public ActionResult<List<Character>> Get()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")] //Get method returnig a single character using the id parameter
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(characters.FirstOrDefault(c => c.Id == id)); //Returns the first character where the id of the characters equals the given ID
        }

        [HttpPost] //POST method for creating a new character
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }
    }
}