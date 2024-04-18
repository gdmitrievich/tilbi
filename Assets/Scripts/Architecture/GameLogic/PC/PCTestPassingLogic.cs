using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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

	private TestPanelAnimation _testPanelAnimation;

	[SerializeField] private AudioMixerSnapshot _defaultSnapshot;
	[SerializeField] private AudioMixerSnapshot _passingTheTestSnapshot;

	public void Awake()
	{
		_uITestRenderer = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<UITestRenderer>();
		_testPanelAnimation = GameObject.Find("/UI").transform.Find("Test/Background").GetComponent<TestPanelAnimation>();
	}

	public void OnPcInteracted(GameObject obj)
	{
		_pc = obj;
		_test = _pc.GetComponent<Test>();
		_test.Reset();

		Cursor.lockState = CursorLockMode.None;
		_currentTestNmb = _previousTestNmb = 0;

		_notAnsweredTestNumbers = new List<int>();
		_notAnsweredTestNumbers = Enumerable.Range(0, _test.NumberOfQuestions).ToList();

		_uITestRenderer.InitialSetup();
		_uITestRenderer.LoadTestNumbers(_test.NumberOfQuestions);
		LoadTestItem(_currentTestNmb);

		_testPanelAnimation.enabled = true;
		_testPanelAnimation.ScalingToShow = true;

		_passingTheTestSnapshot.TransitionTo(1.5f);
	}

	public void TestPassed()
	{
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

		var cameraMovementAnimation = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraMovementAnimation>();
		cameraMovementAnimation.enabled = true;
		cameraMovementAnimation.IsMovingTo = false;

		BgMusicManager.StopBgTestMusic(1.5f);
		_defaultSnapshot.TransitionTo(1.5f);
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

			// _test.TestItems[_currentTestNmb].correctAnswers.Count != 0 it's necessary for incorrect tests which contain 0 correct answers/
			if (correctlyAnswered == _test.TestItems[_currentTestNmb].correctAnswers.Count &&
			_test.TestItems[_currentTestNmb].correctAnswers.Count != 0)
			{
				_uITestRenderer.TilbiMoodIndex -= 1;
				// _uITestRenderer.SetImageColor(selectedTestNumberImage, Color.green);
				// _uITestRenderer.PreviousBtnColor = selectedTestNumberImage.color;
				SFXManager.UI.PlayCorrectBeep();
			}
			// else if (correctlyAnswered > 0)
			// {
			// 	_uITestRenderer.SetImageColor(selectedTestNumberImage, Color.yellow);
			// 	_uITestRenderer.PreviousBtnColor = selectedTestNumberImage.color;
			// }
			// else if (correctlyAnswered <= 0)
			// {
			// 	_uITestRenderer.SetImageColor(selectedTestNumberImage, Color.red);
			// 	_uITestRenderer.PreviousBtnColor = selectedTestNumberImage.color;
			// }
			else
			{
				_uITestRenderer.TilbiMoodIndex = _uITestRenderer.TilbiMoodIndex < 3 ? 3 : _uITestRenderer.TilbiMoodIndex + 1;
				_uITestRenderer.PreviousBtnColor = Color.white;
				SFXManager.UI.PlayIncorrectBeep();
			}

			_uITestRenderer.SetImageColor(selectedTestNumberImage, _uITestRenderer.AnsweredTestNumberColor);
			_uITestRenderer.ChangeNumberTextColor(_currentTestNmb, Color.white);
			_uITestRenderer.PreviousBtnColor = selectedTestNumberImage.color;

			selectedTestNumber.interactable = false;
		}
		else
		{
			Debug.Log($"Can't find {_currentTestNmb} button.");
		}

		_uITestRenderer.UpdateTilbiImage(_uITestRenderer.TilbiMoodIndex);

		if (_notAnsweredTestNumbers.Count != 0)
		{
			LoadTestItem(_notAnsweredTestNumbers[0]);
		}
		else
		{
			_testPanelAnimation.enabled = true;
			_testPanelAnimation.ScalingToShow = false;
			// TestPassed();
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
