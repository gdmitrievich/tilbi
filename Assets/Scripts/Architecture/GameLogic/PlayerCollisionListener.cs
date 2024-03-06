using System;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour
{
	public static event Action PlayerCatched;
	private bool _isTestFailed;

	void Awake() {
		_isTestFailed = false;
	}

	void OnEnable()
	{
		PCTestPassingLogic.TestFailed += OnTestFailed;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestFailed -= OnTestFailed;
	}

	private void OnTestFailed(GameObject obj)
	{
		_isTestFailed = true;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Tilbi") && _isTestFailed)
		{
			PlayerCatched?.Invoke();
		}
	}
}
