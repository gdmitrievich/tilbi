using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorAnimationEventsHandler : MonoBehaviour
{
	[SerializeField] private BoxCollider _boxCollider;

	public void OnDoorIsChanging()
	{
		_boxCollider.excludeLayers = LayerMask.NameToLayer("CharacterLayer");
		Debug.Log("Door is changing");
	}

	public void OnDoorChanged()
	{
		_boxCollider.includeLayers = LayerMask.NameToLayer("CharacterLayer");
		Debug.Log("Door is changed");
	}
}
