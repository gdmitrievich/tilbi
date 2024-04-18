using UnityEngine;

public class TilbiReplicasManager : MonoBehaviour
{
	[SerializeField] private AudioClip _greetings;
	private AudioSource _audioSource;
	[SerializeField] private float _delay;
	private float _time;
	private bool _isOneTime;

	void Start() {
		_audioSource = GetComponent<AudioSource>();

		_time = 0;
		_isOneTime = false;
	}

	void Update() {
		if (_time < _delay) {
			_time += Time.deltaTime;
			return;
		}
		if (!_isOneTime) {
			_isOneTime = true;
			_audioSource.clip = _greetings;
			_audioSource.Play();
			return;
		}
		if (_audioSource.time >= _audioSource.clip.length) {
			_audioSource.enabled = false;
			enabled = false;
		}
	}
}