using UnityEngine;

public class ClockAudioController : MonoBehaviour {
	[SerializeField] private AudioClip[] _tickTockClips;
	private AudioSource _audioSource;

	[SerializeField] private float _time;
	private float _currentTime;

	private AudioAreaZoneController _audioAreaZoneController;
	void Awake() {
		_audioSource = GetComponent<AudioSource>();
		_audioAreaZoneController = GetComponentInChildren<AudioAreaZoneController>();
		_audioSource.loop = true;

		_currentTime = 0;
	}

	void Update() {
		if (!_audioAreaZoneController.IsAtZone) {
			return;
		}

		_currentTime += Time.deltaTime;

		if (_currentTime >= _time) {
			_audioSource.pitch = Random.Range(0.9f, 1.1f);
			_audioSource.PlayOneShot(_tickTockClips[Random.Range(0, _tickTockClips.Length)]);

			_currentTime = 0;
		}
	}
}