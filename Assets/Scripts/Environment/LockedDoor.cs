using UnityEngine;
public class LockedDoor : Door {
	public bool IsLocked {get; set;}

	void Awake() {
		IsLocked = true;
	}

	void OnEnable() {
		UITestPassingLogic.TestFailed += OnTestFailed;
	}

	void OnDisable() {
		UITestPassingLogic.TestFailed -= OnTestFailed;
	}

	private void OnTestFailed(GameObject obj) {
		IsLocked = false;
	}

	public override void Interact(GameObject obj) {
		if (IsLocked) {
			return;
		}

		base.Interact(obj);
	}
}