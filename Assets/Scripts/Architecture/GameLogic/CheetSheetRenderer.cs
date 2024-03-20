using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheetSheetRenderer : MonoBehaviour
{
	[SerializeField] private GameObject _hintPanel;
	private RectTransform _hintsParent;

	private RectTransform _cheetSheetCanvas;
	private CheetSheetPanelAnimation _cheetSheetPanelAnimation;

	private bool _isEscKeyPressedOnce;

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
				GameObject hintPanelPrefab = Instantiate(_hintPanel);
				hintPanelPrefab.transform.SetParent(_hintsParent.transform, false);
				hintPanelPrefab.GetComponentInChildren<TextMeshProUGUI>().text = hint;
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
