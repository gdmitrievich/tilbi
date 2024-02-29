using Unity.VisualScripting;
using UnityEngine;
using System;

public class WetFloor : MonoBehaviour
{
	public static event Action<GameObject> OnWetFloor;
	public static event Action<GameObject> OutOfWetFloor;
	private void OnTriggerEnter(Collider collider)
	{
		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed /= 2;
			OnWetFloor?.Invoke(collider.gameObject);
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed *= 2;
			OutOfWetFloor?.Invoke(collider.gameObject);
		}
	}
}
