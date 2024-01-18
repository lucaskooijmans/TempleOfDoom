using System;
using System.Linq;
using CODE_GameLib.Enums;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Models.Items;
using CODE_GameLib.Models.Room;

namespace CODE_Frontend;

public class GameFrame
{
    private readonly FrameBuffer _buffer = new();

    private string GetTopUI(string levelPath)
    {
        return "   Welcome to Temple of Doom                                                            \n" +
               $"   Current Level: {levelPath}\n" +
               "----------------------------------------------------------------------------------------\n" +
               "----------------------------------------------------------------------------------------\n";
    }

    private string GetInventoryUI(Inventory inventory)
    {
        string inventoryString = "Items in inventory: \n";
        Key[] keys = inventory.GetKeys();
        if (keys.Any())
        {
            inventoryString += "Keys: \n";
            keys.ToList().ForEach(key => inventoryString += $"- {key.Color} key\n");
        }
        inventoryString += $"Sankara Stones: {inventory.GetSankaraStonesCount()}\n";
        return inventoryString;
    }
    
    private string GetPlayerLivesUI(Player player)
    {
        return $"Lives: {player.Lives}\n";
    }


    private string GetBottomUI()
    {
        return "----------------------------------------------------------------------------------------\n" +
               "----------------------------------------------------------------------------------------\n" +
               "   A game for the course Code Development (23/24) by Sven Goossens and Lucas Kooijmans  \n" +
               "----------------------------------------------------------------------------------------\n" +
               "----------------------------------------------------------------------------------------\n";
    }

    private void BuildBuffer(Level level)
    {
        _buffer.Clear();
        BuildRooms(level.Player);
        BuildDoors(level.Player.CurrentRoom);
        BuildItems(level.Player.CurrentRoom);
        BuildPlayer(level.Player);
        BuildSpecialFloors(level.Player.CurrentRoom);
        BuildEnemies(level.Player.CurrentRoom);
    }

    private void BuildEnemies(Room currentRoom)
    {
        currentRoom.Entities.Where(entity => !entity.IsDead).ToList().ForEach(entity =>
        {
            _buffer.SetPixel(entity.Position, UIElements.GetEnemyDisplay(entity));
        });
    }

    private void BuildSpecialFloors(Room currentRoom)
    {
        currentRoom.SpecialFloors.ForEach(specialFloor => _buffer.SetPixel(specialFloor.Position, 
            UIElements.IceChar));
    }

    private void BuildDoors(Room currentRoom)
    {
        foreach ((Direction direction, Connection connection) in currentRoom.ConnectedRooms)
        {
           string doorDisplay = UIElements.GetDoorDisplay(connection.Door, direction);
           _buffer.SetPixel(currentRoom.GetEntrancePosition(direction), doorDisplay);
            
        }
    }

    private void BuildItems(Room playerCurrentRoom)
    {
        foreach (IItem item in playerCurrentRoom.Items.Where(item => item.Exists))
        {
            _buffer.SetPixel(item.Position, UIElements.GetItemDisplay(item));
        }
    }

    private void BuildPlayer(Player player)
    {
        _buffer.SetPixel(player.Position, UIElements.PlayerChar);
    }

    private void BuildRooms(Player player)
    {
        Room currentRoom = player.CurrentRoom;
        
        // Draw top and bottom walls
        for (int x = currentRoom.LeftX(); x <= currentRoom.RightX(); x++)
        {
            
            _buffer.SetPixel(new Position(x, currentRoom.TopY()), UIElements.WallChar);
            _buffer.SetPixel(new Position(x, currentRoom.BottomY()), UIElements.WallChar);
        }
        
        // Draw left and right walls
        for (int y = currentRoom.BottomY(); y <= currentRoom.TopY(); y++)
        {
            _buffer.SetPixel(new Position(currentRoom.LeftX(), y), UIElements.WallChar);
            _buffer.SetPixel(new Position(currentRoom.RightX(), y), UIElements.WallChar);
        }
    }

    public void Render(Level level)
    {
        Console.WriteLine(GetTopUI(level.LevelName));
        BuildBuffer(level);
        _buffer.Render();
        Console.WriteLine(GetPlayerLivesUI(level.Player));
        Console.WriteLine(GetInventoryUI(level.Player.Inventory));
        Console.WriteLine(GetBottomUI());
    }
}