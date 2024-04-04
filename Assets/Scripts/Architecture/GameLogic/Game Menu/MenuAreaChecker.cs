using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAreaChecker : MonoBehaviour
{
	private MenuSlidersAnimationController _menuSlidersAnimationController;
	private GameObject _menuUI;

	void Awake()
	{
		_menuUI = GameObject.Find("/UI").transform.Find("Menu").gameObject;
		_menuSlidersAnimationController = GameObject.FindGameObjectWithTag("GameLogicScripts").GetComponent<MenuSlidersAnimationController>();
	}
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag != "Player")
		{
			return;
		}
		Cursor.lockState = CursorLockMode.None;
		_menuSlidersAnimationController.enabled = true;
		_menuUI.SetActive(true);
		_menuSlidersAnimationController.Show();
	}

	private void OnTriggerExit(Collider collider)
	{
		if (collider.tag != "Player")
		{
			return;
		}
		Cursor.lockState = CursorLockMode.Locked;
		_menuSlidersAnimationController.enabled = true;
		_menuSlidersAnimationController.Hide();
	}
}
