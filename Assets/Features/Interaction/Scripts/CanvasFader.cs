using UnityEngine;
using System.Collections;
using System;

public class CanvasFader : MonoBehaviour
{
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private float _duration = 1f;

	private Coroutine _fadeCoroutine;

	private void Awake()
	{
		if (_canvasGroup == null)
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}
	}

	public void FadeToAlpha(float targetAlpha, Action onComplete = null)
	{
		if (_fadeCoroutine != null)
		{
			StopCoroutine(_fadeCoroutine);
		}
		
		_fadeCoroutine = StartCoroutine(FadeAlphaRoutine(targetAlpha, onComplete));
	}

	private IEnumerator FadeAlphaRoutine(float targetAlpha, Action onComplete)
	{
		if (_canvasGroup == null)
		{
			yield break;
		}

		float startAlpha = _canvasGroup.alpha;
		float elapsed = 0f;

		while (elapsed < _duration)
		{
			elapsed += Time.deltaTime;
			_canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / _duration);
			yield return null;
		}

		_canvasGroup.alpha = targetAlpha;
		_fadeCoroutine = null;
		onComplete?.Invoke();
	}
}
