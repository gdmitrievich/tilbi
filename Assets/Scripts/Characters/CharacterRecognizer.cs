using UnityEngine;

public class CharacterRecognizer : MonoBehaviour
{
	[SerializeField] private DoorDegreesController _doorDegreesController;
	public void OnTriggerEnter(Collider collider)
	{
		// Debug.Log($"Character {collider.gameObject.name} open the door.");
		MakeCharacterInteractionWithDoor(collider, !_doorDegreesController.IsOpen);
	}

	public void OnTriggerExit(Collider collider)
	{
		// Debug.Log($"Character {collider.gameObject.name} close the door.");
		MakeCharacterInteractionWithDoor(collider, _doorDegreesController.IsOpen);
	}

	private void MakeCharacterInteractionWithDoor(Collider collider, bool canInteract) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("CharacterLayer") && !collider.gameObject.CompareTag("Player"))
		{
			if (canInteract)
			{
				_doorDegreesController.Interact(collider.gameObject);
			}
		}
	}
}
