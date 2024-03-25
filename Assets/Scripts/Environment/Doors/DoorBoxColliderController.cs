using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorBoxColliderController : MonoBehaviour
{
	private BoxCollider _boxCollider;

	void Awake()
	{
		_boxCollider = GetComponent<BoxCollider>();
	}

	void OnEnable()
	{
		DoorDegreesController.DoorOpening += OnDoorOpening;
		DoorDegreesController.DoorClosed += OnDoorClosed;
	}
	void OnDisable()
	{
		DoorDegreesController.DoorOpening -= OnDoorOpening;
		DoorDegreesController.DoorClosed -= OnDoorClosed;
	}

	private void OnDoorOpening() {
		_boxCollider.isTrigger = true;
	}

	private void OnDoorClosed() {
		_boxCollider.isTrigger = false;
	}
}
