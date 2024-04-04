using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
	// private static AudioMixer _audioMixer;
	private static AudioSource _bgTestMusic;
	private static AudioSource _bgInitialSceneMusic;
	private static AudioSource _bgEndSceneMusic;
	private static AudioSource _bgHorrorSceneMusic;
	private static Dictionary<AudioSource, float> _defaultVolums;

	private class CoroutineExecuter : MonoBehaviour { }
	private static CoroutineExecuter instance;
	private static float _currentTime;

	private static int _sceneIndex;

	void Start()
	{
		var bg = transform.Find("BG");
		_bgTestMusic = bg.Find("BgTestMusic").GetComponent<AudioSource>();
		_bgInitialSceneMusic = bg.Find("BgInitialSceneMusic").GetComponent<AudioSource>();
		_bgEndSceneMusic = bg.Find("BgEndSceneMusic").GetComponent<AudioSource>();
		_bgHorrorSceneMusic = bg.Find("BgHorrorSceneMusic").GetComponent<AudioSource>();

		_defaultVolums = new Dictionary<AudioSource, float>();
		_defaultVolums.Add(_bgTestMusic, _bgTestMusic.volume);
		_defaultVolums.Add(_bgInitialSceneMusic, _bgInitialSceneMusic.volume);
		_defaultVolums.Add(_bgEndSceneMusic, _bgEndSceneMusic.volume);
		_defaultVolums.Add(_bgHorrorSceneMusic, _bgHorrorSceneMusic.volume);

		_sceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (_sceneIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			StartCoroutine(SoundFadeIn(_bgInitialSceneMusic, 3f));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.Horror)
		{
			StartCoroutine(SoundFadeIn(_bgHorrorSceneMusic, 3f));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.End)
		{
			StartCoroutine(SoundFadeIn(_bgEndSceneMusic, 3f));
		}

		//_audioMixer = Resources.Load<AudioMixer>("Audio/Mixers/Main");
	}


	public static void PlayBgTestMusic(float transition)
	{
		if (!instance)
		{
			instance = FindObjectOfType<CoroutineExecuter>();

			if (!instance)
			{
				instance = new GameObject("CoroutineExecuter").AddComponent<CoroutineExecuter>();
			}
		}

		// _audioMixer.FindSnapshot("Passing The Test").TransitionTo(transition);

		if (_sceneIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			instance.StartCoroutine(ChangeBetweenTwoSounds(_bgInitialSceneMusic, _bgTestMusic, transition));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.Horror)
		{
			instance.StartCoroutine(ChangeBetweenTwoSounds(_bgHorrorSceneMusic, _bgTestMusic, transition));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.BackRooms)
		{
			instance.StartCoroutine(SoundFadeIn(_bgTestMusic, transition));
		}
	}

	public static void StopBgTestMusic(float transition)
	{
		if (!instance)
		{
			instance = FindObjectOfType<CoroutineExecuter>();

			if (!instance)
			{
				instance = new GameObject("CoroutineExecuter").AddComponent<CoroutineExecuter>();
			}
		}

		//_audioMixer.FindSnapshot("Default").TransitionTo(transition);

		if (_sceneIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			instance.StartCoroutine(ChangeBetweenTwoSounds(_bgTestMusic, _bgInitialSceneMusic, transition));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.Horror)
		{
			instance.StartCoroutine(ChangeBetweenTwoSounds(_bgTestMusic, _bgHorrorSceneMusic, transition));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.BackRooms)
		{
			instance.StartCoroutine(SoundFadeOut(_bgTestMusic, transition));
		}
	}

	private static IEnumerator SoundFadeOut(AudioSource audio, float time)
	{
		float timeLeft = time;

		while (timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;

			audio.volume = timeLeft / time * _defaultVolums[audio];

			yield return null;
		}

		audio.Stop();
		audio.enabled = false;
	}

	private static IEnumerator SoundFadeIn(AudioSource audio, float time)
	{
		audio.enabled = true;
		audio.Play();

		float timeLeft = 0;

		while (timeLeft < time)
		{
			timeLeft += Time.deltaTime;

			audio.volume = timeLeft / time * _defaultVolums[audio];

			yield return null;
		}
	}

	private static IEnumerator ChangeBetweenTwoSounds(AudioSource first, AudioSource second, float time)
	{
		_currentTime = 0;

		second.enabled = true;
		second.Play();

		while (_currentTime < time)
		{
			_currentTime += Time.deltaTime;

			first.volume = (time - _currentTime) / time * _defaultVolums[first];
			second.volume = _currentTime / time * _defaultVolums[second];

			yield return null;
		}

		first.Stop();
		first.enabled = false;
	}
}