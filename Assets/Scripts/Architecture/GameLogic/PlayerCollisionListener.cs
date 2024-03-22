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
		PlayerPrefs.SetInt("IsTilbiAngry", 1);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag("Tilbi") && PlayerPrefs.GetInt("IsTilbiAngry") != 0)
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
