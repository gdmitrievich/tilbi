using UnityEngine;

public class DecorativeDoorAudioController : MonoBehaviour, IInteractable
{
	[SerializeField] private AudioClip[] _lockedClips;
	private AudioSource _audioSource;

	void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void Interact(GameObject obj)
	{
		_audioSource.pitch = Random.Range(0.9f, 1.1f);
		_audioSource.PlayOneShot(_lockedClips[Random.Range(0, _lockedClips.Length)]);
	}
}