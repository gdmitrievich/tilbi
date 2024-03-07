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

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	void OnEnable() {
		PCTestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed += OnTestFailed;

		PlayerCollisionListener.PlayerCatched += OnPlayerCatched;
	}

	void OnDisable() {
		PCTestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed -= OnTestFailed;

		PlayerCollisionListener.PlayerCatched -= OnPlayerCatched;
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

	private void OnPlayerCatched() {
		Load((int) Scene.Initial);
	}

	private void Load(int scene) {
		SceneManager.LoadScene(scene);
	}
}
