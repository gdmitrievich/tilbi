using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatisticRenderer : MonoBehaviour
{
	private Image[] _selectedInventoryItemImages;
	private Image[] _selectedInventoryItemIcons;

	private string _basicStringText;
	private TextMeshProUGUI _testInfoText;
	private RectTransform _energyBarTransform;
	private float _MIN_ENERGY_VALUE = 770f;

	private PlayerMovement _playerMovement;
	private Inventory _inventory;

	void Start()
	{
		LoadUIStatisticGameObjects();
		_basicStringText = _testInfoText.text.Substring(1);
		_testInfoText.text = PlayerPrefsManager.prefs.GetInt("PassedTests") + _basicStringText;

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		_playerMovement = player.GetComponent<PlayerMovement>();
		_inventory = player.GetComponent<InventorySystem>().Inventory;
	}

	void Update()
	{
		_energyBarTransform.offsetMax = new Vector2(-_MIN_ENERGY_VALUE + _MIN_ENERGY_VALUE * Utility.GetPercentage(_playerMovement.Energy, _playerMovement.EnergyLimit), 0);
	}

	void OnEnable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;

		Inventory.SelectedItemChanging += OnSelectedItemChanging;
		Inventory.SelectedItemChanged += OnSelectedItemChanged;

		Inventory.ItemAdded += OnItemAdded;
		Inventory.ItemRemoved += OnItemRemoved;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;

		Inventory.SelectedItemChanging -= OnSelectedItemChanging;
		Inventory.SelectedItemChanged -= OnSelectedItemChanged;

		Inventory.ItemAdded -= OnItemAdded;
		Inventory.ItemRemoved -= OnItemRemoved;
	}

	private void OnTestSuccessfullyPassed(GameObject obj)
	{
		_testInfoText.text = PlayerPrefsManager.prefs.GetInt("PassedTests").ToString() + _basicStringText;
	}

	private void OnSelectedItemChanging(GameObject obj)
	{
		Image selectedItemImage = _selectedInventoryItemImages[_inventory.Selected];
		selectedItemImage.enabled = false;
	}

	private void OnSelectedItemChanged(GameObject obj)
	{
		Image selectedItemImage = _selectedInventoryItemImages[_inventory.Selected];
		selectedItemImage.enabled = true;
	}

	private void OnItemAdded(GameObject obj) {
		Image inventoryItemImage = _selectedInventoryItemIcons[_inventory.Selected];
		inventoryItemImage.enabled = true;
		inventoryItemImage.sprite = obj.GetComponent<ItemData>().sprite;
	}

	private void OnItemRemoved() {
		Image inventoryItemImage = _selectedInventoryItemIcons[_inventory.Selected];
		inventoryItemImage.enabled = false;
	}

	private void LoadUIStatisticGameObjects()
	{
		const string COMMON_PATH = "Statistic/Main Panel/";

		GameObject uiObj = GameObject.Find("/UI");
		_testInfoText = uiObj.transform.Find(COMMON_PATH + "Tests Info Text").GetComponent<TextMeshProUGUI>();
		_energyBarTransform = uiObj.transform.Find(COMMON_PATH + "Energy Bar Panel/Energy Bar").GetComponent<RectTransform>();

		RectTransform inventoryItemsParent = uiObj.transform.Find(COMMON_PATH + "Inventory Panel").GetComponent<RectTransform>();
		_selectedInventoryItemImages = new Image[inventoryItemsParent.childCount];
		_selectedInventoryItemIcons = new Image[inventoryItemsParent.childCount];
		for (int i = 0; i < inventoryItemsParent.childCount; ++i)
		{
			_selectedInventoryItemImages[i] = inventoryItemsParent.Find("Inventory Item " + (i + 1).ToString() + "/Selected").GetComponent<Image>();
			_selectedInventoryItemIcons[i] = inventoryItemsParent.Find("Inventory Item " + (i + 1).ToString() + "/Item Icon").GetComponent<Image>();
		}
	}
}
