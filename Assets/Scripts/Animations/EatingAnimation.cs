using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingAnimation : MonoBehaviour
{
	private Animator _animator;
	private InventorySystem _inventorySystem;
	public Animator Animator
	{
		get => _animator;
	}

	void Start()
	{
		_animator = GetComponent<Animator>();
		_inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
	}

	void OnDestroy()
	{
		PlayerKeyboardInteractionController.EnableInventorySystem();
		PlayerKeyboardInteractionController.EnableItemInteractionLogic();
	}

	public void Eat()
	{
		_animator.enabled = true;
		_animator.SetTrigger("Eat");

		PlayerKeyboardInteractionController.DisableInventorySystem();
		PlayerKeyboardInteractionController.DisableItemInteractionLogic();
	}

	public void ItemEated() {
		_inventorySystem.UseSelectedItem();
	}
}
