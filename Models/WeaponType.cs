using System.Text.Json.Serialization;

namespace WebAPITextRPG.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WeaponType
    {
        Sword = 1,
        Axe = 2,
        Staff = 3,
        Wand = 4,
        Spear = 5,
        Bow = 6,
        Dagger = 7
    }
}