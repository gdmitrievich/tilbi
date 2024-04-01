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

	void Start()
	{
		var sfx = transform.Find("SFX");
		var ui = sfx.transform.Find("UI");
		_correctBeep = ui.Find("Correct Beep").GetComponent<AudioSource>();
		_incorrectBeep = ui.Find("Incorrect Beep").GetComponent<AudioSource>();

		_inventoryItemChangedSound = sfx.transform.Find("InventoryItemChanged").GetComponent<AudioSource>();
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

	public static class Other
	{
		public static void PlayInventoryItemChangedSound()
		{
			_inventoryItemChangedSound.PlayOneShot(_inventoryItemChangedSound.clip);
		}
	}
}