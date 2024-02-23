using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEngine;

public class Inventory
{
	private const int _MAX_ITEMS_COUNT = 5;
	private List<Item> _items;
	private int _count;
	private int _selected;
	public int Selected {
		get => _selected;
		set {
			if (value >= 0 && value < _MAX_ITEMS_COUNT) {
				_selected = value;
			}
		}
	}

	public static event Action<Item> ItemPulledOut;

	private bool isEmptyCell(int index)
	{
		return index >= _count;
	}

	public Inventory()
	{
		_items = new List<Item>();
		_count = 0;
		_selected = 2;
	}

	public int Count {
		get => _count;
	}

	public bool Add(Item item)
	{
		if (_items.Count < _MAX_ITEMS_COUNT)
		{
			_items.Add(item);
			_count++;
			foreach(var it in _items) {
				UnityEngine.Debug.Log($"Item name: {it.name}");
			}
			return true;
		}

		return false;
	}

	public Item Get()
	{
		if (!isEmptyCell(_selected))
		{
			Item item = _items[_selected];
			_items.RemoveAt(_selected);
			_count--;
			ItemPulledOut?.Invoke(item);
			return item;
		}

		return null;
	}
}