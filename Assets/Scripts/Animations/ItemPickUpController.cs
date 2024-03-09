using UnityEngine;

public class PickUpController : MonoBehaviour
{
	private Transform _rightHandPosition, _mainCamera;

	public float dropForwardForce, dropUpwardForce;

	private Rigidbody _itemRb;
	private Collider _itemCollider;

	void Awake()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		_rightHandPosition = player.transform.Find("RightHandItem").GetComponent<Transform>();
		_mainCamera = player.transform.Find("Main Camera").GetComponent<Transform>();

		_itemRb = GetComponent<Rigidbody>();
		_itemCollider = GetComponent<Collider>();
	}

	void OnEnable()
	{
		ItemInteractionLogic.InteractableItemTouched += OnInteractableItemTouched;
		InventorySystem.ItemDropped += OnItemDropped;
	}

	void OnDisable()
	{
		ItemInteractionLogic.InteractableItemTouched -= OnInteractableItemTouched;
		InventorySystem.ItemDropped -= OnItemDropped;
	}

	private void OnInteractableItemTouched(GameObject obj)
	{
		if (gameObject != obj)
		{
			return;
		}

		_itemRb.isKinematic = true;
		_itemCollider.isTrigger = true;

		PickUp();
	}

	private void OnItemDropped(GameObject obj)
	{
		if (gameObject != obj)
		{
			return;
		}

		_itemRb.isKinematic = false;
		_itemCollider.isTrigger = false;

		Drop();
	}

	private void PickUp()
	{
		transform.SetParent(_rightHandPosition);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	private void Drop()
	{
		transform.SetParent(null);

		_itemRb.AddForce(_mainCamera.forward * dropForwardForce, ForceMode.Impulse);
		_itemRb.AddForce(_mainCamera.up * dropUpwardForce, ForceMode.Impulse);

		float random = Random.Range(-1f, 1f);
		_itemRb.AddTorque(new Vector3(random, random, random) * 10);
	}
}
