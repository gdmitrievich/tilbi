using UnityEngine;

public class NPCAnimationControllerScript : MonoBehaviour
{
	private Animator _animator;
	[SerializeField] private bool _isTalking;
	[SerializeField] private bool _isChilling;
	[SerializeField] private bool _isSitting;

	void Awake()
	{
		_animator = GetComponent<Animator>();

		if (_isTalking)
		{
			_animator.SetTrigger("Talk");
		}
		else if (_isChilling)
		{
			_animator.SetTrigger("Chill");
		}
		else if (_isSitting)
		{
			_animator.SetTrigger("Sit");
		}
	}

	public void SetWalkingState(bool state) {
		_animator.SetBool("IsWalking", state);
	}
}
