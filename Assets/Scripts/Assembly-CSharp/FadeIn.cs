using UnityEngine;

public class FadeIn : MonoBehaviour
{
	private void Start()
	{
		TweenAlpha.Begin(base.transform.gameObject, 0.1f, 0f);
		base.transform.gameObject.SetActive(true);
		TweenAlpha.Begin(base.transform.gameObject, Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME, 1f);
	}

	public void StartFadeIn(float time)
	{
		TweenAlpha.Begin(base.transform.gameObject, 0.1f, 0f);
		base.transform.gameObject.SetActive(true);
		TweenAlpha.Begin(base.transform.gameObject, time, 1f);
	}
}
