using UnityEngine;

public class AttachmentAnimation : MonoBehaviour
{
	private float mCurrLength;

	private float mStartTime;

	private float mInterval;

	private float mStartRot;

	private float mEndRot;

	private float mAnimationLength;

	private bool mRunning;

	public float StartRotation
	{
		get
		{
			return mStartRot;
		}
		set
		{
			mStartRot = value;
		}
	}

	public float EndRotation
	{
		get
		{
			return mEndRot;
		}
		set
		{
			mEndRot = value;
		}
	}

	public float AnimationLength
	{
		get
		{
			return mAnimationLength;
		}
		set
		{
			mAnimationLength = value;
		}
	}

	public bool Running
	{
		get
		{
			return mRunning;
		}
	}

	public void StartAnimation()
	{
		mRunning = true;
		mStartTime = Time.time;
		mCurrLength = 0f;
		mInterval = (mEndRot - mStartRot) / mAnimationLength;
		base.transform.localEulerAngles = new Vector3(0f, 0f, mStartRot);
	}

	public void EndAnimation()
	{
		mRunning = false;
		mCurrLength = 0f;
		base.transform.localEulerAngles = new Vector3(0f, 0f, mStartRot);
	}

	private void Update()
	{
		if (mRunning)
		{
			float num = Time.time - mStartTime;
			mCurrLength += num;
			if (mCurrLength < mAnimationLength)
			{
				mInterval = (mEndRot - mStartRot) * (1f - (mAnimationLength - mCurrLength));
				base.transform.localEulerAngles = new Vector3(0f, 0f, mInterval);
			}
			else
			{
				EndAnimation();
			}
		}
	}
}
