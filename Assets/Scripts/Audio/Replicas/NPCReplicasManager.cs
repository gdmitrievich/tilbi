using UnityEngine;

public class NPCReplicasManager : MonoBehaviour
{
	[SerializeField] private AudioClip[] _replicas;
	[SerializeField] private float _minTimeBetweenReplicas;
	[SerializeField] private float _maxTimeBetweenReplicas;
	private AudioSource _audioSource;

	private float _targetTime, _currentTime;

	void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		_targetTime = Random.Range(_minTimeBetweenReplicas, _maxTimeBetweenReplicas);
		_currentTime = 0;
	}

	void Update() {
		_currentTime += Time.deltaTime;
		if (_currentTime >= _targetTime) {
			_audioSource.PlayOneShot(_replicas[Random.Range(0, _replicas.Length)]);

			_targetTime = Random.Range(_minTimeBetweenReplicas, _maxTimeBetweenReplicas);
			_currentTime = 0;
		}
	}
}