using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSlidersController : MonoBehaviour
{
	private const float _MULTIPLIER = 15;

	[SerializeField] private AudioMixerGroup _mixer;
	private MouseLook _mouseLook;

	void Start()
	{
		_mouseLook = GameObject.FindGameObjectWithTag("Player").transform.Find("Main Camera").GetComponent<MouseLook>();
		_mouseLook.Sensitivity = PlayerPrefs.GetFloat("MouseSensitivity") * _mouseLook.BaseSensitivity;

		var menuMainPanelTransform = GameObject.Find("/UI").transform.Find("Menu/Main Panel");
		Debug.Log($"BgMusic: {PlayerPrefs.GetFloat("BgMusicVolume")}");
		menuMainPanelTransform.Find("Menu Item Panel 1").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("BgMusicVolume");
		menuMainPanelTransform.Find("Menu Item Panel 2").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");
		menuMainPanelTransform.Find("Menu Item Panel 3").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("UISoundsVolume");
		menuMainPanelTransform.Find("Menu Item Panel 4").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("VoiseActingVolume");
		menuMainPanelTransform.Find("Menu Item Panel 5").GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("MouseSensitivity");
	}

	public void OnBgMusicSliderValueChanged(float value)
	{
		Debug.Log($"bgmusic slider has changed: {PlayerPrefs.GetFloat("BgMusicVolume")}");
		_mixer.audioMixer.SetFloat("BgMusicVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefs.SetFloat("BgMusicVolume", value);
	}

	public void OnSoundEffectsSliderValueChanged(float value)
	{
		_mixer.audioMixer.SetFloat("SFXVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefs.SetFloat("SFXVolume", value);
	}

	public void OnUISoundsSliderValueChanged(float value)
	{
		_mixer.audioMixer.SetFloat("UIVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefs.SetFloat("UISoundsVolume", value);
	}

	public void OnVoiseActingSliderValueChanged(float value)
	{
		_mixer.audioMixer.SetFloat("VoiceActingVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefs.SetFloat("VoiseActingVolume", value);
	}

	public void OnMouseSensitivitySliderValueChanged(float value)
	{
		_mouseLook.Sensitivity = _mouseLook.BaseSensitivity * value;
		PlayerPrefs.SetFloat("MouseSensitivity", value);
	}
}