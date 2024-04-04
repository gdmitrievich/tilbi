using UnityEngine;

public class AudioAreaZoneController : MonoBehaviour
{
	private BoxCollider _boxCollider;
	private AudioSource _audioSource;

	public bool IsAtZone {get; private set;}

	private float _soundTime;

	void Awake()
	{
		_boxCollider = GetComponentInParent<BoxCollider>();
		_audioSource = GetComponentInParent<AudioSource>();

		_soundTime = 0;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			IsAtZone = true;
			_audioSource.time = _soundTime;
			StartCoroutine(BgMusicManager.SoundFadeIn(_audioSource, 1f));
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			IsAtZone = false;
			_soundTime = _audioSource.time;
			StartCoroutine(BgMusicManager.SoundFadeOut(_audioSource, 1f));
		}
	}
}