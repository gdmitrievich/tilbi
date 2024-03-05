using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneReloadingPanelLogic : MonoBehaviour
{
	[SerializeField] private float _time;
	public float MyTime {
		get => _time;
	}
	private float _currentTime;
	private Image _blackPanelImage;

	void Awake()
	{
		_blackPanelImage = gameObject.transform.Find("/UI/Loading/Loading Panel").gameObject.GetComponent<Image>();
		_currentTime = _time;
	}

	void Update()
	{
		if (_currentTime > 0)
		{
			_currentTime -= Time.deltaTime;
			_blackPanelImage.color = new Color(_blackPanelImage.color.r, _blackPanelImage.color.g, _blackPanelImage.color.b, Utility.GetPercentage(_currentTime, _time));
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void SetTime(float time)
	{
		_time = time;
	}
}
