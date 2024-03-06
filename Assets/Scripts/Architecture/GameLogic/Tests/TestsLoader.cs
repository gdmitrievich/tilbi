using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class TestsLoader
{
	private readonly static string _INITIAL_SCENE_TESTS_PATH;
	private readonly static string _HORROR_SCENE_TESTS_PATH;
	private readonly static string _BACK_ROOMS_SCENE_TESTS_PATH;
	private const string _COMMON_FILE_NAME = "test_";
	private const string _INCORRECT_TEST_FILE_NAME = "incorrect_test";
	private const string _EXTENSION = ".txt";

	private static GameObject[] _pcs;
	private static List<int> _testFiles;
	public static int count;

	static TestsLoader()
	{
		string _LOCAL_PATH = Path.GetFullPath("Assets") + "\\GameData\\Tests\\";
		_INITIAL_SCENE_TESTS_PATH = _LOCAL_PATH + "InitialScene\\";
		_HORROR_SCENE_TESTS_PATH = _LOCAL_PATH + "HorrorScene\\";
		_BACK_ROOMS_SCENE_TESTS_PATH = _LOCAL_PATH + "BackRoomScene\\";
		// Debug.Log(_LOCAL_PATH);
	}

	public static void Load(SceneManagerLogic.Scene scene)
	{
		_pcs = Utility.FindGameObjectsWithLayer(LayerMask.NameToLayer("PC"));
		// Debug.Log($"PCs count {_pcs.Count}");
		if (_pcs == null)
		{
			return;
		}

		string path;
		switch (scene)
		{
			case SceneManagerLogic.Scene.Initial:
				count = Directory.GetFiles(_INITIAL_SCENE_TESTS_PATH, _COMMON_FILE_NAME + "*").Length;
				path = _INITIAL_SCENE_TESTS_PATH;
				break;
			case SceneManagerLogic.Scene.Horror:
				count = Directory.GetFiles(_HORROR_SCENE_TESTS_PATH, _COMMON_FILE_NAME + "*").Length;
				path = _HORROR_SCENE_TESTS_PATH;
				break;
			case SceneManagerLogic.Scene.BackRooms:
				count = Directory.GetFiles(_BACK_ROOMS_SCENE_TESTS_PATH, _COMMON_FILE_NAME + "*").Length;
				path = _BACK_ROOMS_SCENE_TESTS_PATH;
				break;
			default:
				return;
		}

		_testFiles = Enumerable.Range(1, _pcs.Length).ToList();
		int i = 0, random = 0;
		while (i < _pcs.Length)
		{
			Test test = _pcs[i].GetComponent<Test>();
			random = Random.Range(1, _testFiles.Count);
			_testFiles.Remove(random);

			if (!test.IsIncorrect)
			{
				SetDataFromFile(test, path + _COMMON_FILE_NAME + random.ToString() + _EXTENSION);
			}
			else
			{
				SetDataFromFile(test, path + _INCORRECT_TEST_FILE_NAME + _EXTENSION);
			}

			++i;
			// for (int j = 0; j < test.TestItems.Count; ++j)
			// {
			// 	Debug.Log($"{j}: {test.TestItems[j].question} ");
			// 	for (int k = 0; k < test.TestItems[j].answers.Count; ++k) {
			// 		Debug.Log($"Answer: {test.TestItems[j].answers[k]} ");
			// 	}
			// 	for (int k = 0; k < test.TestItems[j].correctAnswers.Count; ++k) {
			// 		Debug.Log($"CorrectAnswer: {test.TestItems[j].correctAnswers[k]}");
			// 	}
			// }
		}

	}

	private static void SetDataFromFile(Test test, string path)
	{
		string? line;
		using (StreamReader reader = new StreamReader(path))
		{
			Test.TestItem testItem = new Test.TestItem(null);
			int i = 0;
			while (true)
			{
				line = reader.ReadLine();
				if (line == "")
				{
					test.TestItems.Add(testItem);
					testItem = new Test.TestItem(null);
					i++;
					continue;
				}
				else if (line == null)
				{
					test.TestItems.Add(testItem);
					i++;
					break;
				}

				switch (line[0])
				{
					case '0':
						testItem.question = line.Substring(2);
						break;
					case '1':
						testItem.answers.Add(line.Substring(2));
						break;
					case '2':
						for (int j = 2; j < line.Length; ++j)
						{
							testItem.correctAnswers.Add(line[j] - '0');
							test.TotalNumberOfCorrectAnswersOfQuestions += 1;
						}
						break;
				}
			}
			test.NumberOfQuestions = i;
		}
	}
}