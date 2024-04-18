using UnityEngine;

public static class PlayerKeyboardInteractionController
{
	private static PlayerMovement _playerMovement;
	private static InventorySystem _inventorySystem;
	private static ItemInteractionLogic _itemInteractionLogic;
	private static MouseLook _mouseLook;

	public static void Load()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			_playerMovement = player.GetComponent<PlayerMovement>();
			_inventorySystem = player.GetComponent<InventorySystem>();
			_itemInteractionLogic = player.GetComponentInChildren<ItemInteractionLogic>();
			_mouseLook = player.GetComponentInChildren<MouseLook>();
		}
	}

	public static void EnableMovement()
	{
		if (_playerMovement != null)
			_playerMovement.enabled = true;
	}
	public static void DisableMovement()
	{
		if (_playerMovement != null)
			_playerMovement.enabled = false;
	}

	public static void EnableInventorySystem()
	{
		if (_inventorySystem != null)
			_inventorySystem.enabled = true;
	}
	public static void DisableInventorySystem()
	{
		if (_inventorySystem != null)
			_inventorySystem.enabled = false;
	}

	public static void EnableItemInteractionLogic()
	{
		if (_itemInteractionLogic != null)
			_itemInteractionLogic.enabled = true;
	}
	public static void DisableItemInteractionLogic()
	{
		if (_itemInteractionLogic != null)
			_itemInteractionLogic.enabled = false;
	}

	public static void EnableMouseLook()
	{
		if (_mouseLook != null)
			_mouseLook.enabled = true;
	}
	public static void DisableMouseLook()
	{
		if (_mouseLook != null)
			_mouseLook.enabled = false;
	}
}