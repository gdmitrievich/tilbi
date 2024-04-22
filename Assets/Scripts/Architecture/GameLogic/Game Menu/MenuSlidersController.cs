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
	private const float _DEFAULT_SLIDER_VALUE = 0.5F;

	void Start()
	{
		_mouseLook = GameObject.FindGameObjectWithTag("Player").transform.Find("Main Camera").GetComponent<MouseLook>();
		// _mouseLook.Sensitivity = PlayerPrefsManager.prefs.GetFloat("MouseSensitivity") * _mouseLook.BaseSensitivity;

		var menuMainPanelTransform = GameObject.Find("/UI").transform.Find("Menu/Main Panel");

		menuMainPanelTransform.Find("Menu Item Panel 1").GetComponentInChildren<Slider>().value = PlayerPrefsManager.prefs.GetFloat("BgMusicVolume", _DEFAULT_SLIDER_VALUE);
		menuMainPanelTransform.Find("Menu Item Panel 2").GetComponentInChildren<Slider>().value = PlayerPrefsManager.prefs.GetFloat("SFXVolume", _DEFAULT_SLIDER_VALUE);
		menuMainPanelTransform.Find("Menu Item Panel 3").GetComponentInChildren<Slider>().value = PlayerPrefsManager.prefs.GetFloat("UISoundsVolume", _DEFAULT_SLIDER_VALUE);
		menuMainPanelTransform.Find("Menu Item Panel 4").GetComponentInChildren<Slider>().value = PlayerPrefsManager.prefs.GetFloat("VoiseActingVolume", _DEFAULT_SLIDER_VALUE);
		menuMainPanelTransform.Find("Menu Item Panel 5").GetComponentInChildren<Slider>().value = PlayerPrefsManager.prefs.GetFloat("MouseSensitivity", _DEFAULT_SLIDER_VALUE);
	}

	public void OnBgMusicSliderValueChanged(float value)
	{
		Debug.Log("BgMusic Changed");
		_mixer.audioMixer.SetFloat("BgMusicVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefsManager.prefs.SetFloat("BgMusicVolume", value);
		PlayerPrefsManager.prefs.Save();
	}

	public void OnSoundEffectsSliderValueChanged(float value)
	{
		_mixer.audioMixer.SetFloat("SFXVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefsManager.prefs.SetFloat("SFXVolume", value);
		PlayerPrefsManager.prefs.Save();
	}

	public void OnUISoundsSliderValueChanged(float value)
	{
		_mixer.audioMixer.SetFloat("UIVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefsManager.prefs.SetFloat("UISoundsVolume", value);
		PlayerPrefsManager.prefs.Save();
	}

	public void OnVoiseActingSliderValueChanged(float value)
	{
		_mixer.audioMixer.SetFloat("VoiceActingVolume", Math.Clamp(Mathf.Log10(value) * _MULTIPLIER, -80, 0));
		PlayerPrefsManager.prefs.SetFloat("VoiseActingVolume", value);
		PlayerPrefsManager.prefs.Save();
	}

	public void OnMouseSensitivitySliderValueChanged(float value)
	{
		_mouseLook.Sensitivity = _mouseLook.BaseSensitivity * value;
		PlayerPrefsManager.prefs.SetFloat("MouseSensitivity", value);
		PlayerPrefsManager.prefs.Save();
	}
}