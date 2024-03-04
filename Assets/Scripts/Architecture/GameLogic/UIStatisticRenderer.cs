using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatisticRenderer : MonoBehaviour
{
	private TextMeshProUGUI _testInfoText;
	private GameObject[] _inventoryItems;
	private RectTransform _energyBarTransform;
	private float _MIN_ENERGY_VALUE = 770f;

	private PlayerMovement _playerMovement;

	void Awake()
	{
		LoadUIStatisticGameObjects();
		Debug.Log(_testInfoText.text);
		_playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}

	void Update() {
		_energyBarTransform.offsetMax = new Vector2(-_MIN_ENERGY_VALUE + _MIN_ENERGY_VALUE * Utility.GetPercentage(_playerMovement.Energy, _playerMovement.EnergyLimit),0);
	}

	void OnEnable()
	{
		UITestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		Inventory.SelectedItemChanging += OnSelectedItemChanging;
		Inventory.SelectedItemChanged += OnSelectedItemChanged;
	}

	void OnDisable()
	{
		UITestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		Inventory.SelectedItemChanging -= OnSelectedItemChanging;
		Inventory.SelectedItemChanged -= OnSelectedItemChanged;
	}

	private void OnTestSuccessfullyPassed(GameObject obj) {
		_testInfoText.text = PlayerPrefs.GetInt("PassedTests").ToString() + "/6 Tests";
	}

	private void OnSelectedItemChanging(GameObject obj) {
		_inventoryItems[]
	}

	private void OnSelectedItemChanged(GameObject obj) {

	}

	private void LoadUIStatisticGameObjects()
	{
		const string COMMON_PATH = "Statistic/Main Panel/";

		GameObject uiObj = GameObject.Find("/UI");
		_testInfoText = uiObj.transform.Find(COMMON_PATH + "Tests Info Text").GetComponent<TextMeshProUGUI>();
		_energyBarTransform = uiObj.transform.Find(COMMON_PATH + "Energy Bar Panel/Energy Bar").GetComponent<RectTransform>();

		RectTransform inventoryItemsParent = uiObj.transform.Find(COMMON_PATH + "Inventory Panel").GetComponent<RectTransform>();
		_inventoryItems = new GameObject[inventoryItemsParent.childCount];
		for (int i = 0; i < inventoryItemsParent.childCount; ++i)
		{
			_inventoryItems[i] = uiObj.transform.Find(COMMON_PATH + "Inventory Panel/Inventory Item " + (i + 1).ToString()).GetComponent<GameObject>();
		}
	}
}
