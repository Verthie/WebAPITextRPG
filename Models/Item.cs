using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Longsword"; //Item Name - default is "Longsword"
        public int Damage { get; set; } = 1; //Item Damage - default is 1
        public ItemType Type { get; set; } = ItemType.Sword; //Item Type - default is Sword (List of all types is in ItemType.cs in models)
    }
}