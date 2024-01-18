#nullable enable
using System;
using System.IO;
using CODE_GameLib.Enums;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Items;

namespace CODE_PersistenceLib;

public static class ItemFactory
{
    public static IItem Get(string type, Position position, int? damage, string? color)
    {
        switch (type)
        {
            case "key":
                Color keyColor = Enum.Parse<Color>(color ?? throw new InvalidDataException("A key needs a color value"), true); 
                return new Key(position, keyColor);
            case "boobytrap":
                return new Boobytrap(position, damage ?? throw new InvalidDataException("A boobytrap needs a damage value"));
            case "disappearing boobytrap":
                return new DisappearingBoobytrap(position, damage ?? throw new InvalidDataException("A disappearing boobytrap needs a damage value"));
            case "sankara stone":
                return new SankaraStone(position);
            case "pressure plate":
                return new PressurePlate(position);
            default:
                throw new ArgumentException("Invalid item type");
        }
    }
}