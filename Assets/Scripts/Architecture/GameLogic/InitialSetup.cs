using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSetup : MonoBehaviour
{
	void Awake()
	{
		if (SceneManager.GetActiveScene().buildIndex == (int)SceneManagerLogic.Scene.Initial)
		{
			JsonPlayerPrefs prefs = new JsonPlayerPrefs(Application.persistentDataPath + "/Preferences.json");
			prefs.SetInt("PassedTests", 0);
			prefs.SetInt("IsTilbiAngry", 0);
			prefs.Save();
		}
	}

	void Start()
	{
		StopGameLogic.LoadObjects();
		PlayerKeyboardInteractionController.Load();
	}
}