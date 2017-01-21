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

	AudioSource _audioSource;

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

		_audioSource = GetComponent<AudioSource>();
	}

	void Start()
	{
		_audioSource.clip = _soundtrackIntro;
		_audioSource.Play();

		_startTime = Time.time;
	}

	void Update()
	{
		if (_audioSource.clip == _soundtrack)
		{
			return;
		}

		if (Time.time >= _startTime + _soundtrackIntro.length)
		{
			_audioSource.clip = _soundtrack;
			_audioSource.Play();
			_audioSource.loop = true;
		}
	}

	void OnEnable()
	{
		GameManager.Instance.OnGameOver += OnGameOver;
	}

	void OnDisable()
	{
		GameManager.Instance.OnGameOver -= OnGameOver;
	}

	public void OnGameOver()
	{
		print("Game Over, play game over sound");
		_audioSource.clip = _gameOver;
		_audioSource.Play();
		_audioSource.loop = false;
	}
}
