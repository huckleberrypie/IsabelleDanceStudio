using UnityEngine;

public class SuperPoseCollider : MonoBehaviour
{
	private float mTimeSpanLeft = 15f;

	private float mCurrUpdateTime;

	private float mPrevUpdateTime;

	private void Start()
	{
		mPrevUpdateTime = (mCurrUpdateTime = Time.time);
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
		}
		mPrevUpdateTime = mCurrUpdateTime;
	}

	private void OnDestroy()
	{
	}

	public void SetTimeSpan(float lifeSpan)
	{
		mTimeSpanLeft = lifeSpan;
	}
}
