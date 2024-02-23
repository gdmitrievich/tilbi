using Unity;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Data;
public class InventorySystem : MonoBehaviour
{
	[SerializeField] private Transform _rightHandPosition;
	[SerializeField] private Transform _bananasParentTransform;
	[SerializeField] private Transform _proteinsParentTransform;
	[SerializeField] private Transform _cheetSheetsParentTransform;
	[SerializeField] private Transform _placeForRemovedItem;
	private Inventory _inventory;
	private int diff;

	public static event Action<GameObject> ItemUsed;

	void Awake()
	{
		_inventory = new Inventory();
	}

	void OnEnable()
	{
		ItemInteractionLogic.InteractableItemTouched += OnInteractableItemTouched;
		Inventory.ItemRemoved += OnInventoryItemRemoved;
		Inventory.SelectedItemChanging += OnSelectedItemChanging;
		Inventory.SelectedItemChanged += OnSelectedItemChanged;
	}

	void OnDisable()
	{
		ItemInteractionLogic.InteractableItemTouched -= OnInteractableItemTouched;
		Inventory.ItemRemoved -= OnInventoryItemRemoved;
		Inventory.SelectedItemChanging -= OnSelectedItemChanging;
		Inventory.SelectedItemChanged -= OnSelectedItemChanged;
	}

	private void OnInteractableItemTouched(GameObject item)
	{
		if (_inventory.Add(item))
		{
			// RenderInventoryItem();
			item.gameObject.transform.position = _rightHandPosition.position;
			item.gameObject.transform.SetParent(_rightHandPosition);
			Debug.Log($"{item.name} was added");
		}
		else
		{
			// Make some action to tell the player about of inventory emptiness.
			Debug.Log($"{item.name} WASN'T ADDED!");
		}
	}

	private void OnInventoryItemRemoved()
	{
		// RenderRightHandItem(GameObject);
	}
	private void OnSelectedItemChanging(GameObject item)
	{
		if (item == null)
		{
			return;
		}

		item.SetActive(false);
	}
	private void OnSelectedItemChanged(GameObject item)
	{
		if (item == null)
		{
			return;
		}

		item.SetActive(true);
	}

	void Update()
	{
		CheckUserInputToChangeSelectedItem();
		CheckUserInputToRemoveSelectedItem();
		CheckUserInputToUseSelectedItem();

		if (Input.GetKeyDown(KeyCode.G))
		{
			for (int i = 0; i < _inventory.Count; ++i)
			{
				if (_inventory[i] == null) {
					Debug.Log($"{i}. Empty");
				} else {
					Debug.Log($"{i}. {_inventory[i].name}");
				}
			}
		}
	}

	private void CheckUserInputToUseSelectedItem()
	{
		if (Input.GetMouseButtonDown(1) &&
			_inventory[_inventory.Selected] != null &&
			!_inventory[_inventory.Selected].CompareTag("CheetSheet"))
		{
			Debug.Log($"Item {_inventory[_inventory.Selected].name} was used!");
			ItemUsed?.Invoke(_inventory[_inventory.Selected]);

			Destroy(_inventory[_inventory.Selected]);
			_inventory.Remove();
		}
	}

	private void CheckUserInputToRemoveSelectedItem()
	{
		if (Input.GetKey(KeyCode.H) && _inventory[_inventory.Selected] != null)
		{
			Debug.Log($"Item {_inventory[_inventory.Selected].name} was removed!");

			_inventory[_inventory.Selected].transform.position = _placeForRemovedItem.position;
			if (_inventory[_inventory.Selected].CompareTag("Banana"))
			{
				_inventory[_inventory.Selected].transform.SetParent(_bananasParentTransform);

			}
			else if (_inventory[_inventory.Selected].CompareTag("Protein"))
			{
				_inventory[_inventory.Selected].transform.SetParent(_proteinsParentTransform);
			}
			else if (_inventory[_inventory.Selected].CompareTag("CheetSheet"))
			{
				_inventory[_inventory.Selected].transform.SetParent(_cheetSheetsParentTransform);
			}

			_inventory.Remove();
		}
	}

	private void CheckUserInputToChangeSelectedItem()
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

		diff = Input.GetAxis("Mouse ScrollWheel") > 0 ? 1 : -1;

		if (Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if (_inventory.Selected + 1 == _inventory.MaxCount && diff > 0)
			{
				_inventory.Selected = 0;
			}
			else if (_inventory.Selected == 0 && diff < 0)
			{
				_inventory.Selected = _inventory.MaxCount - 1;
			}
			else if (diff > 0)
			{
				_inventory.Selected++;
			}
			else if (diff < 0)
			{
				_inventory.Selected--;
			}
		}
	}


	void RenderInventoryItem() { }
}