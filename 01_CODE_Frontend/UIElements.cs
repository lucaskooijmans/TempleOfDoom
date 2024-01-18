#nullable enable
using System;
using CODE_GameLib.Enums;
using CODE_GameLib.Models.Doors;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Models.Entities.Enemy;
using CODE_GameLib.Models.Items;
using CODE_GameLib.Models.Utils;
using Pastel;

namespace CODE_Frontend;

public class UIElements
{
    public static readonly string PlayerChar = "P";
    public static readonly string EmptyChar = " ";
    private static readonly string SankaraStoneChar = "S";
    private static readonly string KeyChar = "K";
    private static readonly string BoobytrapChar = "O";
    private static readonly string DisappearingBoobytrapChar = "@";
    private static readonly string PressurePlateChar = "T";
    public static readonly string WallChar = "#";
    private static readonly string ClosingGateDoorChar = "\u2229";
    private static readonly string ToggleDoorChar = "\u2534";
    private static readonly string HorizontalDoorChar = "|";
    private static readonly string VerticalDoorChar = "=";
    public static readonly string IceChar = "~";
    private static readonly string EnemyChar = "E";

    public static string GetItemDisplay(IItem item)
    {
        return item switch
        {
            SankaraStone _ => SankaraStoneChar,
            PressurePlate _ => PressurePlateChar,
            Key key => KeyChar.Pastel(key.Color.ToConsoleColorOrNull() ?? ConsoleColor.Black),
            DisappearingBoobytrap _ => DisappearingBoobytrapChar,
            Boobytrap _ => BoobytrapChar,
            _ => "?"
        };
    }
    
    public static string GetDoorDisplay(IDoor door, Direction direction)
    {
        string doorCharacter = door switch
        {
            ClosingGateDoorDecorator _ => ClosingGateDoorChar,
            ToggleDoorDecorator _ => ToggleDoorChar,
            OpenOnOddLivesDecorator _ => direction.IsHorizontal() ? HorizontalDoorChar : VerticalDoorChar,
            OpenOnStonesInRoomDoorDecorator _ => direction.IsHorizontal() ? HorizontalDoorChar : VerticalDoorChar,
            Door _ => direction.IsHorizontal() ? HorizontalDoorChar : VerticalDoorChar,
            _ => "?"
        };
        
        IDoor? doorDecorator = BaseDoorDecorator.GetDecoratorOrNull(door, typeof(ColoredDoorDecorator));
        
        // to be able to set the text color of the door, check what color the door is, otherwise set to default color white
        if (doorDecorator is ColoredDoorDecorator coloredDoorDecorator)
        {
            doorCharacter = doorCharacter.Pastel(coloredDoorDecorator.GetColor().ToConsoleColorOrNull() ?? ConsoleColor.White);
        }

        return doorCharacter;
    }
    
    public static string GetEnemyDisplay(IEntity entity)
    {
        return entity switch
        {
            EnemyAdapter _ => EnemyChar,
            _ => "?"
        };
    }
}