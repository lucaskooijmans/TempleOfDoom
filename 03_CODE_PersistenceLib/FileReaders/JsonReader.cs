using CODE_PersistenceLib.DTO;
using System;
using System.IO;
using System.Text.Json;

namespace CODE_PersistenceLib.FileReaders
{
    public class JsonReader : IFileReader
    {

		public DTOLevel Read(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                DTOLevel levelData = JsonSerializer.Deserialize<DTOLevel>(json) ??
				throw new InvalidDataException("JSON is null");

                
				return levelData;
            } catch (Exception)
            {
                return null;
            }
        }
    }

}
