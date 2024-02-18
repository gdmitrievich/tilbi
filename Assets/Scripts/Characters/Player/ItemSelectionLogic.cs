using UnityEngine;
using System;

public class ItemSelectionLogic : MonoBehaviour
{
	private Camera _mainCamera;
	private Vector3 _mousePosition;
	[SerializeField] private float _mousePositionZ = 3;
	[SerializeField] private LayerMask _interactableMask;

	public static event Action<Item> ItemReceived;

	void Start()
	{
		_mainCamera = GetComponent<Camera>();
	}

	void Update()
	{
		RayCastLogic();
	}

	void RayCastLogic()
	{
		_mousePosition = Input.mousePosition;
		_mousePosition.z = _mousePositionZ;
		_mousePosition = _mainCamera.ScreenToWorldPoint(_mousePosition);
		// To watch rayCast
		// Debug.DrawRay(transform.position, _mousePosition - transform.position, Color.red);

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, _mousePositionZ, _interactableMask))
			{
				MakeActionWithItem(ref hit);
			}
		}
	}

	void MakeActionWithItem(ref RaycastHit hit)
	{
		if (hit.transform.gameObject.CompareTag("Banana") || hit.transform.gameObject.CompareTag("Protein"))
		{
			ItemReceived?.Invoke(hit.transform.gameObject.GetComponent<ItemDataStorage>().item as Boost);
		}
		else if (hit.transform.gameObject.CompareTag("CheetSheet"))
		{
			ItemReceived?.Invoke(hit.transform.gameObject.GetComponent<ItemDataStorage>().item as CheetSheet);
		}
	}
}