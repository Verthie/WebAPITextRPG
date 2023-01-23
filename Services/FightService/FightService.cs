using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Fight;

namespace WebAPITextRPG.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FightService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                var characters = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Spells)
                    .Where(c => request.CharacterIds.Contains(c.Id))
                    .ToListAsync();

                var defeatedcount = 0;
                List<Character> AlreadyDefeated = new List<Character>();
                bool defeated = false; //fight ends when one character gets defeated
                while (!defeated)
                {
                    foreach (var attacker in characters) //all characters will attack in order
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList(); //getting list of opponents
                        var opponent = opponents[new Random().Next(opponents.Count)]; //choosing the opponent

                        int damage = 0;
                        string attackUsed = string.Empty;

                        if (!AlreadyDefeated.Contains(attacker)) //if attacker is not already defeated
                        {
                            bool useWeapon = new Random().Next(2) == 0;
                            if (!useWeapon && attacker.Spells?.Count == 0 && attacker.Weapon is not null)
                            {
                                attackUsed = attacker.Weapon.Name;
                                damage = DoWeaponAttack(attacker, opponent);
                            }
                            else if (useWeapon && attacker.Weapon is not null)
                            {
                                attackUsed = attacker.Weapon.Name;
                                damage = DoWeaponAttack(attacker, opponent);
                            }
                            else if (!useWeapon && attacker.Spells is not null)
                            {
                                var spell = attacker.Spells[new Random().Next(attacker.Spells.Count)];
                                attackUsed = spell.Name;
                                damage = DoSpellAttack(attacker, opponent, spell);
                            }
                            else
                            {
                                response.Data.Log
                                    .Add($"{attacker.Name} forgot how to attack!"); //they can't attack with the attack they don't have, like if they don't have a weapon or spell
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }


                        if (!AlreadyDefeated.Contains(opponent) && !AlreadyDefeated.Contains(attacker)) //if opponent is not already defeated
                        {
                            response.Data.Log
                                .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage");
                        }
                        else if (AlreadyDefeated.Contains(opponent) && !AlreadyDefeated.Contains(attacker))
                        {
                            response.Data.Log
                                .Add($"{attacker.Name} attacks {opponent.Name}'s corpse with {attackUsed}, what an outstanding move!");
                        }

                        if (opponent.HitPoints <= 0 && !AlreadyDefeated.Contains(opponent)) //if opponent has health lower or equal than 0 and is not already defeated
                        {
                            opponent.Defeats++;
                            response.Data.Log.Add($"{opponent.Name} has been defeated by {attacker.Name}!");
                            defeatedcount++;
                            AlreadyDefeated.Add(opponent);
                        }

                        if (defeatedcount == opponents.Count())
                        {
                            attacker.Victories++;
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            defeated = true;
                            break;
                        }
                    }
                }
                /*
                foreach (var character in AllDefeated)
                {
                    characters.Add(character);
                }
                */

                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> SpellAttack(SpellAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Spells)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker is null || opponent is null || attacker.Spells is null)
                    throw new Exception("Something fishy is going on here...");

                var spell = attacker.Spells.FirstOrDefault(s => s.Id == request.SpellId);
                if (spell is null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't know that spell!";
                    return response;
                }

                int damage = DoSpellAttack(attacker, opponent, spell);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoSpellAttack(Character attacker, Character opponent, Spell spell)
        {
            int damage = spell.Damage + (new Random().Next(attacker.Intelligence)); //weapon damage + random from 0 to strength
            damage -= new Random().Next(opponent.Defense); //damage = damage - random from 0 to opponent defense

            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker is null || opponent is null || attacker.Weapon is null)
                    throw new Exception("Something fishy is going on here...");

                int damage = DoWeaponAttack(attacker, opponent);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoWeaponAttack(Character attacker, Character opponent)
        {
            if (attacker.Weapon is null)
                throw new Exception("Attacker forgot to bring his weapon!");

            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength)); //weapon damage + random from 0 to strength
            damage -= new Random().Next(opponent.Defense); //damage = damage - random from 0 to opponent defense

            if (damage > 0) //do damage if damage is higher than 0
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<List<HighscoreDto>>> GetHighscore()
        {
            var characters = await _context.Characters
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            var response = new ServiceResponse<List<HighscoreDto>>()
            {
                Data = characters.Select(c => _mapper.Map<HighscoreDto>(c)).ToList()
            };

            return response;
        }
    }
}