using UnityEngine;

public class MenuSlidersController : MonoBehaviour
{
	// private GameMusic _gameMusic;
	private MouseLook _mouseLook;

	private const float _BASE_SENSITIVITY = 350F;

	void Awake()
	{
		_mouseLook = GameObject.FindGameObjectWithTag("Player").transform.Find("Main Camera").GetComponent<MouseLook>();
	}

	public void OnBgMusicSliderValueChanged(float value)
	{

	}

	public void OnAudioEffectsSliderValueChanged(float value)
	{

	}

	public void OnMouseSensitivitySliderValueChanged(float value)
	{
		_mouseLook.Sensitivity = _BASE_SENSITIVITY * value * 2;
		//PlayerPrefs.SetFloat("mouseSensitivity", value);
	}
}