using UnityEngine;

public class FloatingFadedText : MonoBehaviour
{
	private float mTimeSpanLeft = 2f;

	private float mCurrUpdateTime;

	private float mPrevUpdateTime;

	private void Start()
	{
		mPrevUpdateTime = (mCurrUpdateTime = Time.time);
		TweenPosition.Begin(base.transform.gameObject, mTimeSpanLeft, new Vector3(base.transform.localPosition.x, base.transform.localPosition.y + 40f, base.transform.localPosition.z));
		TweenAlpha.Begin(base.transform.gameObject, mTimeSpanLeft, 0f);
	}

	private void Update()
	{
		mCurrUpdateTime = Time.time;
		float num = mCurrUpdateTime - mPrevUpdateTime;
		if (!GameManager.Instance.IsGamePaused())
		{
			mTimeSpanLeft -= num;
		}
		if (mTimeSpanLeft < 0f)
		{
			Object.DestroyImmediate(base.transform.gameObject);
		}
		mPrevUpdateTime = mCurrUpdateTime;
	}

	public void SetTimeSpan(float lifeSpan)
	{
		mTimeSpanLeft = lifeSpan;
	}
}
