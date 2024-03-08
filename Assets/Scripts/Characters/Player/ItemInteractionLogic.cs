using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Dependencies.Sqlite;

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

		Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * _mousePositionZ, Color.red);
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, _mousePositionZ))
			{
				if (EventSystem.current.IsPointerOverGameObject())
					return;

				IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
				if (interactable != null)
				{
					interactable.Interact(hit.transform.gameObject);
				}

				if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
				{
					InteractableItemTouched?.Invoke(hit.transform.gameObject);
				}
			}
		}
	}
}