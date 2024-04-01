using UnityEngine;

public class DoorAudioController : MonoBehaviour {
	[SerializeField] private AudioClip _opening;
	[SerializeField] private AudioClip _closed;

	private AudioSource _audioSource;

	void Start() {
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayDoorOpeningAudio() {
		_audioSource.pitch = Random.Range(0.9f, 1.1f);
		_audioSource.PlayOneShot(_opening);
	}

	public void PlayDoorClosedAudio() {
		_audioSource.pitch = Random.Range(0.9f, 1.1f);
		_audioSource.PlayOneShot(_closed);
	}
}