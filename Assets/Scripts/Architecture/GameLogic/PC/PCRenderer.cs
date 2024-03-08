using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PCRenderer : MonoBehaviour
{
	private TextMeshPro _pcText;
	private float _currentTime;
	private bool _timeElapsed;

	[SerializeField] private Material _pcOnMaterial;
	[SerializeField] private Material _pcOffMaterial;
	[SerializeField] private Material _pcReloadMaterial;
	[SerializeField] private Renderer _pcRenderer;
	[SerializeField] private Material[] _pcMaterials;

	public static event Action<GameObject> FailedTimerElapsed;

	void Awake()
	{
		_pcText = GetComponentInChildren<TextMeshPro>();
		_currentTime = 0;
		_timeElapsed = true;

		ReloadMaterials(_pcOnMaterial);
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
			_currentTime -= Time.deltaTime;
			_pcText.text = ((int)_currentTime).ToString();
		}
		else if (!_timeElapsed)
		{
			_timeElapsed = true;
			FailedTimerElapsed?.Invoke(gameObject);

			ReloadMaterials(_pcOnMaterial);
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

		ReloadMaterials(_pcOffMaterial);
		gameObject.GetComponent<PCRenderer>().enabled = false;
	}

	private void OnTestFailed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		if (!obj.GetComponent<Test>().IsReplayable)
		{
			_pcText.enabled = false;
			return;
		}

		ReloadMaterials(_pcReloadMaterial);
		_currentTime = 30;
		_timeElapsed = false;
	}
}