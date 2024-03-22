using UnityEngine;
using UnityEngine.AI;

public class WalkingAnimationSpeedController : MonoBehaviour
{
	[SerializeField] private float _baseSpeed;
	private NavMeshAgent _navMeshAgent;
	private Animator _animator;
	private float _currentSpeed;
	private float _previousFrameSpeed;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_navMeshAgent = GetComponentInParent<NavMeshAgent>();
		_currentSpeed = _previousFrameSpeed = _navMeshAgent.speed;
		_animator.SetFloat("WalkingSpeed", Utility.GetPercentage(_currentSpeed, _baseSpeed));
	}

	void Update()
	{
		_currentSpeed = _navMeshAgent.speed;
		if (_currentSpeed != _previousFrameSpeed)
		{
			_animator.SetFloat("WalkingSpeed", Utility.GetPercentage(_currentSpeed, _baseSpeed));
		}
		_previousFrameSpeed = _navMeshAgent.speed;
	}
}