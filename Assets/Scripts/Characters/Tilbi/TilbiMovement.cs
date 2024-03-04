using UnityEngine;
using UnityEngine.AI;

public class TilbiMovement : MonoBehaviour, IMovable
{
	[SerializeField] private NavMeshAgent _agent;
	[SerializeField] private Transform _playerTransform;

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

	void Update()
	{
		transform.LookAt(_playerTransform);
		_agent.SetDestination(_playerTransform.position);
	}

	void OnEnable()
	{
		UITestPassingLogic.TestFailed += OnTestFailed;
	}

	void OnDisable()
	{
		UITestPassingLogic.TestFailed -= OnTestFailed;
	}

	private void OnTestFailed(GameObject obj)
	{
		if (PlayerPrefs.GetInt("PassedTests") == 0) {
			Speed = 150;
		}
	}
}