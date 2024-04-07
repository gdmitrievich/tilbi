using UnityEngine;

public class TilbiReplicasManager : MonoBehaviour
{
	[SerializeField] private AudioClip _greetings;
	private AudioSource _audioSource;

	void Start() {
		_audioSource = GetComponent<AudioSource>();
		_audioSource.PlayOneShot(_greetings);
	}

	void Update() {
		if (!_audioSource.isPlaying) {
			_audioSource.enabled = false;
			enabled = false;
		}
	}
}