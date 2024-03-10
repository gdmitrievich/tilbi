using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheetSheetAnimation : MonoBehaviour
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

	public void Show()
	{
		_animator.enabled = true;
		_animator.SetBool("IsHidden", false);
	}

	public void Hide()
	{
		_animator.enabled = true;
		_animator.SetBool("IsHidden", true);

		PlayerKeyboardInteractionController.DisableInventorySystem();
		PlayerKeyboardInteractionController.DisableItemInteractionLogic();
	}

	public void Hidden()
	{
		_animator.enabled = false;
		_inventorySystem.UseSelectedItem();
	}

	public void Shown() {
		_animator.enabled = false;
		PlayerKeyboardInteractionController.EnableInventorySystem();
		PlayerKeyboardInteractionController.EnableItemInteractionLogic();
	}
}
