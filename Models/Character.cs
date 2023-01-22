using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Gandalf"; //Character Name - default is "Gandalf"
        public int HitPoints { get; set; } = 100; //Character HitPoints - default is 100
        public int Strength { get; set; } = 10; //Character Strength - default is 10
        public int Defense { get; set; } = 10; //Character Defense - default is 10
        public int Intelligence { get; set; } = 10; //Character Intelligence - default is 10
        public RpgClass Class { get; set; } = RpgClass.Mage; //Character Class - default is Mage (List of all classes is in RpgClass.cs in models)
        public User? User { get; set; }
    }
}