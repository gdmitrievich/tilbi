using UnityEngine;

public class PickUpController : MonoBehaviour
{
	private DroppedItemBehaviour _droppedItemBehaviour;

	private Transform _rightHandPosition, _mainCamera;

	private Rigidbody _itemRb;
	[SerializeField] private BoxCollider _liftableCollider;
	[SerializeField] private Collider _disposableCollider;

	public float dropForwardForce, dropUpwardForce;

	void Awake()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		_rightHandPosition = player.transform.Find("RightHandItem").GetComponent<Transform>();
		_mainCamera = player.transform.Find("Main Camera").GetComponent<Transform>();

		_itemRb = GetComponent<Rigidbody>();

		_droppedItemBehaviour = GetComponent<DroppedItemBehaviour>();
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

		PickUp();
	}

	private void OnItemDropped(GameObject obj)
	{
		if (gameObject != obj)
		{
			return;
		}

		Drop();
	}

	private void PickUp()
	{
		_itemRb.isKinematic = true;

		transform.SetParent(_rightHandPosition);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.Euler(Vector3.zero);

		_liftableCollider.enabled = false;
		_disposableCollider.enabled = true;
	}

	private void Drop()
	{
		_itemRb.isKinematic = false;

		transform.SetParent(null);

		_itemRb.AddForce(_mainCamera.forward * dropForwardForce, ForceMode.Impulse);
		_itemRb.AddForce(_mainCamera.up * dropUpwardForce, ForceMode.Impulse);

		float random = Random.Range(-1f, 1f);
		_itemRb.AddTorque(new Vector3(random, random, random) * 10);

		_droppedItemBehaviour.enabled = true;
	}
}
