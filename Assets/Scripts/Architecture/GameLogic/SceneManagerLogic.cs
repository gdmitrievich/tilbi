using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerLogic : MonoBehaviour
{
	public enum Scene {
		Initial,
		Horror,
		BackRooms
	}

	void OnEnable() {
		UITestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		UITestPassingLogic.TestFailed += OnTestFailed;
	}

	void OnDisable() {
		UITestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		UITestPassingLogic.TestFailed -= OnTestFailed;
	}

	private void OnTestSuccessfullyPassed(GameObject obj) {
		if (PlayerPrefs.GetInt("PassedTests") == 3) {
			Load((int) Scene.BackRooms);
		}
	}

	private void OnTestFailed(GameObject obj) {
		if (PlayerPrefs.GetInt("PassedTests") == 1) {
			Load((int) Scene.Horror);
		}
	}

	private void Load(int scene) {
		SceneManager.LoadScene(scene);
	}
}
