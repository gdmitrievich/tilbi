using UnityEngine;

public class PCAudioController : MonoBehaviour
{
	[SerializeField] private AudioClip[] _oldPcWorkingSounds;
	[SerializeField] private AudioClip _oldPcTurningOff;
	[SerializeField] private AudioClip _beepSound;
	[SerializeField] private AudioClip _psWorkingSound;
	[SerializeField] private bool _isOldPc;

	private AudioSource _audioSource;
	private bool _isTurningOffSoundStartedToPlay;
	private float _time, _timeToFade;

	void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.loop = true;
		if (_isOldPc)
		{
			_audioSource.clip = _oldPcWorkingSounds[Random.Range(0, _oldPcWorkingSounds.Length)];
		}
		else
		{
			_audioSource.clip = _psWorkingSound;
		}

		_isTurningOffSoundStartedToPlay = false;
		_timeToFade = 3f;
		_audioSource.Play();
	}

	void OnEnable()
	{
		PCTestPassingLogic.TestFailed += OnTestFailed;
		PCTestPassingLogic.TestSuccessfullyPassed += OnTestSuccessfullyPassed;
	}

	void OnDisable()
	{
		PCTestPassingLogic.TestFailed -= OnTestFailed;
		PCTestPassingLogic.TestSuccessfullyPassed -= OnTestSuccessfullyPassed;
	}

	private void OnTestFailed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}

		var test = obj.GetComponent<Test>();
		if (!test.IsReplayable || test.IsIncorrect)
		{
			PlayTurningOffSound();
		}
	}

	private void OnTestSuccessfullyPassed(GameObject obj)
	{
		if (obj != gameObject)
		{
			return;
		}
		PlayTurningOffSound();
	}

	void Update()
	{
		if (!_isTurningOffSoundStartedToPlay)
		{
			return;
		}

		if (_isOldPc && !_audioSource.isPlaying)
		{
			_audioSource.enabled = false;
		}
		else if (!_isOldPc)
		{
			_time += Time.deltaTime;
			_audioSource.volume = (_timeToFade - _time) / _timeToFade;
		}

		if (_time >= _timeToFade)
		{
			_audioSource.enabled = false;
		}
	}

	public void PlayBeep()
	{
		_audioSource.PlayOneShot(_beepSound);
	}

	private void PlayTurningOffSound()
	{
		_isTurningOffSoundStartedToPlay = true;
		_audioSource.loop = false;

		if (_isOldPc)
		{
			_audioSource.clip = _oldPcTurningOff;
			_audioSource.Play();
		}
	}
}