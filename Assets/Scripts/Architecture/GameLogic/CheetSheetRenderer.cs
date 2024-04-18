using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class CheetSheetRenderer : MonoBehaviour
{
	[SerializeField] private GameObject _hintText;
	private RectTransform _hintsParent;

	private RectTransform _cheetSheetCanvas;
	private CheetSheetPanelAnimation _cheetSheetPanelAnimation;

	private bool _isMouseKeyPressedOnce;

	private const float _INITIAL_FONT_SIZE = 50;
	private const float _INITIAL_TEXT_HEIGHT = 60;
	private const float _MULTIPLIER = _INITIAL_TEXT_HEIGHT / _INITIAL_FONT_SIZE;

	[SerializeField] private AudioMixerSnapshot _defaultSnapshot;
	[SerializeField] private AudioMixerSnapshot _cheetSheetReadingSnapshot;

	void OnEnable()
	{
		_cheetSheetCanvas = (RectTransform)GameObject.Find("/UI").transform.Find("Cheet Sheet");
		_hintsParent = (RectTransform)_cheetSheetCanvas.transform.Find("Cheet Sheet Panel/Body Panel/Content");
		_cheetSheetPanelAnimation = GameObject.Find("/UI").transform.Find("Cheet Sheet/Cheet Sheet Panel").GetComponent<CheetSheetPanelAnimation>();
		_isMouseKeyPressedOnce = false;
	}

	public void RenderItem(GameObject obj)
	{
		if (obj.CompareTag("CheetSheet"))
		{
			Cursor.lockState = CursorLockMode.None;

			_cheetSheetCanvas.gameObject.SetActive(true);

			_cheetSheetPanelAnimation.enabled = true;
			_cheetSheetPanelAnimation.Raise();
			_cheetSheetReadingSnapshot.TransitionTo(1f);

			if (_hintsParent.transform.childCount > 0)
			{
				Utility.DestroyChildrens(_hintsParent);
			}

			List<string> hints = obj.GetComponent<CheetSheet>().hints;
			foreach (var hint in hints)
			{
				GameObject hintTextPrefab = Instantiate(_hintText);
				hintTextPrefab.transform.SetParent(_hintsParent.transform, false);
				TextMeshProUGUI hintText = hintTextPrefab.GetComponent<TextMeshProUGUI>();
				hintText.text = hint;

				hintText.ForceMeshUpdate();
				hintText.rectTransform.sizeDelta = new Vector2(1600, hintText.textInfo.lineCount * hintText.fontSize * _MULTIPLIER);
			}
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(1) && !_cheetSheetPanelAnimation.isActiveAndEnabled && !_isMouseKeyPressedOnce)
		{
			_cheetSheetPanelAnimation.enabled = true;
			_cheetSheetPanelAnimation.PutDown();
			_defaultSnapshot.TransitionTo(1f);
			_isMouseKeyPressedOnce = true;
		}
	}

	public void HidePanel()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_cheetSheetCanvas.gameObject.SetActive(false);
		_isMouseKeyPressedOnce = false;
		enabled = false;
	}
}
