using UnityEngine;

public class ItemAudioSourcesScript : MonoBehaviour
{
	[SerializeField] private static AudioSource[] _dropSounds;
	[SerializeField] private static AudioSource _eatingSound;
	[SerializeField] private static AudioSource _pickingUpSound;
	[SerializeField] private static AudioSource _pageTurnSound;
	[SerializeField] private static AudioSource _swooshSound;

	private static float _min_pitch;
	private static float _max_pitch;
	private static float _timeToEat;
	private static float _currentTimeToEat;
	private static float _intervalTime;
	private static float _currentIntervalTime;

	void Start()
	{
		var items = GameObject.Find("Audio/SFX/Items");
		_dropSounds = new AudioSource[3];
		for (int i = 0; i < 3; ++i)
		{
			_dropSounds[i] = items.transform.Find("DropSound" + (i + 1)).GetComponent<AudioSource>();
		}
		_eatingSound = items.transform.Find("Eating").GetComponent<AudioSource>();
		_pickingUpSound = items.transform.Find("PickingUp").GetComponent<AudioSource>();
		_pageTurnSound = items.transform.Find("PageTurn").GetComponent<AudioSource>();
		_swooshSound = items.transform.Find("Swoosh").GetComponent<AudioSource>();
	}

	void Update()
	{
		if (_currentTimeToEat >= _timeToEat)
		{
			return;
		}

		if (_currentIntervalTime < _intervalTime)
		{
			_currentIntervalTime += Time.deltaTime;
			_currentTimeToEat += Time.deltaTime;
			return;
		}

		_eatingSound.pitch = Random.Range(_min_pitch, _max_pitch);
		_eatingSound.PlayOneShot(_eatingSound.clip);
		_currentIntervalTime = 0;
	}

	public static void PlayDropSound(float min_pitch = 1, float max_pitch = 1)
	{
		AudioSource dropSound = _dropSounds[Random.Range(0, _dropSounds.Length)];
		dropSound.pitch = Random.Range(min_pitch, max_pitch);
		dropSound.Play();
	}

	public static void PlayEatingSound(float timeToEat, float interval, float min_pitch = 1, float max_pitch = 1)
	{
		_currentTimeToEat = 0;

		_timeToEat = timeToEat;
		_intervalTime = interval;

		_min_pitch = min_pitch;
		_max_pitch = max_pitch;
	}

	public static void PlayPickingUpSound(float min_pitch = 1, float max_pitch = 1)
	{
		_pickingUpSound.pitch = Random.Range(min_pitch, max_pitch);
		_pickingUpSound.Play();
	}

	public static void PlayPageTurnSound(float min_pitch = 1, float max_pitch = 1)
	{
		_pageTurnSound.pitch = Random.Range(min_pitch, max_pitch);
		_pageTurnSound.Play();
	}

	public static void PlaySwooshSound(float min_pitch = 1, float max_pitch = 1)
	{
		_swooshSound.pitch = Random.Range(min_pitch, max_pitch);
		_swooshSound.Play();
	}
}