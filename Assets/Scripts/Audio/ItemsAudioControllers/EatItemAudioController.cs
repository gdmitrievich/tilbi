using UnityEngine;
public class EatItemAudioController : ItemAudioController {
	[SerializeField] private AudioClip _eatingClip;

	private float _min_pitch;
	private float _max_pitch;
	private float _timeToEat;
	private float _currentTimeToEat;
	private float _intervalTime;
	private float _currentIntervalTime;

	void Update()
	{
		if (_currentTimeToEat >= _timeToEat)
		{
			return;
		}

		if (_currentIntervalTime < _intervalTime)
		{
			_currentIntervalTime += Time.deltaTime;
			_currentTimeToEat += Time.deltaTime;
			return;
		}

		_audioSource.pitch = Random.Range(_min_pitch, _max_pitch);
		_audioSource.PlayOneShot(_eatingClip);
		_currentIntervalTime = 0;
	}

	public void PlayEatingClip(float timeToEat, float interval, float min_pitch = 1, float max_pitch = 1)
	{
		_audioSource.volume = 0.3f;
		_currentTimeToEat = 0;

		_timeToEat = timeToEat;
		_intervalTime = interval;

		_min_pitch = min_pitch;
		_max_pitch = max_pitch;
	}
}