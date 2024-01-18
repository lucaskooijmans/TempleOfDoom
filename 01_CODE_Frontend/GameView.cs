using System;
using System.Collections.Generic;
using CODE_GameLib.Models;

namespace CODE_Frontend;

public static class GameView
{
	private static readonly GameFrame _gameFrame = new();

	public static Dictionary<string, string> AskReaderType()
	{
		Dictionary<string, string> levelInfo = new()
		{
			{ "fileType", "json" }
		};
		Console.WriteLine("What level do you want to play? Type '1' or '2'");
		string level = Console.ReadLine();
		levelInfo.Add("level", level);
		return levelInfo;
	}


	private static string GetTopUI(string levelPath)
	{
		return "   Welcome to Temple of Doom                                                             \n" +
		       $"   Current Level: {levelPath}															  \n" +
		       "----------------------------------------------------------------------------------------\n" +
		       "----------------------------------------------------------------------------------------\n";
	}

	private static string GetBottomUI()
	{
		return "----------------------------------------------------------------------------------------\n" +
		       "----------------------------------------------------------------------------------------\n" +
		       "   A game for the course Code Development (23/24) by Lucas Kooijmans and Sven Goossens  \n" +
		       "----------------------------------------------------------------------------------------\n" +
		       "----------------------------------------------------------------------------------------\n";
	}

	public static void Render(Level level)
	{
		Console.Clear();
		_gameFrame.Render(level);
	}
}