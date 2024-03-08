using System;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour
{
	public static event Action PlayerCatched;

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
		PlayerPrefs.SetInt("IsTilbiAngry", 1);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Tilbi") && PlayerPrefs.GetInt("IsTilbiAngry") != 0)
		{
			PlayerCatched?.Invoke();
		}
	}
}
