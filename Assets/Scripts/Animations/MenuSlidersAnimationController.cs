using System;
using System.Collections;
using UnityEngine;

public class MenuSlidersAnimationController : MonoBehaviour
{
	[SerializeField] private GameObject[] _sliderNameParents;
	[SerializeField] private GameObject[] _sliders;

	[SerializeField] protected float _animationTime;
	protected float _time;

	private GameObject _menuUI;

	private static float _hiddenSliderNamePositionX = -1250f;
	private static float _shownSliderNamePositionX = -520f;
	private static float _hiddenSliderPositionX = 1550f;
	private static float _shownSliderPositionX = 200f;

	private static int _objectsAnimated;
	private bool _isCoroutineInProgress;
	private bool _needToSwitchCoroutine;

	void Awake() {
		_menuUI = GameObject.Find("/UI").transform.Find("Menu").gameObject;
	}

	void OnEnable()
	{
		_objectsAnimated = 0;
		_isCoroutineInProgress = false;
		_needToSwitchCoroutine = false;
	}

	public void Show()
	{
		if (_isCoroutineInProgress)
		{
			_time = _animationTime - _time;
			_needToSwitchCoroutine = true;
		}
		else
		{
			StartCoroutine(ShowObjects());
		}
	}

	public void Hide()
	{
		if (_isCoroutineInProgress)
		{
			_time = _animationTime - _time;
			_needToSwitchCoroutine = true;
		}
		else
		{
			_objectsAnimated = _sliders.Length - 1;
			StartCoroutine(HideObjects());
		}
	}

	private IEnumerator ShowObjects()
	{
		_isCoroutineInProgress = true;
		while (_objectsAnimated < _sliders.Length)
		{
			while (_time < _animationTime)
			{
				if (_needToSwitchCoroutine)
				{
					_needToSwitchCoroutine = false;
					StartCoroutine(HideObjects());
					yield break;
				}
				_time += Time.deltaTime;

				MoveObject(
					_sliderNameParents[_objectsAnimated].transform,
					new Vector3(
						_hiddenSliderNamePositionX,
						_sliderNameParents[_objectsAnimated].transform.localPosition.y,
						_sliderNameParents[_objectsAnimated].transform.localPosition.z),
					new Vector3(
						_shownSliderNamePositionX,
						_sliderNameParents[_objectsAnimated].transform.localPosition.y,
						_sliderNameParents[_objectsAnimated].transform.localPosition.z));
				MoveObject(
					_sliders[_objectsAnimated].transform,
					new Vector3(
						_hiddenSliderPositionX,
						_sliders[_objectsAnimated].transform.localPosition.y,
						_sliders[_objectsAnimated].transform.localPosition.z),
					new Vector3(
						_shownSliderPositionX,
						_sliders[_objectsAnimated].transform.localPosition.y,
						_sliders[_objectsAnimated].transform.localPosition.z));

				yield return null;
			}

			++_objectsAnimated;
			_time = 0;
		}

		enabled = false;
	}

	private IEnumerator HideObjects()
	{
		_isCoroutineInProgress = true;
		while (_objectsAnimated >= 0)
		{
			while (_time < _animationTime)
			{
				if (_needToSwitchCoroutine)
				{
					_needToSwitchCoroutine = false;
					StartCoroutine(ShowObjects());
					yield break;
				}
				_time += Time.deltaTime;

				MoveObject(
					_sliderNameParents[_objectsAnimated].transform,
					new Vector3(
						_shownSliderNamePositionX,
						_sliderNameParents[_objectsAnimated].transform.localPosition.y,
						_sliderNameParents[_objectsAnimated].transform.localPosition.z),
					new Vector3(
						_hiddenSliderNamePositionX,
						_sliderNameParents[_objectsAnimated].transform.localPosition.y,
						_sliderNameParents[_objectsAnimated].transform.localPosition.z));
				MoveObject(
					_sliders[_objectsAnimated].transform,
					new Vector3(
						_shownSliderPositionX,
						_sliders[_objectsAnimated].transform.localPosition.y,
						_sliders[_objectsAnimated].transform.localPosition.z),
					new Vector3(
						_hiddenSliderPositionX,
						_sliders[_objectsAnimated].transform.localPosition.y,
						_sliders[_objectsAnimated].transform.localPosition.z));

				yield return null;
			}

			--_objectsAnimated;
			_time = 0;
		}

		_menuUI.SetActive(false);
		enabled = false;
	}

	private void MoveObject(Transform localTransform, Vector3 init, Vector3 target)
	{
		localTransform.localPosition = Vector3.Lerp(init, target, _time / _animationTime);
	}
}