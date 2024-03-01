using System;
using System.IO;
using TMPro;
using UnityEngine;

public static class CheetSheetsLoader
{
	private readonly static string _LOCAL_PATH;
	private const string _COMMON_FILE_NAME = "cheet_sheet_";
	private const string _EXTENSION = ".txt";
	private static GameObject[] _cheetSheets;
	public readonly static int count;

	static CheetSheetsLoader()
	{
		_LOCAL_PATH = Path.GetFullPath("Assets") + "\\GameData\\CheetSheets\\";
		// Debug.Log(_LOCAL_PATH);
		_cheetSheets = null;
		count = Directory.GetFiles(_LOCAL_PATH, "*.txt").Length;
	}

	public static void Load()
	{
		_cheetSheets = GameObject.FindGameObjectsWithTag("CheetSheet");
		// Debug.Log($"Cheet sheets count {_cheetSheets.Length}");

		// foreach (var obj in _cheetSheets) {
		// 	Debug.Log($"x: {obj.transform.position.x} y: {obj.transform.position.y} z: {obj.transform.position.z}");
		// 	Debug.Log(obj.name, obj);
		// }

		int offset = count == _cheetSheets.Length ? 0 : count;
		for (int i = offset; i < _cheetSheets.Length; ++i)
		{
			SetDataFromFile(_cheetSheets[i].GetComponent<CheetSheet>(), _LOCAL_PATH + _COMMON_FILE_NAME + (i + 1 - offset).ToString() + _EXTENSION);
		}
	}

	private static void SetDataFromFile(CheetSheet cheetSheet, string path)
	{
		string? line;
		using (StreamReader reader = new StreamReader(path))
		{
			while ((line = reader.ReadLine()) != null)
			{
				if (line != "")
				{
				// Debug.Log($"Line: {line}");
					cheetSheet.hints.Add(line);
				// Debug.Log($"Hint: {cheetSheet.hints[0]}");
				}
			}
		}
	}
}