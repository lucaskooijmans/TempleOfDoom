using CODE_PersistenceLib;
using CODE_PersistenceLib.DTO;
using CODE_PersistenceLib.FileReaders;
using System.Text;
using CODE_Frontend;
using CODE_GameLib.Models;


namespace TempleOfDoom
{
    public abstract class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                
                Console.CursorVisible = false;

                Dictionary<string, string> readerType = GameView.AskReaderType();
                readerType.TryGetValue("fileType", out string? fileType);
                IFileReader reader = GameReaderFactory.GetFileReader(fileType);
                readerType.TryGetValue("level", out string? level);
                string fileName = GameReaderFactory.GetFileName(fileType, level);
                DTOLevel? levelDto = reader.Read(fileName);
                if (levelDto == null) return;
                Level levelData = LevelFactory.CreateLevel(levelDto, fileName);
                Game game = new Game(levelData);
                game.Run();

            } catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}