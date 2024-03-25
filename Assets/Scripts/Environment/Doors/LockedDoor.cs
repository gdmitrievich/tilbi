using UnityEngine;
public class LockedDoor : OneDoorDegreesController {
	public bool IsLocked {get; set;}

	void Awake() {
		IsLocked = true;
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
			return;
		}

		base.Interact(obj);
	}
}