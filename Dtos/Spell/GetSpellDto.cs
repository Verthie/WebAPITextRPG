using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Dtos.Spell
{
    public class GetSpellDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Fireball";
        public int Damage { get; set; } = 1;
        public SpellSchool School { get; set; } = SpellSchool.Evocation;
    }
}
