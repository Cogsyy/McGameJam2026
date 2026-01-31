using UnityEngine;

public class TitleScreen : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private CanvasFader _canvasFader;
    [SerializeField] private Canvas _playerCanvas;

	[Header("Audio")]
	[SerializeField] private AudioClip _titleMusic;
	[SerializeField] private AudioClip _gameMusic;

	private bool _hasStarted;

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
			_canvasFader.FadeToAlpha(0, OnFadeOutComplete);
		}
	}

    private void OnFadeOutComplete()
    {
        if (AudioManager.Instance != null && _gameMusic != null)
		{
			AudioManager.Instance.PlayMusic(_gameMusic, true);
		}
        
        _playerCanvas.enabled = true;
    }
}
