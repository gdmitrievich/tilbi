using UnityEngine;

public class TestPanelAnimation : MonoBehaviour
{
	private RectTransform _testPanelRectTransform;
	// private Vector2 _hiddenOffsetMax;
	// private Vector2 _shownOffsetMax;

	[SerializeField] private float _time;
	private float _currentTime;
	private bool _scalingToShow;
	public bool ScalingToShow
	{
		get => _scalingToShow;
		set => _scalingToShow = value;
	}

	void OnEnable()
	{
		_testPanelRectTransform = GetComponent<RectTransform>();

		// _shownOffsetMax = Vector2.zero;
		// _hiddenOffsetMax = new Vector2(-_testPanelRectTransform.rect.width, -_testPanelRectTransform.rect.height);

		// _testPanelRectTransform.offsetMax = _hiddenOffsetMax;
		_testPanelRectTransform.localScale = Vector3.zero;

		_currentTime = 0;
	}

	void Update()
	{
		if (_scalingToShow)
		{
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void Show()
	{
		if (_currentTime < _time)
		{
			Debug.Log(_currentTime);
			_currentTime += Time.deltaTime;
			// _testPanelRectTransform.offsetMax = Vector2.Lerp(_hiddenOffsetMax, _shownOffsetMax, _currentTime / _time);
			_testPanelRectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _currentTime / _time);
			return;
		}

		_scalingToShow = false;
		_currentTime = 0;
		enabled = false;
	}

	private void Hide()
	{
		if (_currentTime < _time)
		{
			_currentTime += Time.deltaTime;
			// _testPanelRectTransform.offsetMax = Vector2.Lerp(_shownOffsetMax, _hiddenOffsetMax, _currentTime / _time);
			_testPanelRectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _currentTime / _time);
			return;
		}

		var pCTestPassingLogic = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<PCTestPassingLogic>();
		pCTestPassingLogic.TestPassed();

		_scalingToShow = true;
		_currentTime = 0;
		enabled = false;
	}
}