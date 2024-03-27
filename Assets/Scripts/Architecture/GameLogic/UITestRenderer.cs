using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITestRenderer : MonoBehaviour
{
	public Color AnsweredTestNumberColor { get; private set; }
	public Color CurrentTestNumberColor { get; private set; }
	private Color _previousBtnColor;
	public Color PreviousBtnColor
	{
		set => _previousBtnColor = value;
	}
	private Transform _testCanvas;
	private TextMeshProUGUI _questionText;
	private GameObject _answersParent;
	public GameObject AnswersParent
	{
		get => _answersParent;
	}
	private ToggleGroup _answersParentToggleGroup;
	public ToggleGroup AnswersParentToggleGroup
	{
		get => _answersParentToggleGroup;
	}
	private GameObject _testNumbersParent;

	[SerializeField] private GameObject _answerPrefab;
	[SerializeField] private GameObject _testNumberPrefab;

	private TextMeshProUGUI _scoreText;
	private Button _acceptBtn;

	private Image _tilbiImage;
	private Sprite[] _tilbiMoodSprites;
	private int _tilbiMoodIndex;
	private const int _TILBI_MOODS_SPRITES_COUNT = 8;
	public int TilbiMoodIndex {
		get => _tilbiMoodIndex;
		set {
			if (value >= 0 && value < _TILBI_MOODS_SPRITES_COUNT) {
				_tilbiMoodIndex = value;
			}
		}
	}

	private PCTestPassingLogic _pCTestPassingLogic;
	void Awake()
	{
		_pCTestPassingLogic = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<PCTestPassingLogic>();

		AnsweredTestNumberColor = new Color(
			Utility.GetPercentage(44, 255),
			Utility.GetPercentage(70, 255),
			Utility.GetPercentage(200, 255),
			1);
		CurrentTestNumberColor = new Color(
			Utility.GetPercentage(83, 255),
			1,
			Utility.GetPercentage(214, 255),
			1);

		LoadTilbiMoodSprites();
	}

	public void InitialSetup()
	{
		LoadUITestGameObjects();
		_testCanvas.gameObject.SetActive(true);
		_scoreText.text = "0 %";

		_tilbiMoodIndex = 3;
		UpdateTilbiImage(_tilbiMoodIndex);

		_answersParentToggleGroup = _answersParent.GetComponent<ToggleGroup>();

		if (_testNumbersParent.transform.childCount > 0)
		{
			Utility.DestroyChildrens(_testNumbersParent.transform);
		}

		_previousBtnColor = CurrentTestNumberColor;

		// _pCTestPassingLogic.OnAcceptButtonClicked
		_acceptBtn.onClick.AddListener(_pCTestPassingLogic.OnAcceptButtonClicked);
	}

	private void LoadUITestGameObjects()
	{
		const string COMMON_PATH = "Test/Background/Main Panel/";

		GameObject uiObj = GameObject.Find("/UI");
		_testCanvas = uiObj.transform.Find("Test").GetComponent<Transform>();
		_questionText = uiObj.transform.Find(COMMON_PATH + "Header Panel/Question Text").gameObject.GetComponent<TextMeshProUGUI>();
		_answersParent = uiObj.transform.Find(COMMON_PATH + "Body Panel/Answers Panel/Answers").gameObject;
		_testNumbersParent = uiObj.transform.Find(COMMON_PATH + "Footer Panel/Test Numbers Panel/Test Numbers").gameObject;
		_scoreText = uiObj.transform.Find(COMMON_PATH + "Body Panel/Right Panel/Score Text").gameObject.GetComponent<TextMeshProUGUI>();
		_acceptBtn = uiObj.transform.Find(COMMON_PATH + "Footer Panel/Accept Button Panel/Accept Button").gameObject.GetComponent<Button>();
		_tilbiImage = uiObj.transform.Find(COMMON_PATH + "Body Panel/Right Panel/Tilbi Panel").gameObject.GetComponent<Image>();
	}

	private void LoadTilbiMoodSprites()
	{
		_tilbiMoodSprites = new Sprite[_TILBI_MOODS_SPRITES_COUNT];
		for (int i = 0; i < _tilbiMoodSprites.Length; ++i) {
			_tilbiMoodSprites[i] = Resources.Load("Sprites/BaldiMoods/baldi_mood_" + i.ToString(), typeof(Sprite)) as Sprite;
		}
	}

	public void UpdateTilbiImage(int tilbiMoodIndex) {
		_tilbiImage.sprite = _tilbiMoodSprites[tilbiMoodIndex];
	}

	public void TestPassed()
	{
		_testCanvas.gameObject.SetActive(false);
		_acceptBtn.onClick.RemoveListener(_pCTestPassingLogic.OnAcceptButtonClicked);
	}

	public void SetImageColor(Image image, in Color color)
	{
		image.color = color;
	}

	public void DestroyAnswersParent()
	{
		if (_answersParent.transform.childCount > 0)
		{
			Utility.DestroyChildrens(_answersParent.transform);
		}
	}

	public void SetQuestionText(string question)
	{
		_questionText.text = question;
	}

	public void ChangeNumberColor(int previousTestNmb, int testNmb)
	{
		SetImageColor(_testNumbersParent.transform.Find(previousTestNmb.ToString()).gameObject.GetComponent<Image>(), _previousBtnColor);
		Image selectedTestNumberImage = _testNumbersParent.transform.Find(testNmb.ToString())?.gameObject.GetComponent<Image>();
		if (selectedTestNumberImage != null)
		{
			SetImageColor(selectedTestNumberImage, CurrentTestNumberColor);
		}

		_previousBtnColor = Color.white;
	}

	public void ChangeNumberTextColor(int testNmb, Color color)
	{
		GameObject selectedTestNumber = _testNumbersParent.transform.Find(testNmb.ToString())?.gameObject;
		if (selectedTestNumber != null)
		{
			selectedTestNumber.GetComponentInChildren<TextMeshProUGUI>().color = color;
		}
	}

	public void SetScoreText(string text)
	{
		_scoreText.text = text;
	}

	public Transform GetSelectedTestNumberTransform(int testNmb)
	{
		return _testNumbersParent.transform.Find(testNmb.ToString());
	}

	public void SetRadioButtonAnswers(in Test.TestItem testItem)
	{
		_answersParentToggleGroup.enabled = true;

		for (int i = 0; i < testItem.answers.Count; ++i)
		{
			GameObject answerPrefab = Instantiate(_answerPrefab);
			answerPrefab.transform.SetParent(_answersParent.transform, false);
			answerPrefab.name = i.ToString();

			Toggle answerPrefabToggle = answerPrefab.GetComponent<Toggle>();
			answerPrefabToggle.group = _answersParentToggleGroup;
			answerPrefabToggle.isOn = false;

			answerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = testItem.answers[i];
		}
	}

	public void SetCheckBoxAnswers(in Test.TestItem testItem)
	{
		_answersParentToggleGroup.enabled = false;

		for (int i = 0; i < testItem.answers.Count; ++i)
		{
			GameObject answerPrefab = Instantiate(_answerPrefab, _answersParent.transform, false);
			answerPrefab.GetComponentInChildren<TextMeshProUGUI>().text = testItem.answers[i];
			answerPrefab.name = i.ToString();
		}
	}

	public void LoadTestNumbers(int numberOfQuestions)
	{
		for (int i = 0; i < numberOfQuestions; ++i)
		{
			GameObject testNumberPrefab = Instantiate(_testNumberPrefab);
			testNumberPrefab.transform.SetParent(_testNumbersParent.transform, false);
			testNumberPrefab.name = i.ToString();

			Button testNumberBtn = testNumberPrefab.GetComponentInChildren<Button>();
			testNumberBtn.onClick.AddListener(() => _pCTestPassingLogic.LoadTestItem(Convert.ToInt32(testNumberPrefab.name)));

			testNumberPrefab.GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
		}
	}
}
