using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Enums;
using CODE_GameLib.Models.Items;

namespace CODE_GameLib.Models.Entities;

public class Inventory
{
    private ICollection<IItem> Items { get; } = new List<IItem>();
    
    public void AddItem(IItem item)
    {
        Items.Add(item);
    }
    
    public int GetSankaraStonesCount()
    {
        return Items.Count(i => i is SankaraStone);
    }
    
    public bool HasKey(Color color)
    {
        return Items.Any(i => i is Key key && key.Color == color);
    }

    public Key[] GetKeys()
    {
        return Items.Where(i => i is Key).Cast<Key>().ToArray();
    }
}