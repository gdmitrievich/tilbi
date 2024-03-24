using UnityEngine;
using UnityEngine.AI;

public class TilbiMovement : MonoBehaviour, IMovable
{
	private NavMeshAgent _agent;
	private GameObject _player;

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

	void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		_agent.SetDestination(_player.transform.position);
	}

	void OnEnable()
	{
		PCTestPassingLogic.TestFailed += OnTestFailed;
		PlayerCollisionListener.PlayerCatched += OnPlayerCatched;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestFailed -= OnTestFailed;
		PlayerCollisionListener.PlayerCatched -= OnPlayerCatched;
	}

	private void OnTestFailed(GameObject obj)
	{
		Speed *= 1.5f;
	}

	private void OnPlayerCatched()
	{
		_agent.isStopped = true;
		_agent.velocity = Vector3.zero;
	}
}