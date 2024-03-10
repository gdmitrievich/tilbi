using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingAnimation : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	public Animator Animator
	{
		get => _animator;
	}

	void Start()
	{
		_animator = GetComponent<Animator>();
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
}
