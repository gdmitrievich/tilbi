using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class SlideShowController : MonoBehaviour
{
	[SerializeField] private float _timeToSee;
	[SerializeField] private float _transitionTime;
	[SerializeField] private Sprite[] _sprites;

	private Image _currentImage;
	private Image _nextImage;

	private int _spritesCounter;
	private float _time;

	private bool _isGoing;
	private bool _isTransitionPanelAppeared;

	void Awake()
	{
		_currentImage = transform.Find("Current Image").GetComponent<Image>();
		_nextImage = transform.Find("Next Image").GetComponent<Image>();

		_currentImage.sprite = _sprites[0];
		_nextImage.sprite = _sprites[1];

		_time = 0;
		_spritesCounter = 0;

		_isGoing = false;
		_isTransitionPanelAppeared = false;
	}

	void OnEnable() {
		SceneDarknessManager.SceneAppeared += OnSceneAppeared;
	}

	void OnDisable() {
		SceneDarknessManager.SceneAppeared -= OnSceneAppeared;
	}

	private void OnSceneAppeared() {
		_isTransitionPanelAppeared = true;
	}

	void Update()
	{
		if (!_isTransitionPanelAppeared) {
			return;
		}
		if (!_isGoing)
		{
			StartCoroutine(ShowPicture());
		}
	}

	private IEnumerator ShowPicture()
	{
		_isGoing = true;
		yield return new WaitForSeconds(_timeToSee);

		while (_time < _transitionTime)
		{
			_time += Time.deltaTime;

			_currentImage.color = new Color(_currentImage.color.r, _currentImage.color.g, _currentImage.color.b, 1 - _time / _transitionTime);

			yield return null;
		}

		_time = 0;

		_spritesCounter++;
		if (_spritesCounter < _sprites.Length - 1) {
			_currentImage.color = new Color(_currentImage.color.r, _currentImage.color.g, _currentImage.color.b, 1);
			_currentImage.sprite = _sprites[_spritesCounter];
			_nextImage.sprite = _sprites[_spritesCounter + 1];
			_isGoing = false;
		} else {
			SceneDarknessManager.LongFade();
		}

		yield break;
	}
}