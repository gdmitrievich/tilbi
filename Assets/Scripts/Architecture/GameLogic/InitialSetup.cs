using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSetup : MonoBehaviour
{
	void Awake()
	{
		if (SceneManager.GetActiveScene().buildIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			PlayerPrefsManager.Init();
			PlayerPrefsManager.prefs.SetInt("PassedTests", 0);
			PlayerPrefsManager.prefs.SetInt("IsTilbiAngry", 0);
			PlayerPrefsManager.prefs.Save();

		}
	}

	void Start()
	{
		StopGameLogic.LoadObjects();
		PlayerKeyboardInteractionController.Load();
	}
}