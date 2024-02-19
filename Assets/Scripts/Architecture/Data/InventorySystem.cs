using UnityEngine;

public class InventorySystem : MonoBehaviour
{
	private Inventory _inventory;

	void Awake()
	{
		_inventory = new Inventory();
	}

	void OnEnable()
	{
		ItemInteractionLogic.ItemReceived += OnItemReceived;
	}

	void OnDisable()
	{
		ItemInteractionLogic.ItemReceived -= OnItemReceived;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			_inventory.Selected = 0;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			_inventory.Selected = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			_inventory.Selected = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			_inventory.Selected = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			_inventory.Selected = 4;
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log($"Count of inventory items: {_inventory.Count}");
		}
		if (Input.GetMouseButtonDown(1))
		{
			_inventory.Get();
			Debug.Log($"Count of inventory items: {_inventory.Count}");
		}
	}

	private void OnItemReceived(Item item)
	{
		if (_inventory.Add(item)) {
			Debug.Log($"Item {item.name} was added! Count of items in inventory: {_inventory.Count}");
		} else {
			// Make some action to tell the player about of inventory emptiness.
			Debug.Log($"Item {item.name} didn't added!");
		}
	}
}