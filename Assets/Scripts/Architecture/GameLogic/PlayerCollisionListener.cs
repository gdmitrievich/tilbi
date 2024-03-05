using System;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour {
	public static event Action PlayerCatched;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Tilbi")) {
			PlayerCatched?.Invoke();
		}
	}
}
