using UnityEngine;

public class InitialSetup : MonoBehaviour {
	private PCTestPassingLogic _pCTestPassingLogic;
	private UITestRenderer _uITestRenderer;

	void Awake() {
		PlayerPrefs.SetInt("PassedTests", 0);
		_pCTestPassingLogic = GetComponent<PCTestPassingLogic>();
		_uITestRenderer = GetComponent<UITestRenderer>();
	}

	void Start() {
		StopGameLogic.LoadObjects();
		_pCTestPassingLogic.Init();
		_uITestRenderer.Init();
	}
}