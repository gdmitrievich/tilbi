using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheetSheetRenderer : MonoBehaviour
{
	[SerializeField] private GameObject _hintText;
	private RectTransform _hintsParent;

	private RectTransform _cheetSheetCanvas;
	private CheetSheetPanelAnimation _cheetSheetPanelAnimation;

	private bool _isEscKeyPressedOnce;

	private const float _INITIAL_FONT_SIZE = 50;
	private const float _INITIAL_TEXT_HEIGHT = 60;
	private const float _MULTIPLIER = _INITIAL_TEXT_HEIGHT / _INITIAL_FONT_SIZE;

	void OnEnable()
	{
		_cheetSheetCanvas = (RectTransform)GameObject.Find("/UI").transform.Find("Cheet Sheet");
		_hintsParent = (RectTransform)_cheetSheetCanvas.transform.Find("Cheet Sheet Panel/Body Panel/Content");
		_cheetSheetPanelAnimation = GameObject.Find("/UI").transform.Find("Cheet Sheet/Cheet Sheet Panel").GetComponent<CheetSheetPanelAnimation>();
		_isEscKeyPressedOnce = false;
	}

	public void RenderItem(GameObject obj)
	{
		if (obj.CompareTag("CheetSheet"))
		{
			Cursor.lockState = CursorLockMode.None;

			_cheetSheetCanvas.gameObject.SetActive(true);

			_cheetSheetPanelAnimation.enabled = true;
			_cheetSheetPanelAnimation.Raise();

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
		if (Input.GetKey(KeyCode.Escape) && !_cheetSheetPanelAnimation.isActiveAndEnabled && !_isEscKeyPressedOnce)
		{
			_cheetSheetPanelAnimation.enabled = true;
			_cheetSheetPanelAnimation.PutDown();
			_isEscKeyPressedOnce = true;
		}
	}

	public void HidePanel()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_cheetSheetCanvas.gameObject.SetActive(false);
		_isEscKeyPressedOnce = false;
		enabled = false;
	}
}
