using UnityEngine;
using System;

public class ItemInteractionLogic : MonoBehaviour
{
	private Camera _mainCamera;
	private Vector3 _mousePosition;
	[SerializeField] private float _mousePositionZ = 3;
	[SerializeField] private LayerMask _interactableMask;

	public static event Action<GameObject> InteractableItemTouched;

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

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, _mousePositionZ, _interactableMask))
			{
				InteractableItemTouched?.Invoke(hit.transform.gameObject);
			}
		}
	}
}