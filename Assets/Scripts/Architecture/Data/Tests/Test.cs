using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public struct TestItem
	{
		public string question;
		public List<string> answers;
		public List<int> correctAnswers;
		public TestItem(string question = null, List<string> answers = null, List<int> correctAnswers = null)
		{
			this.question = question;
			this.answers = new List<string>();
			this.correctAnswers = new List<int>();
		}
	}

	private const int _MIN_PERSENT_FOR_SUCCESS = 60;
	private int _correctlyAnsweredQuestions;

	private List<TestItem> _testItems;
	public List<TestItem> TestItems {
		get => _testItems;
		set {
			if (value != null) {
				_testItems = value;
			}
		}
	}

	public int NumberOfQuestions {get; set;}
	public bool IsReplayable {get; set;}
	public bool IsFaild {get; set;}

	public Test()
	{
		_correctlyAnsweredQuestions = 0;
		_testItems = new List<TestItem>();
		IsReplayable = false;
		IsFaild = false;
	}
}