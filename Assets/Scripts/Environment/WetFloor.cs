using Unity.VisualScripting;
using UnityEngine;
using System;

public class WetFloor : MonoBehaviour
{
	public static event Action<GameObject> OnWetFloor;
	public static event Action<GameObject> OutOfWetFloor;

	private bool _isCollidingOnce;
	private bool _isCollidedOnce;

	void Update()
	{
		_isCollidingOnce = false;
		_isCollidedOnce = false;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (_isCollidingOnce) return;
		_isCollidingOnce = true;

		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed /= 2;
			OnWetFloor?.Invoke(collider.gameObject);
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (_isCollidedOnce) return;
		_isCollidedOnce = true;

		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed *= 2;
			OutOfWetFloor?.Invoke(collider.gameObject);
		}
	}
}
