using UnityEngine;
public class LockedDoor : OneDoorDegreesController {
	public bool IsLocked {get; set;}

	void Awake() {
		IsLocked = true;

		if (_colliderController == null)
		{
			_colliderController = GetComponent<DoorBoxColliderController>();
		}

		if (_doorAudioController == null) {
			_doorAudioController = GetComponent<DoorAudioController>();
		}
	}

	void OnEnable() {
		PCTestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
	}

	void OnDisable() {
		PCTestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
	}

	private void OnTestSuccessfullyPassed(GameObject obj) {
		IsLocked = false;
	}

	public override void Interact(GameObject obj) {
		if (IsLocked) {
			(_doorAudioController as LockedDoorAudioController).PlayLockedDoorAudio();
			return;
		}

		base.Interact(obj);
	}
}