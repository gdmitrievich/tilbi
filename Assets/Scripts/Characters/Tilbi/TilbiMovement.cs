using UnityEngine;
using UnityEngine.AI;

public class TilbiMovement : MonoBehaviour, IMovable
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private Transform _playerTransform;
	[SerializeField] private bool _isPlayerVisible;

	public float Speed
	{
		get => _agent.speed;
		set
		{
			if (value > 0)
			{
				_agent.speed = value;
			}
		}
	}

	void Update()
	{
		transform.LookAt(_playerTransform);
		_agent.SetDestination(_playerTransform.position);

	}
}