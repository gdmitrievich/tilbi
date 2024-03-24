using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;

public static class TestsLoader
{
	private readonly static string _INITIAL_SCENE_TESTS_PATH;
	private readonly static string _HORROR_SCENE_TESTS_PATH;
	private readonly static string _BACK_ROOMS_SCENE_TESTS_PATH;
	private const string _COMMON_FILE_NAME = "test_";
	private const string _INCORRECT_TEST_FILE_NAME = "incorrect_test_";
	private const string _EXTENSION = ".txt";

	private static GameObject[] _pcs;
	private static List<int> _testFileIndexes;

	static TestsLoader()
	{
		string _LOCAL_PATH = Path.GetFullPath("Assets") + "\\GameData\\Tests\\";
		_INITIAL_SCENE_TESTS_PATH = _LOCAL_PATH + "InitialScene\\";
		_HORROR_SCENE_TESTS_PATH = _LOCAL_PATH + "HorrorScene\\";
		_BACK_ROOMS_SCENE_TESTS_PATH = _LOCAL_PATH + "BackRoomScene\\";
	}

	public static void Load(SceneManagerLogic.Scene scene)
	{
		_pcs = Utility.FindGameObjectsWithLayer(LayerMask.NameToLayer("PC"));
		if (_pcs == null)
		{
			return;
		}

		string filePath = GetPath(scene);

		_testFileIndexes = Enumerable.Range(1, _pcs.Length * 2).ToList();
		int i = 0;
		while (i < _pcs.Length)
		{
			// Gets random test index from the current list of test files.
			int randomIdx = Utility.ExtractRandomElementFromList(_testFileIndexes);
			// Remove the second test index from the similar topic.
			_testFileIndexes.Remove(randomIdx % 2 == 0 ? randomIdx - 1 : randomIdx + 1);

			Test pcTest = _pcs[i].GetComponent<Test>();
			Test test = new Test();

			if (!pcTest.IsIncorrect)
			{
				SetTestDataFromFIle(test, filePath + _COMMON_FILE_NAME + randomIdx.ToString() + _EXTENSION);
			}
			else
			{
				SetTestDataFromFIle(test, filePath + _INCORRECT_TEST_FILE_NAME + Random.Range(1, 3).ToString() + _EXTENSION);
			}

			while (test.TestItems.Count > 0) {
				var randomTestItem = Utility.ExtractRandomElementFromList(test.TestItems);
				var testItemWithMixedAnswers = GetTestItemWithMixedAnswers(randomTestItem);
				pcTest.TestItems.Add(testItemWithMixedAnswers);
			}

			pcTest.CorrectlyAnsweredQuestionAnswers = test.CorrectlyAnsweredQuestionAnswers;
			pcTest.TotalNumberOfCorrectAnswersOfQuestions = test.TotalNumberOfCorrectAnswersOfQuestions;
			pcTest.NumberOfQuestions = test.NumberOfQuestions;
			pcTest.AttemptsToPassTest = test.AttemptsToPassTest;

			++i;
		}
	}

	private static string GetPath(SceneManagerLogic.Scene scene)
	{
		switch (scene)
		{
			case SceneManagerLogic.Scene.Initial:
				return _INITIAL_SCENE_TESTS_PATH;
			case SceneManagerLogic.Scene.Horror:
				return _HORROR_SCENE_TESTS_PATH;
			case SceneManagerLogic.Scene.BackRooms:
				return _BACK_ROOMS_SCENE_TESTS_PATH;
			default:
				return "";
		}
	}

	private static void SetTestDataFromFIle(Test test, string filePath)
	{
		string? line;
		using (StreamReader reader = new StreamReader(filePath))
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

				SetTestItemFields(test, ref testItem, line);
			}
			test.NumberOfQuestions = i;
		}
	}

	private static void SetTestItemFields(Test test, ref Test.TestItem testItem, string line)
	{
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
				}
				test.TotalNumberOfCorrectAnswersOfQuestions += testItem.correctAnswers.Count;
				break;
		}
	}

	private static Test.TestItem GetTestItemWithMixedAnswers(Test.TestItem sourceTestItem)
	{
		Test.TestItem newTestItem = new Test.TestItem(sourceTestItem.question);
		Dictionary<string, int> answerIndexes = new Dictionary<string, int>();
		int i = 0;
		sourceTestItem.answers.ForEach((item) => {
			answerIndexes.Add(item, i++);
		});

		while (sourceTestItem.answers.Count > 0)
		{
			var answer = Utility.ExtractRandomElementFromList(sourceTestItem.answers, out int idx);
			newTestItem.answers.Add(answer);
			if (sourceTestItem.correctAnswers.Contains(answerIndexes[answer]))
			{
				newTestItem.correctAnswers.Add(newTestItem.answers.Count - 1);
			}
		}

		return newTestItem;
	}
}