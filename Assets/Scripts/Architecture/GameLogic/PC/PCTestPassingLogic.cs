using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PCTestPassingLogic : MonoBehaviour
{
	private GameObject _pc;
	private Test _test;

	private int _currentTestNmb;
	private int _previousTestNmb;
	private List<int> _notAnsweredTestNumbers;

	public static event Action<GameObject> TestSuccessfullyPassed;
	public static event Action<GameObject> TestFailed;

	private UITestRenderer _uITestRenderer;
	private const int TO_PERSENTS = 100;

	public void Awake()
	{
		_uITestRenderer = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<UITestRenderer>();
	}

	void OnEnable()
	{
		PCInteractionListener.PcInteracted += OnPcInteracted;
	}

	void OnDisable()
	{
		PCInteractionListener.PcInteracted -= OnPcInteracted;
	}

	private void OnPcInteracted(GameObject obj)
	{
		_pc = obj;
		_test = _pc.GetComponent<Test>();
		_test.Reset();

		if (!_test.IsReplayable && _test.AttemptsToPassTest == 1)
		{
			return;
		}
		_test.AttemptsToPassTest += 1;

		StopGameLogic.StopGame();
		Cursor.lockState = CursorLockMode.None;
		_currentTestNmb = _previousTestNmb = 0;

		_notAnsweredTestNumbers = new List<int>();
		_notAnsweredTestNumbers = Enumerable.Range(0, _test.NumberOfQuestions).ToList();

		_uITestRenderer.InitialSetup();
		_uITestRenderer.LoadTestNumbers(_test.NumberOfQuestions);
		LoadTestItem(_currentTestNmb);
	}

	public IEnumerator TestPassed()
	{
		yield return new WaitForSeconds(0.5f);

		// Don't place this line down of if statement to prevent
		// 0-speed of Tilbi movement in the first scene
		StopGameLogic.ResumeGame();

		if (_test.IsSuccessfullyPassed())
		{
			PlayerPrefs.SetInt("PassedTests", PlayerPrefs.GetInt("PassedTests") + 1);
			TestSuccessfullyPassed?.Invoke(_pc);
			Debug.Log("Test Passed");
		}
		else
		{
			TestFailed?.Invoke(_pc);
			Debug.Log("Test faild");
		}

		Cursor.lockState = CursorLockMode.Locked;
		_uITestRenderer.TestPassed();
	}

	public void OnAcceptButtonClicked()
	{
		int correctlyAnswered = 0;
		_notAnsweredTestNumbers.Remove(_currentTestNmb);

		foreach (Transform tr in _uITestRenderer.AnswersParent.transform)
		{
			Toggle answerToggle = tr.GetComponent<Toggle>();

			if (!answerToggle.isOn)
			{
				continue;
			}

			if (_test.TestItems[_currentTestNmb].correctAnswers.Contains(Convert.ToInt32(answerToggle.name)))
			{
				_test.CorrectlyAnsweredQuestionAnswers += 1;
				correctlyAnswered += 1;
			}
			else if (!_uITestRenderer.AnswersParentToggleGroup.enabled)
			{
				_test.CorrectlyAnsweredQuestionAnswers -= 1;
				correctlyAnswered -= 1;
			}
		}

		_uITestRenderer.SetScoreText(Math.Clamp(Math.Round(Utility.GetPercentage(_test.CorrectlyAnsweredQuestionAnswers, _test.TotalNumberOfCorrectAnswersOfQuestions) * TO_PERSENTS), 0, 100).ToString() + " %");

		Transform selectedTestNumberTransform = _uITestRenderer.GetSelectedTestNumberTransform(_currentTestNmb);
		if (selectedTestNumberTransform != null)
		{
			Button selectedTestNumber = selectedTestNumberTransform.gameObject.GetComponent<Button>();
			Image selectedTestNumberImage = selectedTestNumber.GetComponent<Image>();

			if (correctlyAnswered == _test.TestItems[_currentTestNmb].correctAnswers.Count)
			{
				_uITestRenderer.SetImageColor(selectedTestNumberImage, Color.green);
				_uITestRenderer.PreviousBtnColor = selectedTestNumberImage.color;
			}
			else if (correctlyAnswered > 0)
			{
				_uITestRenderer.SetImageColor(selectedTestNumberImage, Color.yellow);
				_uITestRenderer.PreviousBtnColor = selectedTestNumberImage.color;
			}
			else if (correctlyAnswered <= 0)
			{
				_uITestRenderer.SetImageColor(selectedTestNumberImage, Color.red);
				_uITestRenderer.PreviousBtnColor = selectedTestNumberImage.color;
			}
			else
			{
				_uITestRenderer.PreviousBtnColor = Color.white;
			}

			selectedTestNumber.interactable = false;
		}
		else
		{
			Debug.Log($"Can't find {_currentTestNmb} button.");
		}

		if (_notAnsweredTestNumbers.Count != 0)
		{
			LoadTestItem(_notAnsweredTestNumbers[0]);
		}
		else
		{
			StartCoroutine(TestPassed());
		}
	}

	public void LoadTestItem(int testNmb)
	{
		_previousTestNmb = _currentTestNmb;

		_uITestRenderer.DestroyAnswersParent();
		_uITestRenderer.SetQuestionText($"Вопрос ({testNmb + 1}) - " + _test.TestItems[testNmb].question);
		_uITestRenderer.ChangeNumberColor(_previousTestNmb, testNmb);

		if (_test.TestItems[testNmb].correctAnswers.Count == 1)
		{
			_uITestRenderer.SetRadioButtonAnswers(_test.TestItems[testNmb]);
		}
		else
		{
			_uITestRenderer.SetCheckBoxAnswers(_test.TestItems[testNmb]);
		}

		_currentTestNmb = testNmb;
	}
}
