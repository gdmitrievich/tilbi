using UnityEngine;
public static class PlayerPrefsManager {
	public static JsonPlayerPrefs prefs;
	public static void Init() {
		prefs = new JsonPlayerPrefs(Application.persistentDataPath + "/Preferences.json");
		Debug.Log("PlayerPrefs have Loaded");
	}
}