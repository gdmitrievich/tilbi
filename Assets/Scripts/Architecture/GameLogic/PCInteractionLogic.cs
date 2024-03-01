using UnityEngine;

public class PCInteractionLogic : MonoBehaviour, IInteractable {
	[SerializeField] private UITestPassingLogic _uITestPassingLogic;

	public void Interact(GameObject obj) {
		_uITestPassingLogic.InitialSetup(obj.GetComponent<Test>());
	}
}