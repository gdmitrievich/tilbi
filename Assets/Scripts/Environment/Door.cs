using UnityEngine;
public class Door : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	private bool _isOpen;

	void Awake()
	{
		_isOpen = false;
	}

	void OnEnable()
	{
		ItemInteractionLogic.InteractableItemTouched += OnInteractableItemTouched;
	}

	void OnDisable()
	{
		ItemInteractionLogic.InteractableItemTouched -= OnInteractableItemTouched;
	}

	private void OnInteractableItemTouched(GameObject item)
	{
		_isOpen = !_isOpen;

		if (item.CompareTag("Door"))
		{
			if (_isOpen)
			{
				_animator.SetBool("IsOpen", true);
			}
			else
			{
				_animator.SetBool("IsOpen", false);
			}
		}
	}
}
