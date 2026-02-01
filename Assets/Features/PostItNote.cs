using UnityEngine;
using System.Collections;

public class PostItNote : MonoBehaviour
{
	[SerializeField] private float _animationDuration = 0.3f;
	[SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	private void OnEnable()
	{
		StartCoroutine(ScaleInRoutine());
	}

	private IEnumerator ScaleInRoutine()
	{
        yield return new WaitForSeconds(1f);
		float elapsed = 0f;
		Vector3 targetScale = Vector3.one;
		
		// Start with 0 scale on X and Y
		transform.localScale = new Vector3(0f, 0f, 1f);

		while (elapsed < _animationDuration)
		{
			elapsed += Time.deltaTime;
			float t = _scaleCurve.Evaluate(elapsed / _animationDuration);
			
			float currentScale = Mathf.Lerp(0f, 1f, t);
			transform.localScale = new Vector3(currentScale, currentScale, 1f);
			
			yield return null;
		}

		transform.localScale = targetScale;
	}
}
