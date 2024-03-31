using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneDarknessManager : MonoBehaviour
{
	private static Animator _animator;

	public static Action SceneAppeared;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_animator.SetBool("IsAppearing", true);
	}

	public static void Appear()
	{
		_animator.SetBool("IsAppearing", true);
	}

	public static void Fade()
	{
		_animator.SetBool("IsAppearing", false);
	}

	public static void LongFade()
	{
		_animator.SetTrigger("LongFading");
	}

	private void OnSceneFaded()
	{
		SceneManagerLogic.Load();
	}

	private void OnSceneAppeared() {
		SceneAppeared?.Invoke();
	}

	private void OnSceneLongFaded() {
		Application.Quit();
	}
}
