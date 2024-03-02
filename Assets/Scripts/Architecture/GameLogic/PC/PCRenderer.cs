using TMPro;
using UnityEngine;

public class PCRenderer : MonoBehaviour
{
	private TextMeshPro _pcText;
	private Test _test;
	private float _currentTime;

	private const string _DEFAULT_MESSAGE = "ПРОЙТИ";

	void Awake()
	{
		_pcText = GetComponentInChildren<TextMeshPro>();
		_test = GetComponent<Test>();
		_pcText.text = _DEFAULT_MESSAGE;
		_currentTime = 0;
	}

	void Update() {
		if (_currentTime > 0) {
			_currentTime -= Time.deltaTime;
			_pcText.text = ((int) _currentTime).ToString();
		} else {
			_test.IsLocked = false;
			_pcText.text = _DEFAULT_MESSAGE;
			_pcText.color = Color.white;
			_currentTime = 0;
		}
	}

	void OnEnable()
	{
		UITestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		UITestPassingLogic.TestFailed += OnTestFailed;
	}

	void OnDisable()
	{
		UITestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		UITestPassingLogic.TestFailed += OnTestFailed;
	}

	private void OnTestSuccessfullyPassed() {
		_pcText.text = "Passed";
		_pcText.color = Color.green;
	}

	private void OnTestFailed() {
		_pcText.color = Color.red;

		_currentTime = 30;
		_test.IsLocked = true;
	}
}