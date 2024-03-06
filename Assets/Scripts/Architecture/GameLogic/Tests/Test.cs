using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
	public struct TestItem
	{
		public string question;
		public List<string> answers;
		public List<int> correctAnswers;

		public TestItem(string question = null)
		{
			this.question = question;
			answers = new List<string>();
			correctAnswers = new List<int>();
		}
	}

	void Awake() {
		IsReplayable = gameObject.CompareTag("UnreplayablePC") ? false : true;
		IsIncorrect = gameObject.CompareTag("IncorrectPC") ? true : false;
	}

	private const int _MIN_PERSENT_FOR_SUCCESS = 60;

	private List<TestItem> _testItems;
	public List<TestItem> TestItems
	{
		get => _testItems;
		set
		{
			if (value != null)
			{
				_testItems = value;
			}
		}
	}

	public int CorrectlyAnsweredQuestionAnswers { get; set; }
	public int TotalNumberOfCorrectAnswersOfQuestions { get; set; }

	public int NumberOfQuestions { get; set; }
	public bool IsReplayable { get; set; }
	public bool IsIncorrect { get; set; }

	public Test()
	{
		CorrectlyAnsweredQuestionAnswers = 0;
		TotalNumberOfCorrectAnswersOfQuestions = 0;

		_testItems = new List<TestItem>();
	}

	public void Reset()
	{
		CorrectlyAnsweredQuestionAnswers = 0;
	}

	public bool IsSuccessfullyPassed() {
		return Math.Round(Utility.GetPercentage(CorrectlyAnsweredQuestionAnswers, TotalNumberOfCorrectAnswersOfQuestions) * 100) >= _MIN_PERSENT_FOR_SUCCESS;
	}
}