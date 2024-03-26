using UnityEngine;

public class TwoDoorDegreesController : DoorDegreesController
{
	[SerializeField] protected Transform _leftDoorPivotTransform;
	[SerializeField] protected Transform _rightDoorPivotTransform;
	private Quaternion _leftInitialRotation;
	private Quaternion _rightInitialRotation;

	void Update()
	{
		if (_time >= _animationTime)
		{
			return;
		}
		_time += Time.deltaTime;

		if (_isOpen)
		{
			if (!_isTheFirstFrameOfOpening)
			{
				_colliderController.SetWalkable();
				_isTheFirstFrameOfOpening = true;
			}
			RotateDoor(_leftDoorPivotTransform, _leftInitialRotation, Quaternion.Euler(_leftDoorPivotTransform.localRotation.x, _degrees, _leftDoorPivotTransform.localRotation.z));
			RotateDoor(_rightDoorPivotTransform, _rightInitialRotation, Quaternion.Euler(_rightDoorPivotTransform.localRotation.x, 180 - _degrees, _rightDoorPivotTransform.localRotation.z));
		}
		else
		{
			RotateDoor(_leftDoorPivotTransform, _leftInitialRotation, Quaternion.Euler(_leftDoorPivotTransform.localRotation.x, 0, _leftDoorPivotTransform.localRotation.z));
			RotateDoor(_rightDoorPivotTransform, _rightInitialRotation, Quaternion.Euler(_rightDoorPivotTransform.localRotation.x, 180, _rightDoorPivotTransform.localRotation.z));
			if (_time >= _animationTime && !_isTheFirstFrameOfClosedDoor)
			{
				_colliderController.SetUnWalkable();
				_isTheFirstFrameOfClosedDoor = true;
			}
		}
	}

	public override void Interact(GameObject obj)
	{
		base.Interact(obj);

		_leftInitialRotation = _leftDoorPivotTransform.localRotation;
		_rightInitialRotation = _rightDoorPivotTransform.localRotation;
	}
}