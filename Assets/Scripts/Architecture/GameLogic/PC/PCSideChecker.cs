using System;
using UnityEngine;

public class PCSideChecker : MonoBehaviour
{
	public static bool IsOnSideCheckTrigger;

	void Awake() {
		IsOnSideCheckTrigger = false;
	}

	private void OnTriggerEnter()
	{
		IsOnSideCheckTrigger = true;
	}

	private void OnTriggerExit()
	{
		IsOnSideCheckTrigger = false;
	}
}