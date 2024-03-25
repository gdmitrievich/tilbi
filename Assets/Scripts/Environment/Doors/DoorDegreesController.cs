using System;
using UnityEngine;

public abstract class DoorDegreesController : MonoBehaviour, IInteractable
{
	[SerializeField] protected Transform _doorPivotTransform;
	[SerializeField] protected float _degrees;
	[SerializeField] protected float _animationTime;
	protected float _time;

	public static Action DoorOpening;
	public static Action DoorClosed;
	protected bool _isDoorOpeningEventInvoked;
	protected bool _isDoorClosedEventInvoked;

	protected bool _isOpen;

	public bool IsOpen
	{
		get => _isOpen;
	}

	void Awake()
	{
		_isDoorOpeningEventInvoked = false;
		_isDoorClosedEventInvoked = false;
		_isOpen = false;
		_time = 0;
	}

	public virtual void Interact(GameObject obj)
	{
		_isDoorOpeningEventInvoked = false;
		_isDoorClosedEventInvoked = false;
		_isOpen = !_isOpen;
		_time = 0;
	}
}