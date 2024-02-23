using Unity.VisualScripting;
using UnityEngine;

public class WetFloor : MonoBehaviour
{
	private void OnTriggerEnter(Collider collider)
	{
		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed = movable.BaseSpeed / 2;
			movable.BaseSpeed /= 2;
			movable.OnWetFloor = true;
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		IMovable movable = collider.gameObject.GetComponent<IMovable>();
		if (movable != null)
		{
			movable.Speed = movable.BaseSpeed * 2;
			movable.BaseSpeed *= 2;
			movable.OnWetFloor = false;
		}
	}
}
