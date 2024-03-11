using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheetSheetAnimation : MonoBehaviour
{
	private Animator _animator;
	private InventorySystem _inventorySystem;
	private CheetSheetRenderer _cheetSheetRenderer;

	public Animator Animator
	{
		get => _animator;
	}

	void Start()
	{
		_animator = GetComponent<Animator>();
		_inventorySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
		_cheetSheetRenderer = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<CheetSheetRenderer>();
	}

	public void Show()
	{
		_animator.enabled = true;
		_animator.SetBool("IsHidden", false);

		PlayerKeyboardInteractionController.EnableInventorySystem();
		PlayerKeyboardInteractionController.EnableItemInteractionLogic();
		PlayerKeyboardInteractionController.EnableMovement();
		PlayerKeyboardInteractionController.EnableMouseLook();

		StopGameLogic.ResumeGame();
	}

	public void Hide()
	{
		_animator.enabled = true;
		_animator.SetBool("IsHidden", true);
	}

	public void Hidden()
	{
		_animator.enabled = false;
		_inventorySystem.UseSelectedItem();

		PlayerKeyboardInteractionController.DisableInventorySystem();
		PlayerKeyboardInteractionController.DisableItemInteractionLogic();
		PlayerKeyboardInteractionController.DisableMovement();
		PlayerKeyboardInteractionController.DisableMouseLook();

		StopGameLogic.StopGame();

		var cheetSheetRenderer = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<CheetSheetRenderer>();
		cheetSheetRenderer.enabled = true;
		cheetSheetRenderer.RenderItem(gameObject);
	}

	public void Shown()
	{
		_animator.enabled = false;

		_cheetSheetRenderer.HidePanel();
	}
}
