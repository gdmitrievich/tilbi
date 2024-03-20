using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneDarknessManager : MonoBehaviour
{
	private static Animator _animator;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_animator.SetBool("IsAppearing", true);
	}

	public static void Appear() {
		_animator.SetBool("IsAppearing", true);
	}

	public static void Fade() {
		_animator.SetBool("IsAppearing", false);
	}
}
