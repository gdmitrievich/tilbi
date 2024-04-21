using System;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour
{
	public static event Action PlayerCatched;
	private CameraMovementAnimation _playerCameraMovementAnimation;

	void Awake()
	{
		_playerCameraMovementAnimation = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CameraMovementAnimation>();
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
		JsonPlayerPrefs prefs = new JsonPlayerPrefs(Application.persistentDataPath + "/Preferences.json");
		if (prefs.GetInt("PassedTests") > 0)
		{
			prefs.SetInt("IsTilbiAngry", 1);
			prefs.Save();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		JsonPlayerPrefs prefs = new JsonPlayerPrefs(Application.persistentDataPath + "/Preferences.json");
		if (collider.gameObject.CompareTag("Tilbi") && prefs.GetInt("IsTilbiAngry") != 0)
		{
			_playerCameraMovementAnimation.enabled = true;
			_playerCameraMovementAnimation.IsMovingTo = true;
			PlayerCatched?.Invoke();

			PlayerKeyboardInteractionController.DisableInventorySystem();
			PlayerKeyboardInteractionController.DisableItemInteractionLogic();
			PlayerKeyboardInteractionController.DisableMovement();
			PlayerKeyboardInteractionController.DisableMouseLook();
		}
	}
}
