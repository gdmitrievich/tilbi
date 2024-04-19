using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
	// UI
	private static AudioSource _correctBeep;
	private static AudioSource _incorrectBeep;

	// Inventory
	private static AudioSource _inventoryItemChangedSound;

	// CheetSheet
	private static AudioSource _pageTurnSound;
	private static AudioSource _swooshSound;

	void Start()
	{
		var sfx = transform.Find("SFX");
		var ui = sfx.transform.Find("UI");
		_correctBeep = ui.Find("Correct Beep").GetComponent<AudioSource>();
		_incorrectBeep = ui.Find("Incorrect Beep").GetComponent<AudioSource>();

		_inventoryItemChangedSound = sfx.transform.Find("InventoryItemChanged")?.GetComponent<AudioSource>();

		var items = sfx.transform.Find("Items");
		_pageTurnSound = items?.transform.Find("PageTurn")?.GetComponent<AudioSource>();
		_swooshSound = items?.transform.Find("Swoosh")?.GetComponent<AudioSource>();
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

	public static class Items
	{
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

	public static class Other
	{
		public static void PlayInventoryItemChangedSound()
		{
			_inventoryItemChangedSound?.PlayOneShot(_inventoryItemChangedSound.clip);
		}
	}
}