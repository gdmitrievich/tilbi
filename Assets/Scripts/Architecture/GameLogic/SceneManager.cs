using System;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	public enum Scene {
		Initial,
		Horror,
		BackRooms,
		Loading
	}

	public static void LoadScene(int scene) {
		SceneManager.LoadScene(scene);
	}
}
