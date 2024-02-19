
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerLogic : MonoBehaviour
{
	[SerializeField] private PlayerMovement _playerMovement;
	private List<string> _hints;

	void Awake() {
		_playerMovement = GetComponent<PlayerMovement>();
	}

	void OnEnable()
	{
		Inventory.ItemPulledOut += OnItemPulledOut;
	}
	void OnDisable()
	{
		Inventory.ItemPulledOut -= OnItemPulledOut;
	}

	private void OnItemPulledOut(Item item)
	{
		if (item is Boost boost)
		{
			_playerMovement.Energy = Math.Clamp(_playerMovement.Energy + boost.energy, 0, _playerMovement.ENERGY_LIMIT);
		}
		else if (item is CheetSheet cheetSheet)
		{
			_hints = cheetSheet.hints;
		}
	}
}