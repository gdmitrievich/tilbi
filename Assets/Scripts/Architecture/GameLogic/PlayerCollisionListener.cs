using System;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour
{
	public static event Action PlayerCatched;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Tilbi") && PlayerPrefs.GetInt("PassedTests") >= 1)
		{
			PlayerCatched?.Invoke();
		}
	}
}
