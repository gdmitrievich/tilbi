using UnityEngine;

public class InitialSetup : MonoBehaviour {
	void Awake() {
		PlayerPrefs.SetInt("PassedTests", 0);
	}
}