using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CheetSheetPanelAnimation : MonoBehaviour
{
	private RectTransform rectTransform;
	private Vector2 _position;

	[SerializeField] private float _time;
	private float _currentTime;
	private bool _isShowing;

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		_position = new Vector2(rectTransform.offsetMax.x, -rectTransform.rect.height);
		rectTransform.offsetMax = _position;
		_currentTime = 0;
		_isShowing = false;
	}

	void Update()
	{
		if (_isShowing)
		{
			PutDown();
		}
		else
		{
			Raise();
		}
	}

	public void Raise()
	{
		if (_currentTime < _time)
		{
			_currentTime += Time.deltaTime;
			rectTransform.offsetMax = Vector2.Lerp(_position, Vector2.zero, _currentTime / _time);
			return;
		}

		_isShowing = true;
		_currentTime = 0;
		enabled = false;
	}

	public void PutDown()
	{
		if (_currentTime < _time)
		{
			_currentTime += Time.deltaTime;
			rectTransform.offsetMax = Vector2.Lerp(Vector2.zero, _position, _currentTime / _time);
			return;
		}

		CheetSheetAnimation cheetSheetAnimation = GameObject.Find("/Characters/Player/Main Camera/RightHandItem").GetComponentInChildren<CheetSheetAnimation>();
		cheetSheetAnimation.Show();
		_isShowing = false;
		_currentTime = 0;
		enabled = false;
	}
}
