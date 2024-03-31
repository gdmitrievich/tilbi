using TMPro;
using UnityEngine;

public class FootstepsAudioController : MonoBehaviour
{
	[SerializeField] private AudioClip[] _footstepClips;
	[SerializeField] private float _baseInterval;
	private float _interval;
	[SerializeField] private float _min_pitch;
	[SerializeField] private float _max_pitch;
	private IMovable _movable;

	private AudioSource _audioSource;
	private float _time;

	void Start()
	{
		_movable = GetComponent<IMovable>();
		_audioSource = GetComponent<AudioSource>();
	}

	void OnEnable() {
		PlayerCollisionListener.PlayerCatched += OnPlayerCatched;
	}

	void OnDisable() {
		PlayerCollisionListener.PlayerCatched -= OnPlayerCatched;
	}

	private void OnPlayerCatched() {
		_audioSource.enabled = false;
		enabled = false;
	}

	void Update() {
		if (_movable.Speed <= 0.1f) {
			_time = 0;
			return;
		}

		_interval = _baseInterval / (_movable.Speed / _movable.BaseSpeed);
		if (_time < _interval) {
			_time += Time.deltaTime;
			return;
		}

		_audioSource.pitch = Random.Range(_min_pitch, _max_pitch);
		_audioSource.PlayOneShot(_footstepClips[Random.Range(0, _footstepClips.Length)]);
		_time = 0;
	}
}