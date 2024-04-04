using Unity.VisualScripting;
using UnityEngine;

public class WindSoundAudioController : MonoBehaviour
{
	private AudioSource _audioSource;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	void Update() {
		if (_audioSource.isPlaying) {
			_audioSource.time = Random.Range(0, _audioSource.clip.length);
			enabled = false;
		}
	}
}