using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicManager : MonoBehaviour
{
	private static AudioSource _bgTestMusic;
	private static AudioSource _bgInitialSceneMusic;
	private static AudioSource _bgEndSceneMusic;
	public static AudioSource BgTestMusic
	{
		get => _bgTestMusic;
	}
	public static AudioSource BgInitialSceneMusic
	{
		get => _bgInitialSceneMusic;
	}
	public static AudioSource BgEndSceneMusic
	{
		get => _bgEndSceneMusic;
	}

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

		_sceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (_sceneIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			StartCoroutine(SoundFadeIn(_bgInitialSceneMusic, 3f));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.End)
		{
			StartCoroutine(SoundFadeIn(_bgEndSceneMusic, 3f));
		}
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

		if (_sceneIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			instance.StartCoroutine(ChangeBetweenTwoSounds(_bgInitialSceneMusic, _bgTestMusic, transition));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.End)
		{
			instance.StartCoroutine(ChangeBetweenTwoSounds(_bgEndSceneMusic, _bgTestMusic, transition));
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

		if (_sceneIndex == (int)SceneManagerLogic.Scene.Initial)
		{
		instance.StartCoroutine(ChangeBetweenTwoSounds(_bgTestMusic, _bgInitialSceneMusic, transition));
		}
		else if (_sceneIndex == (int)SceneManagerLogic.Scene.End)
		{
			instance.StartCoroutine(ChangeBetweenTwoSounds(_bgTestMusic, _bgEndSceneMusic, transition));
		}
	}

	private static IEnumerator SoundFadeOut(AudioSource audio, float time)
	{
		float timeLeft = time;

		while (timeLeft > 0)
		{
			timeLeft -= Time.deltaTime;

			audio.volume = timeLeft / time;

			yield return null;
		}

		audio.Stop();
		audio.enabled = false;
	}

	private static IEnumerator SoundFadeIn(AudioSource audio, float time)
	{
		Debug.Log(audio.isActiveAndEnabled);
		audio.enabled = true;
		Debug.Log(audio.isActiveAndEnabled);
		audio.Play();

		float timeLeft = 0;

		while (timeLeft < time)
		{
			timeLeft += Time.deltaTime;

			audio.volume = timeLeft / time;

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

			first.volume = (time - _currentTime) / time;
			second.volume = _currentTime / time;

			yield return null;
		}

		first.Stop();
		first.enabled = false;
	}
}