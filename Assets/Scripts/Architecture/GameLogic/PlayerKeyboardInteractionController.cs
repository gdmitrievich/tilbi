using Unity.VisualScripting.Dependencies.Sqlite;
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
		_playerMovement = player.GetComponent<PlayerMovement>();
		_inventorySystem = player.GetComponent<InventorySystem>();
		_itemInteractionLogic = player.GetComponentInChildren<ItemInteractionLogic>();
		_mouseLook = player.GetComponentInChildren<MouseLook>();
	}

	public static void EnableMovement()
	{
		_playerMovement.enabled = true;
	}
	public static void DisableMovement()
	{
		_playerMovement.enabled = false;
	}

	public static void EnableInventorySystem()
	{
		_inventorySystem.enabled = true;
	}
	public static void DisableInventorySystem()
	{
		_inventorySystem.enabled = false;
	}

	public static void EnableItemInteractionLogic()
	{
		_itemInteractionLogic.enabled = true;
	}
	public static void DisableItemInteractionLogic()
	{
		_itemInteractionLogic.enabled = false;
	}

	public static void EnableMouseLook()
	{
		_mouseLook.enabled = true;
	}
	public static void DisableMouseLook()
	{
		_mouseLook.enabled = false;
	}
}