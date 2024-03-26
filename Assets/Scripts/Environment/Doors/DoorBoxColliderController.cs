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

	public void SetWalkable() {
		_boxCollider.isTrigger = true;
	}

	public void SetUnWalkable() {
		_boxCollider.isTrigger = false;
	}
}
