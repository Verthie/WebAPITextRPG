using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Dtos.Fight
{
    public class SpellAttackDto
    {
        public int AttackerId { get; set; }
        public int OpponentId { get; set; }
        public int SpellId { get; set; }
    }
}