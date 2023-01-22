using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITextRPG.Dtos.Spell;

namespace WebAPITextRPG.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Gandalf";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Mage;
        public GetWeaponDto? Weapon { get; set; }
        public List<GetSpellDto>? Spells { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}