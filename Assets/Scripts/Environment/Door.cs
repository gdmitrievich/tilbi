using UnityEngine;
public class Door : MonoBehaviour, IInteractable
{
	[SerializeField] private Animator _animator;
	private bool _isOpen;

	void Awake()
	{
		_isOpen = false;
	}

	public void Interact(GameObject obj) {
		_isOpen = !_isOpen;

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
