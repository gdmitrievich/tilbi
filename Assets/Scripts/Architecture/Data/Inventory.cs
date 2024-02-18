using System.Collections.Generic;

public class Inventory
{
	private const int _MAX_ITEMS_COUNT = 5;
	private List<Item> _items;
	private int _count;

	private bool isEmptyCell(int index)
	{
		return index >= _count;
	}

	public Inventory()
	{
		_items = new List<Item>();
		_count = 0;
	}

	public int Count {get;}

	public bool Add(ref Item item)
	{
		if (_items.Count < _MAX_ITEMS_COUNT)
		{
			_items.Add(item);
			_count++;
			return true;
		}

		return false;
	}

	public Item Get(int index)
	{
		if (index >= 0 && index < _MAX_ITEMS_COUNT && !isEmptyCell(index))
		{
			Item item = _items[index];
			_items.RemoveAt(index);
			_count--;
			return item;
		}

		return null;
	}
}