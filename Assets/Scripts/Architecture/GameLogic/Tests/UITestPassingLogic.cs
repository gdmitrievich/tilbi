using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITestPassingLogic : MonoBehaviour
{
	[SerializeField] private GameObject _testCanvas;
	private Test _test;

	[SerializeField] private TextMeshProUGUI _questionText;
	[SerializeField] private GameObject _answerParent;
	[SerializeField] private GameObject _testNumbersParent;
	private ToggleGroup _answerParentToggleGroup;

	[SerializeField] private GameObject _answerPrefab;
	[SerializeField] private GameObject _testNumberPrefab;

	public void InitialSetup(Test test)
	{
		Cursor.lockState = CursorLockMode.None;

		_answerParentToggleGroup = _answerParent.GetComponent<ToggleGroup>();
		_test = test;
		_testCanvas.SetActive(true);
		LoadTestItem(0);
		LoadTestNumbers();
	}

	public void TestPassed() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void LoadTestItem(int testNmb)
	{
		RemovePreviousTestItem();
		_questionText.text = $"Вопрос ({testNmb + 1}) - " + _test.TestItems[testNmb].question;

		if (_test.TestItems[testNmb].correctAnswers.Count == 1)
		{
			SetRadioButtonAnswers(_test.TestItems[testNmb]);
		}
		else
		{
			SetCheckBoxAnswers(_test.TestItems[testNmb]);
		}
	}

	private void RemovePreviousTestItem() {
		DestroyChildrens(_answerParent.transform);
	}

	private void DestroyChildrens(Transform transform) {
		foreach (Transform tr in transform) {
			Destroy(tr.gameObject);
		}
	}

	private void SetRadioButtonAnswers(in Test.TestItem testItem)
	{
		_answerParentToggleGroup.enabled = true;

		for (int i = 0; i < testItem.answers.Count; ++i)
		{
			GameObject answerPrefab = Instantiate(_answerPrefab);
			answerPrefab.transform.SetParent(_answerParent.transform, false);

			Toggle answerPrefabToggle = answerPrefab.GetComponent<Toggle>();
			answerPrefabToggle.group = _answerParentToggleGroup;
			answerPrefabToggle.isOn = false;

			answerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = testItem.answers[i];
		}
	}

	private void SetCheckBoxAnswers(in Test.TestItem testItem)
	{
		_answerParentToggleGroup.enabled = false;

		for (int i = 0; i < testItem.answers.Count; ++i)
		{
			GameObject answerPrefab = Instantiate(_answerPrefab, _answerParent.transform, false);
			answerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = testItem.answers[i];
		}
	}

	private void LoadTestNumbers()
	{
		for (int i = 0; i < _test.NumberOfQuestions; ++i) {
			GameObject testNumberPrefab = Instantiate(_testNumberPrefab);
			testNumberPrefab.transform.SetParent(_testNumbersParent.transform, false);
			testNumberPrefab.name = i.ToString();

			Button testNumberBtn = testNumberPrefab.GetComponentInChildren<Button>();
			testNumberBtn.onClick.AddListener(() => LoadTestItem(Convert.ToInt32(testNumberPrefab.name)));

			testNumberPrefab.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
		}
	}
}
