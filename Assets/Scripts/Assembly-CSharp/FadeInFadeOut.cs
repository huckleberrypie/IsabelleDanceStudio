using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
	private float mTimeSpanLeft;

	private float mCurrUpdateTime;

	private float mPrevUpdateTime;

	private bool mFadingOut;

	private bool mFadingIn;

	private float mFadeInInterval = 1f;

	private float mFadeOutInterval = 2f;

	private bool mRemoveScriptAfter;

	private bool mAllowStart;

	public bool RemoveScriptAfter
	{
		get
		{
			return mRemoveScriptAfter;
		}
		set
		{
			mRemoveScriptAfter = value;
		}
	}

	public bool AllowStart
	{
		get
		{
			return mAllowStart;
		}
		set
		{
			mAllowStart = value;
		}
	}

	private void Start()
	{
		TweenAlpha.Begin(base.transform.gameObject, 0.1f, 0f);
		mPrevUpdateTime = (mCurrUpdateTime = Time.time);
	}

	private void Update()
	{
		if (mAllowStart)
		{
			if (!mFadingIn)
			{
				mFadingIn = true;
				TweenAlpha.Begin(base.transform.gameObject, mFadeInInterval, 1f);
			}
			mCurrUpdateTime = Time.time;
			float num = mCurrUpdateTime - mPrevUpdateTime;
			if (!GameManager.Instance.IsGamePaused())
			{
				mTimeSpanLeft -= num;
			}
			if (mTimeSpanLeft <= mFadeOutInterval && !mFadingOut)
			{
				mFadingOut = true;
				TweenAlpha.Begin(base.transform.gameObject, mFadeOutInterval, 0f);
			}
			mPrevUpdateTime = mCurrUpdateTime;
			if (mTimeSpanLeft < 0f && mRemoveScriptAfter)
			{
				Object.Destroy(base.transform.gameObject.GetComponent("FadeInFadeOut"));
			}
		}
	}

	public void SetTimeSpan(float lifeSpan)
	{
		mTimeSpanLeft = lifeSpan;
	}

	public void SetIntervals(float fadeIn, float fadeOut)
	{
		mFadeInInterval = fadeIn;
		mFadeOutInterval = fadeOut;
	}
}
