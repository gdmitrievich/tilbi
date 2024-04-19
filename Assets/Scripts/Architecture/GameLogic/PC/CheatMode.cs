using UnityEngine;

public class CheatMode : MonoBehaviour {
	private PCTestPassingLogic _pCTestPassingLogic;
	private Test _test;
	void Awake() {
		_pCTestPassingLogic = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<PCTestPassingLogic>();
	}

	void Update()
	{
		if (_test == null) {
			return;
		}
		if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.T))
		{
			_test.CorrectlyAnsweredQuestionAnswers = _test.TotalNumberOfCorrectAnswersOfQuestions;
			_pCTestPassingLogic.TestPassed();
			enabled = false;
		}
	}

	public void StartCheatMode(Test test) => _test = test;
}