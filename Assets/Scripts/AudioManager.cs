using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	public static AudioManager Singleton { get; set; }

	public AudioClip _soundtrackIntro;
	public AudioClip _soundtrack;
	public AudioClip _gameOver;
	public AudioClip _crateCollect;
	public AudioClip _crateScore;
	public AudioClip _shipCrash;
	public AudioClip _engine;

	public AudioSource _audioSourceEffects;
	public AudioSource _audioSourceEngine;
	AudioSource _audioSourceSoundtrack;

	float _startTime;

	void Awake()
	{
		if (Singleton == null)
		{
			Singleton = this;
		}
		else
		{
			throw new System.InvalidOperationException("Cannot create another instance of the 'AudioManager' class");
		}

		_audioSourceSoundtrack = GetComponent<AudioSource>();
	}

	void Start()
	{
		_audioSourceSoundtrack.clip = _soundtrackIntro;
		_audioSourceSoundtrack.Play();

		_audioSourceEngine.clip = _engine;
		_audioSourceEngine.loop = true;
		_audioSourceEngine.Play();

		_audioSourceEffects.loop = false;

		_startTime = Time.time;
	}

	void Update()
	{
		_audioSourceEngine.pitch = .75f + ShipManager.Singleton.Speed * .0025f;

		if (_audioSourceSoundtrack.clip == _soundtrack)
		{
			return;
		}

		if (Time.time >= _startTime + _soundtrackIntro.length)
		{
			_audioSourceSoundtrack.clip = _soundtrack;
			_audioSourceSoundtrack.Play();
			_audioSourceSoundtrack.loop = true;
		}
	}

	public void OnGameOver()
	{
		print("Game Over, play game over sound");
		_audioSourceSoundtrack.clip = _gameOver;
		_audioSourceSoundtrack.Play();
		_audioSourceSoundtrack.loop = false;
	}

	public void PlayEffect(AudioClip clip)
	{
		_audioSourceEffects.clip = clip;
		_audioSourceEffects.Play();
	}
}
