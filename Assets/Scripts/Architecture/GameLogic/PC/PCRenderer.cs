using System;
using TMPro;
using UnityEngine;

public class PCRenderer : MonoBehaviour
{
	private TextMeshPro _pcText;
	private float _currentTime;

	private const string _DEFAULT_MESSAGE = "ПРОЙТИ";

	public static event Action FailedTimerElapsed;

	void Awake()
	{
		_pcText = GetComponentInChildren<TextMeshPro>();
		_pcText.text = _DEFAULT_MESSAGE;
		_currentTime = 0;
	}

	void Update()
	{
		if (_currentTime > 0)
		{
			_currentTime -= Time.deltaTime;
			_pcText.text = ((int)_currentTime).ToString();
		}
		else
		{
			FailedTimerElapsed?.Invoke();

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

	private void OnTestSuccessfullyPassed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		_pcText.text = "Passed";
		_pcText.color = Color.green;
		gameObject.GetComponent<PCRenderer>().enabled = false;
	}

	private void OnTestFailed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		_pcText.color = Color.red;
		_currentTime = 30;
	}
}