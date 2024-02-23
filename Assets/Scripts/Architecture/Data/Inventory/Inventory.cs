using System.Collections.Generic;
using System;
using UnityEngine;
public class Inventory
{
	private const int _MAX_ITEMS_COUNT = 5;
	private GameObject[] _items;
	private int _count;
	private int _selected;

	public int Count
	{
		get => _count;
	}
	public int MaxCount
	{
		get => _MAX_ITEMS_COUNT;
	}
	public int Selected
	{
		get => _selected;
		set
		{
			if (value >= 0 && value < _MAX_ITEMS_COUNT)
			{
				SelectedItemChanging?.Invoke(_items[_selected]);
				_selected = value;
				SelectedItemChanged?.Invoke(_items[_selected]);
			}
		}
	}

	public Inventory()
	{
		_items = new GameObject[5];
		_count = _selected = 0;
	}

	public GameObject this[int index]
	{
		get
		{
			if (index >= 0 && index < _MAX_ITEMS_COUNT)
			{
				return _items[index];
			}

			return null;
		}
	}

	public static event Action ItemRemoved;
	public static event Action<GameObject> ItemAdded;
	public static event Action<GameObject> SelectedItemChanging;
	public static event Action<GameObject> SelectedItemChanged;

	private bool isEmptyCell(int index)
	{
		return _items[index] == null;
	}

	public bool Add(GameObject item)
	{
		if (_count == _MAX_ITEMS_COUNT)
		{
			return false;
		}

		int i = 0;
		while (i < _MAX_ITEMS_COUNT && !isEmptyCell(i))
		{
			++i;
		}

		_items[i] = item;
		_count++;
		Selected = i;
		ItemAdded?.Invoke(_items[_selected]);
		return true;
	}

	public void Remove()
	{
		_items[_selected] = null;
		_count--;
		Selected = _selected;
		ItemRemoved?.Invoke();
	}

	// public GameObject Get()
	// {
	// 	if (!isEmptyCell(_selected))
	// 	{
	// 		ItemReceived?.Invoke(_items[_selected]);
	// 		return _items[_selected];
	// 	}

	// 	return null;
	// }
}