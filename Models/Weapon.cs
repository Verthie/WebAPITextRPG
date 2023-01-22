using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Longsword"; //Weapon Name - default is "Longsword"
        public int Damage { get; set; } = 1; //Weapon Damage - default is 1
        public WeaponType Type { get; set; } = WeaponType.Sword; //Weapon Type - default is Sword (List of all types is in WeaponType.cs in models)
    }
}