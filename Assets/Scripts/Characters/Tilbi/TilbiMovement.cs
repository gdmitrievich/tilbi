using UnityEngine;
using UnityEngine.AI;

public class TilbiMovement : MonoBehaviour, IMovable
{
	private NavMeshAgent _agent;
	private Transform _playerTransform;

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
		_agent = GetComponent<NavMeshAgent>();
		_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		transform.LookAt(_playerTransform);
		_agent.SetDestination(_playerTransform.position);
	}

	void OnEnable()
	{
		PCTestPassingLogic.TestFailed += OnTestFailed;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestFailed -= OnTestFailed;
	}

	private void OnTestFailed(GameObject obj)
	{
		if (PlayerPrefs.GetInt("PassedTests") == 0) {
			//Speed = 150;
		} else {
			Speed *= 1.5f;
		}
	}
}