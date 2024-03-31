using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
	[SerializeField] private AudioMixer _mixer;
	[SerializeField] private AudioMixerGroup _master;

	private static AudioSource _correctBeep;
	private static AudioSource _incorrectBeep;

	void Start()
	{
		var sfx = transform.Find("SFX");
		_correctBeep = sfx.Find("Correct Beep").GetComponent<AudioSource>();
		_incorrectBeep = sfx.Find("Incorrect Beep").GetComponent<AudioSource>();
	}

	public static class UI
	{
		public static void PlayCorrectBeep()
		{
			_correctBeep.Play();
		}

		public static void PlayIncorrectBeep()
		{
			_incorrectBeep.Play();
		}
	}
}