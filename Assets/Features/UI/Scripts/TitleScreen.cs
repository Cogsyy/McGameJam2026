using UnityEngine;
using System;

public class TitleScreen : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private CanvasFader _canvasFader;
    [SerializeField] private Canvas _playerCanvas;
	[SerializeField] private ResumeIntroAnimation _resumeAnimation;
	[SerializeField] private FirstPersonCamera _fpCamera;

	[Header("Audio")]
	[SerializeField] private AudioClip _titleMusic;
	[SerializeField] private AudioClip _gameMusic;

	private bool _hasStarted;

	public static event Action OnGameStart;

	private void Awake()
	{
		_fpCamera.SetMouseLookEnabled(false);
	}

	private void Start()
	{
		if (AudioManager.Instance != null && _titleMusic != null)
		{
			AudioManager.Instance.PlayMusic(_titleMusic);
		}

        _playerCanvas.enabled = false;
	}

	private void Update()
	{
		if (_hasStarted)
		{
			return;
		}

		if (!_hasStarted &&(Input.anyKeyDown || Input.GetMouseButtonDown(0)))
		{
			StartGame();
		}
	}

	private void StartGame()
	{
		_hasStarted = true;

		if (_canvasFader != null)
		{
			if (AudioManager.Instance != null && _gameMusic != null)
			{
				AudioManager.Instance.PlayMusic(_gameMusic, true);
			}

			_canvasFader.FadeToAlpha(0, OnFadeOutComplete);

		}
	}

    private void OnFadeOutComplete()
    {

		if (_resumeAnimation != null)
		{
			_resumeAnimation.PlayAnimation(OnResumeAnimationComplete);
		}
    }

	private void OnResumeAnimationComplete()
	{
		_playerCanvas.enabled = true;
		_fpCamera.SetMouseLookEnabled(true);
		OnGameStart?.Invoke();
	}
}
