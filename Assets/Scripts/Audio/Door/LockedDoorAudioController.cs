using UnityEngine;

public class LockedDoorAudioController : DoorAudioController {
	[SerializeField] private AudioClip[] _lockedClips;

	public void PlayLockedDoorAudio() {
		_audioSource.pitch = Random.Range(0.9f, 1.1f);
		_audioSource.PlayOneShot(_lockedClips[Random.Range(0, _lockedClips.Length)]);
	}
}