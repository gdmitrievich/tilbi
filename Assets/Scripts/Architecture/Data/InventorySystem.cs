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
		ItemSelectionLogic.ItemReceived += OnItemReceived;
	}

	void OnDisable()
	{
		ItemSelectionLogic.ItemReceived -= OnItemReceived;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.H))
		{
			Debug.Log($"Count of inventory items: {_inventory.Count}");
		}
	}

	private void OnItemReceived(Item item)
	{
		_inventory.Add(item);
		Debug.Log($"Item {item.name} was added! Count of items in inventory: {_inventory.Count}");
	}
}