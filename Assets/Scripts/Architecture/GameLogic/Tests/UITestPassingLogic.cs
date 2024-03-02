using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UITestPassingLogic : MonoBehaviour
{
	private bool _isPlayedOnce;

	private int _currentTestNmb;
	private int _previousTestNmb;
	private Color _previousBtnColor;
	private Test _test;
	private List<int> _notAnsweredTestNumbers;

	private Transform _testCanvas;
	private TextMeshProUGUI _questionText;
	private GameObject _answersParent;
	private ToggleGroup _answersParentToggleGroup;
	private GameObject _testNumbersParent;

	[SerializeField] private GameObject _answerPrefab;
	[SerializeField] private GameObject _testNumberPrefab;

	private TextMeshProUGUI _scoreText;
	private Button _acceptBtn;

	private const int TO_PERSENTS = 100;

	public void InitialSetup(Test test)
	{
		if (!test.IsReplayable && _isPlayedOnce) {
			return;
		}
		_isPlayedOnce = true;

		StopGameLogic.StopGame();

		_test = test;
		_test.Reset();

		LoadUITestGameObjects();
		_testCanvas.gameObject.SetActive(true);
		_scoreText.text = "0 %";

		Cursor.lockState = CursorLockMode.None;

		_currentTestNmb = _previousTestNmb = 0;

		_answersParentToggleGroup = _answersParent.GetComponent<ToggleGroup>();

		if (_testNumbersParent.transform.childCount > 0)
		{
			DestroyChildrens(_testNumbersParent.transform);
		}

		_previousBtnColor = Color.cyan;

		LoadTestNumbers();
		LoadTestItem(_currentTestNmb);

		_acceptBtn.onClick.AddListener(OnAcceptButtonClicked);

		_notAnsweredTestNumbers = new List<int>();
		_notAnsweredTestNumbers = Enumerable.Range(0, _test.NumberOfQuestions).ToList();
	}

	private void LoadUITestGameObjects() {
		const string COMMON_PATH = "Test/Background/Main Panel/";

		GameObject uiObj = GameObject.Find("/UI");
		_testCanvas = uiObj.transform.Find("Test").GetComponent<Transform>();
		_questionText = uiObj.transform.Find(COMMON_PATH + "Header Panel/Question Text").gameObject.GetComponent<TextMeshProUGUI>();
		_answersParent = uiObj.transform.Find(COMMON_PATH + "Body Panel/Answers Panel/Answers").gameObject;
		_testNumbersParent = uiObj.transform.Find(COMMON_PATH + "Footer Panel/Test Numbers Panel/Test Numbers").gameObject;
		_scoreText = uiObj.transform.Find(COMMON_PATH + "Body Panel/Right Panel/Score Text").gameObject.GetComponent<TextMeshProUGUI>();
		_acceptBtn = uiObj.transform.Find(COMMON_PATH + "Footer Panel/Accept Button Panel/Accept Button").gameObject.GetComponent<Button>();
	}

	public IEnumerator TestPassed()
	{
		yield return new WaitForSeconds(0.5f);

		Cursor.lockState = CursorLockMode.Locked;
		_testCanvas.gameObject.SetActive(false);
		_acceptBtn.onClick.RemoveListener(OnAcceptButtonClicked);

		StopGameLogic.ResumeGame();
	}

	public void OnAcceptButtonClicked()
	{
		int correctlyAnswered = 0;
		_notAnsweredTestNumbers.Remove(_currentTestNmb);

		foreach (Transform tr in _answersParent.transform)
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
			} else if (!_answersParentToggleGroup.enabled) {
				_test.CorrectlyAnsweredQuestionAnswers -= 1;
				correctlyAnswered -= 1;
			}
		}

		_scoreText.text = Math.Clamp(Math.Round(Utility.GetPercentage(_test.CorrectlyAnsweredQuestionAnswers, _test.TotalNumberOfCorrectAnswersOfQuestions) * TO_PERSENTS), 0, 100).ToString() + " %";

		Transform selectedTestNumberTransform = _testNumbersParent.transform.Find(_currentTestNmb.ToString());
		if (selectedTestNumberTransform != null)
		{
			Button selectedTestNumber = selectedTestNumberTransform.gameObject.GetComponent<Button>();
			Image selectedTestNumberImage = selectedTestNumber.GetComponent<Image>();

			if (correctlyAnswered == _test.TestItems[_currentTestNmb].correctAnswers.Count)
			{
				SetImageColor(selectedTestNumberImage, Color.green);
				_previousBtnColor = selectedTestNumberImage.color;
			}
			else if (correctlyAnswered > 0)
			{
				SetImageColor(selectedTestNumberImage, Color.yellow);
				_previousBtnColor = selectedTestNumberImage.color;
			}
			else if (correctlyAnswered <= 0)
			{
				SetImageColor(selectedTestNumberImage, Color.red);
				_previousBtnColor = selectedTestNumberImage.color;
			} else {
				_previousBtnColor = Color.white;
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

	private void SetImageColor(Image image, in Color color)
	{
		image.color = color;
	}

	private void LoadTestItem(int testNmb)
	{
		_previousTestNmb = _currentTestNmb;

		_questionText.text = $"Вопрос ({testNmb + 1}) - " + _test.TestItems[testNmb].question;

		if (_answersParent.transform.childCount > 0)
		{
			DestroyChildrens(_answersParent.transform);
		}

		if (_test.TestItems[testNmb].correctAnswers.Count == 1)
		{
			SetRadioButtonAnswers(_test.TestItems[testNmb]);
		}
		else
		{
			SetCheckBoxAnswers(_test.TestItems[testNmb]);
		}

		SetImageColor(_testNumbersParent.transform.Find(_previousTestNmb.ToString()).gameObject.GetComponent<Image>(), _previousBtnColor);
		Image selectedTestNumberImage = _testNumbersParent.transform.Find(testNmb.ToString())?.gameObject.GetComponent<Image>();
		if (selectedTestNumberImage != null)
		{
			SetImageColor(selectedTestNumberImage, Color.cyan);
		}

		_currentTestNmb = testNmb;

		_previousBtnColor = Color.white;
	}

	private void DestroyChildrens(Transform transform)
	{
		foreach (Transform tr in transform)
		{
			Destroy(tr.gameObject);
		}
	}

	private void SetRadioButtonAnswers(in Test.TestItem testItem)
	{
		_answersParentToggleGroup.enabled = true;

		for (int i = 0; i < testItem.answers.Count; ++i)
		{
			GameObject answerPrefab = Instantiate(_answerPrefab);
			answerPrefab.transform.SetParent(_answersParent.transform, false);
			answerPrefab.name = i.ToString();

			Toggle answerPrefabToggle = answerPrefab.GetComponent<Toggle>();
			answerPrefabToggle.group = _answersParentToggleGroup;
			answerPrefabToggle.isOn = false;

			answerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = testItem.answers[i];
		}
	}

	private void SetCheckBoxAnswers(in Test.TestItem testItem)
	{
		_answersParentToggleGroup.enabled = false;

		for (int i = 0; i < testItem.answers.Count; ++i)
		{
			GameObject answerPrefab = Instantiate(_answerPrefab, _answersParent.transform, false);
			answerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = testItem.answers[i];
			answerPrefab.name = i.ToString();
		}
	}

	private void LoadTestNumbers()
	{
		for (int i = 0; i < _test.NumberOfQuestions; ++i)
		{
			GameObject testNumberPrefab = Instantiate(_testNumberPrefab);
			testNumberPrefab.transform.SetParent(_testNumbersParent.transform, false);
			testNumberPrefab.name = i.ToString();

			Button testNumberBtn = testNumberPrefab.GetComponentInChildren<Button>();
			testNumberBtn.onClick.AddListener(() => LoadTestItem(Convert.ToInt32(testNumberPrefab.name)));

			testNumberPrefab.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
		}
	}
}
