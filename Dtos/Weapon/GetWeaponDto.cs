using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Dtos.Weapon
{
    public class GetWeaponDto
    {
        public string Name { get; set; } = "Longsword";
        public int Damage { get; set; } = 1;
    }
}