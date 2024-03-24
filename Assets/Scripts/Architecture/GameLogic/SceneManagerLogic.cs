using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerLogic : MonoBehaviour
{
	public enum Scene
	{
		Loading,
		Initial,
		Horror,
		BackRooms
	}

	private static Scene _sceneToLoad;

	void Start()
	{
		_sceneToLoad = Scene.Initial;
	}

	private static Action LoaderCallbacked;

	void OnEnable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed += OnTestFailed;

		PlayerCollisionListener.PlayerCatched += OnPlayerCatched;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
		PCTestPassingLogic.TestFailed -= OnTestFailed;

		PlayerCollisionListener.PlayerCatched -= OnPlayerCatched;
	}

	private void OnTestSuccessfullyPassed(GameObject obj)
	{
		if (PlayerPrefs.GetInt("PassedTests") == 4) {
			//Load(Scene.BackRooms);
			_sceneToLoad = Scene.BackRooms;
			SceneDarknessManager.Fade();
		}
	}

	private void OnTestFailed(GameObject obj)
	{
		if (PlayerPrefs.GetInt("PassedTests") == 1) {
			//Load(Scene.Horror);
			_sceneToLoad = Scene.Horror;
			SceneDarknessManager.Fade();
		}
	}

	private void OnPlayerCatched()
	{
		// Load(Scene.Initial);
		_sceneToLoad = Scene.Initial;
	}

	public static void Load()
	{
		LoaderCallbacked = () =>
		{
			// SceneManager.LoadScene((int)scene);
			SceneManager.LoadScene((int)_sceneToLoad);
		};

		SceneManager.LoadScene((int)Scene.Loading);
	}

	public static void LoaderCallback()
	{
		if (LoaderCallbacked != null)
		{
			LoaderCallbacked();
			LoaderCallbacked = null;
		}
	}
}
