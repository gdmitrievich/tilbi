using System;
using TMPro;
using UnityEngine;

public class PCRenderer : MonoBehaviour
{
	private TextMeshPro _pcText;
	private float _currentTime;
	private bool _timeElapsed;

	private const string _DEFAULT_MESSAGE = "ПРОЙТИ";

	public static event Action<GameObject> FailedTimerElapsed;

	void Awake()
	{
		_pcText = GetComponentInChildren<TextMeshPro>();
		_pcText.text = _DEFAULT_MESSAGE;
		_currentTime = 0;
		_timeElapsed = true;
	}

	void Update()
	{
		if (_currentTime > 0)
		{
			_currentTime -= Time.deltaTime;
			_pcText.text = ((int)_currentTime).ToString();
		}
		else if (!_timeElapsed)
		{
			_timeElapsed = true;
			FailedTimerElapsed?.Invoke(gameObject);

			_pcText.text = _DEFAULT_MESSAGE;
			_pcText.color = Color.white;
			_currentTime = 0;
		}
	}

	void OnEnable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed += OnTestFailed;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed -= OnTestFailed;
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
		_timeElapsed = false;
	}
}