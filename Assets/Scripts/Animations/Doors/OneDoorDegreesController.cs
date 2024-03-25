using UnityEngine;

public class OneDoorDegreesController : DoorDegreesController, IInteractable
{
	[SerializeField] protected Transform _doorPivotTransform;
	private Quaternion _initialRotation;

	void Update()
	{
		if (_time >= _animationTime)
		{
			return;
		}
		_time += Time.deltaTime;

		if (_isOpen)
		{
			if (!_isDoorOpeningEventInvoked)
			{
				DoorOpening?.Invoke();
				_isDoorOpeningEventInvoked = true;
			}
			RotateDoor(_doorPivotTransform, _initialRotation, Quaternion.Euler(_doorPivotTransform.localRotation.x, _degrees, _doorPivotTransform.localRotation.z));
		}
		else
		{
			RotateDoor(_doorPivotTransform, _initialRotation, Quaternion.Euler(_doorPivotTransform.localRotation.x, 0, _doorPivotTransform.localRotation.z));
			if (_time >= _animationTime && !_isDoorClosedEventInvoked)
			{
				DoorClosed?.Invoke();
				_isDoorClosedEventInvoked = true;
			}
		}
	}

	public override void Interact(GameObject obj)
	{
		base.Interact(obj);

		_initialRotation = _doorPivotTransform.localRotation;
	}
}