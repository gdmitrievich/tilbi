using UnityEngine;

public class FluorescentAudioController : MonoBehaviour {
	[SerializeField] private AudioClip[] _audioClips;
	private AudioSource _audioSource;
	void Start() {
		_audioSource = GetComponent<AudioSource>();

		_audioSource.pitch = Random.Range(0.99f, 1.01f);
		_audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];

		_audioSource.Play();
	}
}