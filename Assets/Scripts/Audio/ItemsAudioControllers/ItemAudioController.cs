using UnityEngine;
public class ItemAudioController : MonoBehaviour {
	[SerializeField] private AudioClip[] _dropClips;
	[SerializeField] private AudioClip _pickingUpClip;
	protected AudioSource _audioSource;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayDropClip(float min_pitch = 1, float max_pitch = 1)
	{
		_audioSource.clip = _dropClips[Random.Range(0, _dropClips.Length)];
		_audioSource.pitch = Random.Range(min_pitch, max_pitch);
		_audioSource.PlayOneShot(_audioSource.clip);
	}

	public void PlayPickingUpClip(float min_pitch = 1, float max_pitch = 1)
	{
		_audioSource.pitch = Random.Range(min_pitch, max_pitch);
		_audioSource.PlayOneShot(_pickingUpClip);
	}
}