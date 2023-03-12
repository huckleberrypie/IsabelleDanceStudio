using UnityEngine;

public class FadeInOutLoop : MonoBehaviour
{
	private float mNextStage;

	private bool mFadingOut;

	private bool mFadingIn = true;

	private float mFadeIntervals = 0.7f;

	private void Update()
	{
		mNextStage -= Time.deltaTime;
		if (!(mNextStage <= 0f))
		{
			return;
		}
		if (mFadingIn)
		{
			mFadingIn = false;
			mFadingOut = true;
			mNextStage = mFadeIntervals;
			TweenAlpha.Begin(base.transform.gameObject, mFadeIntervals, 0f);
			if ((bool)(base.transform.GetComponent("UISprite") as UISprite) && (base.transform.GetComponent("UISprite") as UISprite).atlas.name.CompareTo("SharedUIReferenceAtlas") == 0)
			{
				TweenColor.Begin(base.transform.gameObject, mFadeIntervals, new Color(1f, 1f, 1f, 1f));
			}
		}
		else if (mFadingOut)
		{
			mFadingIn = true;
			mFadingOut = false;
			mNextStage = mFadeIntervals;
			TweenAlpha.Begin(base.transform.gameObject, mFadeIntervals, 1f);
			if ((bool)(base.transform.GetComponent("UISprite") as UISprite) && (base.transform.GetComponent("UISprite") as UISprite).atlas.name.CompareTo("SharedUIReferenceAtlas") == 0)
			{
				TweenColor.Begin(base.transform.gameObject, mFadeIntervals, new Color(Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, 0.1f));
			}
		}
	}

	public void SetFadeTime(float intervals)
	{
		mFadeIntervals = intervals;
	}
}
