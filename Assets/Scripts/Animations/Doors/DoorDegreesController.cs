using System;
using UnityEngine;

public abstract class DoorDegreesController : MonoBehaviour, IInteractable
{
	[SerializeField] protected float _degrees;
	[SerializeField] protected float _animationTime;
	protected float _time;

	protected bool _isTheFirstFrameOfOpening;
	protected bool _isTheFirstFrameOfClosedDoor;
	protected DoorBoxColliderController _colliderController;

	protected bool _isOpen;

	public bool IsOpen
	{
		get => _isOpen;
	}

	void Awake()
	{
		_isTheFirstFrameOfOpening = false;
		_isTheFirstFrameOfClosedDoor = false;
		_isOpen = false;
		_time = 0;

		_colliderController = GetComponent<DoorBoxColliderController>();
	}

	protected virtual void RotateDoor(Transform localTransform, Quaternion init, Quaternion target) => localTransform.localRotation = Quaternion.Lerp(init, target, _time / _animationTime);

	public virtual void Interact(GameObject obj)
	{
		_isTheFirstFrameOfOpening = false;
		_isTheFirstFrameOfClosedDoor = false;
		_isOpen = !_isOpen;
		_time = 0;
	}
}