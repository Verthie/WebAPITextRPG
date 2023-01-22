using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Dtos.Character
{
    public class AddCharacterSpellDto
    {
        public int CharacterId { get; set; }
        public int SpellId { get; set; }
    }
}