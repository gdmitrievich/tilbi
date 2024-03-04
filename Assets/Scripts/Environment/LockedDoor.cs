using UnityEngine;
public class LockedDoor : Door {
	public bool IsLocked {get; set;}

	void Awake() {
		IsLocked = true;
	}

	void OnEnable() {
		UITestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
	}

	void OnDisable() {
		UITestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
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