using UnityEngine;

public class AnimationPoseEffect : MonoBehaviour
{
	private float mTimeSpanLeft;

	private float mCurrUpdateTime;

	private float mPrevUpdateTime;

	private float mStartingOpacity;

	private float mTimeTillDestroy;

	private float mGravity;

	public void InitializeEffect(float opacity, Vector3 position, float fadeOutTime, float destroyTime, float gravity)
	{
		mStartingOpacity = opacity;
		TweenAlpha.Begin(base.transform.gameObject, 0.1f, mStartingOpacity);
		base.transform.localPosition = position;
		TweenAlpha.Begin(base.transform.gameObject, fadeOutTime, 0f);
		if ((bool)(base.transform.GetComponent("UISprite") as UISprite) && (base.transform.GetComponent("UISprite") as UISprite).atlas.name.CompareTo("SharedUIReferenceAtlas") == 0)
		{
			TweenColor.Begin(base.transform.gameObject, fadeOutTime, new Color(Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, Constants.DesginerGlobals.PREMULITPLY_COLOR_FADE, 0f));
		}
		mTimeTillDestroy = destroyTime;
		mGravity = gravity;
		if (mGravity != 0f)
		{
			if (base.transform.GetComponent<Rigidbody>() == null)
			{
				base.transform.gameObject.AddComponent<Rigidbody>();
			}
			if (base.transform.GetComponent<Rigidbody>() != null)
			{
				base.transform.GetComponent<Rigidbody>().useGravity = false;
				base.transform.GetComponent<Rigidbody>().isKinematic = false;
				base.transform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, mGravity, 0f), ForceMode.VelocityChange);
			}
		}
		mPrevUpdateTime = (mCurrUpdateTime = Time.time);
	}

	private void Start()
	{
	}

	private void Update()
	{
		mCurrUpdateTime = Time.time;
		float num = mCurrUpdateTime - mPrevUpdateTime;
		if (!GameManager.Instance.IsGamePaused())
		{
			mTimeSpanLeft -= num;
		}
		if (mTimeSpanLeft <= mTimeTillDestroy)
		{
		}
		mPrevUpdateTime = mCurrUpdateTime;
	}
}
