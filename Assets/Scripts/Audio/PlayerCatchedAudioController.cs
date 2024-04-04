using UnityEngine;
using UnityEngine.Audio;

public class PlayerCatchedAudioController : MonoBehaviour
{
	[SerializeField] private AudioClip[] _hittingInAFaceClips;
	private AudioSource _audioSource;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlayHittingInAFaceSound()
	{
		_audioSource.pitch = Random.Range(0.9f, 1.1f);
		_audioSource.PlayOneShot(_hittingInAFaceClips[Random.Range(0, _hittingInAFaceClips.Length)]);
	}
}