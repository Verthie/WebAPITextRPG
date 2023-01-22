using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Models
{
    public class Spell
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Fireball"; //Spell name - default is "Fireball"

        public int Damage { get; set; } = 1; // Spell damage - default is 1

        public SpellSchool School { get; set; } = SpellSchool.Evocation; // Spell school - default is Evocation (List of all schools is in SpellSchool.cs in models)
    }
}
