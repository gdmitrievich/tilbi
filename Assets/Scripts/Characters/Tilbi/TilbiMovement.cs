using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TilbiMovement : MonoBehaviour, IMovable
{
	private float _baseSpeed;
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

	public float BaseSpeed
	{
		get => _baseSpeed;
	}

	void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_player = GameObject.FindGameObjectWithTag("Player");

		_baseSpeed = _agent.speed;
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
		if (SceneManager.GetActiveScene().buildIndex == (int) SceneManagerLogic.Scene.Initial) {
			return;
		}

		Speed += 1f;
		StopGameLogic.ChangeSpeedValue(this, 1f);
	}

	private void OnPlayerCatched()
	{
		_agent.isStopped = true;
		_agent.velocity = Vector3.zero;
	}
}