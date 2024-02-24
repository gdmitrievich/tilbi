using UnityEngine;
using UnityEngine.AI;

public class TilbiMovement : MonoBehaviour
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private Transform _playerTransform;
	[SerializeField] private bool _isPlayerVisible;
	void Update()
	{
		transform.LookAt(_playerTransform);
		_agent.SetDestination(_playerTransform.position);

	}
}