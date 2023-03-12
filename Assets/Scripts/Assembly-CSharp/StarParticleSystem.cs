using UnityEngine;

public class StarParticleSystem : MonoBehaviour
{
	private float mTimeSpanLeft;

	private float mCurrUpdateTime;

	private float mPrevUpdateTime;

	private float mFadeOutTime;

	private float mGravity;

	private bool initialized;

	public GameObject starParticle;

	private int starCount;

	public void InitializeEffect(Vector3 position, float fadeOutTime, float destroyTime)
	{
		base.transform.localPosition = position;
		mTimeSpanLeft = destroyTime;
		mFadeOutTime = fadeOutTime;
		mPrevUpdateTime = (mCurrUpdateTime = Time.time);
		initialized = true;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!initialized)
		{
			return;
		}
		mCurrUpdateTime = Time.time;
		float num = mCurrUpdateTime - mPrevUpdateTime;
		mPrevUpdateTime = mCurrUpdateTime;
		if (!GameManager.Instance.IsGamePaused())
		{
			mTimeSpanLeft -= num;
			if (starParticle != null)
			{
				for (int i = 0; i < 2; i++)
				{
					int num2 = starCount % Constants.Sprites.Game.STAR_PARTICLES.Length;
					GameObject gameObject = Object.Instantiate(starParticle) as GameObject;
					gameObject.transform.SetParent(GameManager.FallingContainer);
					if ((bool)(gameObject.GetComponent("UISprite") as UISprite))
					{
						(gameObject.GetComponent("UISprite") as UISprite).spriteName = Constants.Sprites.Game.STAR_PARTICLES[num2];
					}
					if ((bool)(gameObject.GetComponent("StarParticle") as StarParticle))
					{
						(gameObject.GetComponent("StarParticle") as StarParticle).InitializeEffect(base.transform.localPosition, mFadeOutTime, mFadeOutTime);
					}
					starCount++;
				}
			}
		}
		if (mTimeSpanLeft <= 0f)
		{
			Object.Destroy(base.transform.gameObject);
		}
	}
}
