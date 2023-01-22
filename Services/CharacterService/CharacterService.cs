using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPITextRPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
        .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter) //adding character method
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character); //creating a new character
            await _context.SaveChangesAsync(); //writing changes to database and generating new ID for character
            serviceResponse.Data =
                await _context.Characters
                    .Where(c => c.User!.Id == GetUserId()) //condition for displaying list of characters belonging only to one user
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();

            return serviceResponse; //sending the response to controller
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try //whenever we try to delete a character that doesn't exist we catch an exception and display a massage
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId()); //choosing the character and its user by id, this allows to only delete characters that belong to their user
                if (character is null) //checking if character doesn't exist 
                    throw new Exception($"Chracter with Id '{id}' not found."); //throwing an exception with a custom message

                _context.Characters.Remove(character); //deleting the character

                await _context.SaveChangesAsync(); //writing changes to database

                serviceResponse.Data = await _context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();
            }
            catch (Exception ex) //contents of exception
            {
                serviceResponse.Success = false; //prompt signalising operation wasn't a success
                serviceResponse.Message = ex.Message; //exception message
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters() //Returning list of characters belonging to a single user
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Spells)
                .Where(c => c.User!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id) //Returning a single character by id
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Spells)
                .FirstOrDefaultAsync(c => c.Id == id); //choosing the character by id
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter); //mapping response to DTO
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try //whenever we try to update a character that doesn't exist we catch an exception and display a massage
            {
                var character =
                    await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character is null || character.User!.Id != GetUserId()) //checking if character doesn't exist and throwing an exception with a custom message
                    throw new Exception($"Chracter with Id '{updatedCharacter.Id}' not found.");

                //values that are allowed to be updated
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSpell(AddCharacterSpellDto newCharacterSpell)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Spells)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSpell.CharacterId &&
                    c.User!.Id == GetUserId());

                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                var spell = await _context.Spells
                    .FirstOrDefaultAsync(s => s.Id == newCharacterSpell.SpellId);
                if (spell is null)
                {
                    response.Success = false;
                    response.Message = "Spell not found.";
                    return response;
                }

                character.Spells!.Add(spell);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}