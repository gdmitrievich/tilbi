using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorAnimationEventsHandler : MonoBehaviour
{
	[SerializeField] private BoxCollider _boxCollider;

	public void OnDoorIsOpening()
	{
		_boxCollider.isTrigger = true;
	}

	public void OnDoorIsClosed()
	{
		_boxCollider.isTrigger = false;
	}
}
