using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Models.Entities.Enemy;

public class Field : IField
{
    public IField GetNeighbour(int direction)
    {
        return new Field();
    }

    public bool CanEnter { get; }
    public IPlacable Item { get; set; }
}