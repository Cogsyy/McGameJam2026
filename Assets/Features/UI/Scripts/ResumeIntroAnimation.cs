using UnityEngine;
using System.Collections;
using System;

public class ResumeIntroAnimation : MonoBehaviour
{
	[Header("Interpolation Settings")]
	[SerializeField] private Transform _startTransform;
	[SerializeField] private Transform _deskTransform;
	[SerializeField] private float _delay = 2f;
	[SerializeField] private float _moveDuration = 1.5f;
	[SerializeField] private AnimationCurve _moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	private void Awake()
	{
		if (_startTransform != null)
		{
			transform.position = _startTransform.position;
			transform.rotation = _startTransform.rotation;
		}
	}

	public void PlayAnimationBackwards(Action onComplete)
	{
		StartCoroutine(AnimateResumeBackwards(onComplete));
	}

	public void PlayAnimation(Action onComplete)
	{
		StartCoroutine(AnimateResume(onComplete));
	}

	private IEnumerator AnimateResume(Action onComplete)
	{
		if (_startTransform == null || _deskTransform == null)
		{
			Debug.LogWarning("ResumeIntroAnimation: Missing start or desk transform!");
			yield break;
		}

		// Initial set to start position just in case
		transform.position = _startTransform.position;
		transform.rotation = _startTransform.rotation;

		yield return new WaitForSeconds(_delay);

		float elapsed = 0f;
		Vector3 startPos = _startTransform.position;
		Quaternion startRot = _startTransform.rotation;
		Vector3 targetPos = _deskTransform.position;
		Quaternion targetRot = _deskTransform.rotation;

		while (elapsed < _moveDuration)
		{
			elapsed += Time.deltaTime;
			float t = _moveCurve.Evaluate(elapsed / _moveDuration);
			
			transform.position = Vector3.Lerp(startPos, targetPos, t);
			transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
			
			yield return null;
		}

		transform.position = targetPos;
		transform.rotation = targetRot;

		onComplete?.Invoke();
	}

	private IEnumerator AnimateResumeBackwards(Action onComplete)
	{
		float elapsed = _moveDuration;
		Vector3 startPos = _deskTransform.position;
		Quaternion startRot = _deskTransform.rotation;
		Vector3 targetPos = _startTransform.position;
		Quaternion targetRot = _startTransform.rotation;

		while (elapsed > 0)
		{
			elapsed -= Time.deltaTime;
			float t = _moveCurve.Evaluate(elapsed / _moveDuration);
			
			transform.position = Vector3.Lerp(startPos, targetPos, t);
			transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
			
			yield return null;
		}

		transform.position = targetPos;
		transform.rotation = targetRot;

		onComplete?.Invoke();
	}
}
