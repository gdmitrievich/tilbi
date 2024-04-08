using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PCRenderer : MonoBehaviour
{
	private const float _TIME_TO_WAIT = 30;
	private TextMeshPro _pcText;
	private float _currentTime;
	private bool _timeElapsed;

	[SerializeField] private Material _pcOnMaterial;
	[SerializeField] private Material _pcOffMaterial;
	[SerializeField] private Material _pcReloadMaterial;
	[SerializeField] private Renderer _pcRenderer;
	[SerializeField] private Material[] _pcMaterials;

	private AudioSource _audioSource;
	private PCAudioController _pCAudioController;
	private const float _BEEP_VOLUME = 0.15F;

	public static event Action<GameObject> FailedTimerElapsed;
	private float _nextSecond;

	void Awake()
	{
		_pcText = GetComponentInChildren<TextMeshPro>();
		_currentTime = 0;
		_timeElapsed = true;

		ReloadMaterials(_pcOnMaterial);
		_pCAudioController = GetComponent<PCAudioController>();
		_audioSource = GetComponent<AudioSource>();
		_nextSecond = _TIME_TO_WAIT;
	}

	void ReloadMaterials(Material material) {
		_pcMaterials = new Material[_pcRenderer.materials.Length];
		_pcMaterials[0] = _pcRenderer.materials[0];
		_pcMaterials[1] = material;

		_pcRenderer.materials = _pcMaterials;
	}

	void Update()
	{
		if (_currentTime > 0)
		{
			if (_currentTime <= _nextSecond) {
				_audioSource.volume = _BEEP_VOLUME;
				_pCAudioController.PlayBeep();
				--_nextSecond;
			}
			_currentTime -= Time.deltaTime;
			_pcText.text = ((int)_currentTime).ToString();
		}
		else if (!_timeElapsed)
		{
			_audioSource.volume = 1;

			_timeElapsed = true;
			FailedTimerElapsed?.Invoke(gameObject);

			ReloadMaterials(_pcOnMaterial);
			_currentTime = 0;
			_pcText.text = "";
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

		ReloadMaterials(_pcOffMaterial);
		gameObject.GetComponent<PCRenderer>().enabled = false;
		_pcText.text = "";
	}

	private void OnTestFailed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		var test = obj.GetComponent<Test>();
		if (!test.IsReplayable || test.IsIncorrect)
		{
			_pcText.enabled = false;
			ReloadMaterials(_pcOffMaterial);
			return;
		}

		ReloadMaterials(_pcReloadMaterial);
		_currentTime = _TIME_TO_WAIT;
		_timeElapsed = false;
	}
}