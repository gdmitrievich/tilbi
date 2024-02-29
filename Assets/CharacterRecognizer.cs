using UnityEngine;

public class CharacterRecognizer : MonoBehaviour
{
	[SerializeField] private Door _door;
	public void OnTriggerEnter(Collider collider)
	{
		// Debug.Log($"Character {collider.gameObject.name} open the door.");
		MakeCharacterInteractionWithDoor(collider, !_door.IsOpen);
	}

	public void OnTriggerExit(Collider collider)
	{
		// Debug.Log($"Character {collider.gameObject.name} close the door.");
		MakeCharacterInteractionWithDoor(collider, _door.IsOpen);
	}

	private void MakeCharacterInteractionWithDoor(Collider collider, bool canInteract) {
		if (collider.gameObject.layer == LayerMask.NameToLayer("CharacterLayer") && !collider.gameObject.CompareTag("Player"))
		{
			if (canInteract)
			{
				_door.Interact(collider.gameObject);
			}
		}
	}
}
