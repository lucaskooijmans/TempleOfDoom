using CODE_PersistenceLib.FileReaders;
using System;

namespace CODE_PersistenceLib
{
	public static class GameReaderFactory
	{
		public static IFileReader GetFileReader(string readerType)
		{
			return readerType switch
			{
				"json" => new JsonReader(),
				"xml" => new XMLReader(),
				_ => throw new NotImplementedException()
			};
		}
		public static string GetFileName(string readerType, string level)
		{

			return $"./Levels/TempleOfDoom{level}.{readerType}";

		}

	}
}
