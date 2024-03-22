using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour, IMovable
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private float _range;

	private NPCAnimationControllerScript _nPCAnimationControllerScript;

	public float Speed
	{
		get => _agent.speed;
		set
		{
			if (value >= 0)
			{
				_agent.speed = value;
			}
		}
	}

	void Awake() {
		_nPCAnimationControllerScript = GetComponentInChildren<NPCAnimationControllerScript>();
	}

	void Update()
	{
		if (_agent.remainingDistance <= _agent.stoppingDistance)
		{
			_nPCAnimationControllerScript.SetWalkingState(false);
			Vector3 point;
			if (RandomPoint(transform.position, _range, out point))
			{
				_agent.SetDestination(point);
			}
		} else {
			_nPCAnimationControllerScript.SetWalkingState(true);
		}
	}

	bool RandomPoint(Vector3 center, float _range, out Vector3 result)
	{
		Vector3 randomPoint = center + Random.insideUnitSphere * _range;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
		{
			result = hit.position;
			return true;
		}

		result = Vector3.zero;
		return false;
	}
}