using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class TestsLoader
{
	private readonly static string _LOCAL_PATH;
	private const string _COMMON_FILE_NAME = "test_";
	private const string _INCORRECT_TEST_FILE_NAME = "incorrect_test";
	private const string _EXTENSION = ".txt";

	private static List<GameObject> _pcs;
	public readonly static int count;

	static TestsLoader()
	{
		_LOCAL_PATH = Path.GetFullPath("Assets") + "\\GameData\\Tests\\";
		// Debug.Log(_LOCAL_PATH);
		count = Directory.GetFiles(_LOCAL_PATH, "*.txt").Length;
	}

	public static void Load()
	{
		// _pcs = GameObject.FindGameObjectsWithTag("PC").ToList();
		_pcs = Utility.FindGameObjectsWithLayer(LayerMask.NameToLayer("PC"))?.ToList();
		// Debug.Log($"PCs count {_pcs.Count}");
		if (_pcs == null) {
			return;
		}

		int i = 0, random = 0;
		while (i < count && _pcs.Count > 0)
		{
			random = Random.Range(0, _pcs.Count);
			Test test = _pcs[random].GetComponent<Test>();
			_pcs.RemoveAt(random);

			if (!test.IsIncorrect) {
				SetDataFromFile(test, _LOCAL_PATH + _COMMON_FILE_NAME + (i + 1).ToString() + _EXTENSION);
			} else {
				SetDataFromFile(test, _LOCAL_PATH + _INCORRECT_TEST_FILE_NAME + _EXTENSION);
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