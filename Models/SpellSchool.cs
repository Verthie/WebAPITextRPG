using System.Text.Json.Serialization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SpellSchool
    {
        Abjuration = 1,
        Conjuration = 2,
        Divination = 3,
        Enchantment = 4,
        Evocation = 5,
        Illusion = 6,
        Necromancy = 7,
        Transmutation = 8
    }
}