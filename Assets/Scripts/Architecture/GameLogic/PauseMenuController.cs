using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour {
	private AudioSource[] _allAudioSources;
	private Dictionary<AudioSource, bool> _audioSourcesPlayingState;

	private AudioMixer _audioMixer;
	private float _previousMasterVolume;
	private GameObject _pauseMenu;
	private bool _isMenuShown;
	private CursorLockMode _previousCursorLockMode;

	void Awake() {
		_pauseMenu = transform.Find("BG").gameObject;
		_audioMixer = Resources.Load<AudioMixer>("Audio/Mixers/Main");
		_allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

		_audioSourcesPlayingState = new Dictionary<AudioSource, bool>();

		_isMenuShown = false;
	}

	void Update() {
		if (Input.GetKey(KeyCode.Escape) && !_isMenuShown) {
			_pauseMenu.gameObject.SetActive(true);

			_previousCursorLockMode = Cursor.lockState;
			Cursor.lockState = CursorLockMode.None;

			Time.timeScale = 0;

			_audioMixer.GetFloat("MasterVolume", out _previousMasterVolume);
			_audioMixer.SetFloat("MasterVolume", -80);
			StopAllAudio();

			_isMenuShown = true;
		}
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void ContinueGame() {
		_pauseMenu.gameObject.SetActive(false);

		Cursor.lockState = _previousCursorLockMode;

		Time.timeScale = 1;

		_audioMixer.SetFloat("MasterVolume", _previousMasterVolume);
		ResumeAllAudio();

		_isMenuShown = false;
	}

	private void StopAllAudio() {
		foreach (var audioSource in _allAudioSources) {
			_audioSourcesPlayingState[audioSource] = audioSource.isPlaying;
			if (_audioSourcesPlayingState[audioSource]) {
				audioSource.Pause();
			}
		}
	}

	private void ResumeAllAudio() {
		foreach (var audioSource in _allAudioSources) {
			if (_audioSourcesPlayingState[audioSource]) {
				audioSource.Play();
			}
		}
	}
}