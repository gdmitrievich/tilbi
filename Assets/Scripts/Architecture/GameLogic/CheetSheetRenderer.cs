using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheetSheetRenderer : MonoBehaviour
{
	[SerializeField] private GameObject _hintPanel;
	private RectTransform _hintsParent;

	private RectTransform _cheetSheetCanvas;

	void Start()
	{
		_cheetSheetCanvas = (RectTransform) GameObject.Find("/UI").transform.Find("Cheet Sheet");
		_hintsParent = (RectTransform) _cheetSheetCanvas.transform.Find("Cheet Sheet Panel/Body Panel/Content");
	}

	void OnEnable()
	{
		InventorySystem.ItemUsed += OnItemUsed;
	}
	void OnDisable()
	{
		InventorySystem.ItemUsed -= OnItemUsed;
	}

	private void OnItemUsed(GameObject obj)
	{
		if (obj.CompareTag("CheetSheet"))
		{
			Cursor.lockState = CursorLockMode.None;
			_cheetSheetCanvas.gameObject.SetActive(true);

			if (_hintsParent.transform.childCount > 0) {
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

	void Update() {
		if (Input.GetKey(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.Locked;
			_cheetSheetCanvas.gameObject.SetActive(false);
		}
	}
}
