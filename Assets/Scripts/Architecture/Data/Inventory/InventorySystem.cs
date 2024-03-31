using UnityEngine;
using System;
using System.Collections;

public class InventorySystem : MonoBehaviour
{
	private Inventory _inventory;
	public Inventory Inventory
	{
		get => _inventory;
	}
	private int diff;

	public static event Action<GameObject> ItemUsed;
	public static event Action<GameObject> ItemDropped;

	private AudioSource _audioSource;

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
		if (item.CompareTag("Banana") || item.CompareTag("Protein") || item.CompareTag("CheetSheet"))
		{
			if (_inventory.Add(item))
			{
				Debug.Log($"{item.name} was added");
			}
			else
			{
				Debug.Log($"{item.name} WASN'T ADDED!");
			}
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
		CheckUserInputToDropSelectedItem();
		if (Input.GetMouseButtonDown(1) &&
		_inventory[_inventory.Selected] != null)
		{
			StartSelectedItemUsageAnimation();
		}
	}

	private void StartSelectedItemUsageAnimation()
	{
		if (_inventory[_inventory.Selected].CompareTag("Banana") ||
			_inventory[_inventory.Selected].CompareTag("Protein"))
		{
			EatingAnimation eatingAnimation = GameObject.Find("/Characters/Player/Main Camera/RightHandItem").GetComponentInChildren<EatingAnimation>();
			eatingAnimation.Eat();
			ItemAudioSourcesScript.PlayEatingSound(1f, 0.1f, 0.7f, 1.3f);
		}
		else if (_inventory[_inventory.Selected].CompareTag("CheetSheet"))
		{
			CheetSheetAnimation cheetSheetAnimation = GameObject.Find("/Characters/Player/Main Camera/RightHandItem").GetComponentInChildren<CheetSheetAnimation>();
			cheetSheetAnimation.Hide();
		}
	}

	public void UseSelectedItem()
	{
		ItemUsed?.Invoke(_inventory[_inventory.Selected]);
		Debug.Log($"Item {_inventory[_inventory.Selected].name} was used!");

		if (!_inventory[_inventory.Selected].CompareTag("CheetSheet"))
		{
			Destroy(_inventory[_inventory.Selected]);
			_inventory.Remove();
		}
	}

	private void CheckUserInputToDropSelectedItem()
	{
		if (Input.GetKey(KeyCode.H) && _inventory[_inventory.Selected] != null)
		{
			Debug.Log($"Item {_inventory[_inventory.Selected].name} was removed!");


			ItemDropped?.Invoke(_inventory[_inventory.Selected]);

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