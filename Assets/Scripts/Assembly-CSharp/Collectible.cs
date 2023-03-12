using UnityEngine;

public class Collectible : MonoBehaviour
{
	private float mTimeSpanLeft = 5f;

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
			if (base.transform.gameObject.CompareTag("Star"))
			{
				DanceSessionManager.AwardCollectAllStarsBonus = false;
			}
			Object.DestroyImmediate(base.transform.gameObject);
		}
		mPrevUpdateTime = mCurrUpdateTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Flower") && (!(other.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics != null) || !(other.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics).BlackStar))
		{
			if (base.transform.CompareTag("Star"))
			{
				SpawnManager.Instance.AddFlowerBurst(base.transform.gameObject);
				Scoreboard.Instance.AddStars(1);
				AudioManager.Sound.PlaySoundOneShot("hit_sequin_collectstar");
				Scoreboard.Instance.AddPoints(Constants.DesginerGlobals.HIT_STAR_POINT);
				SpawnManager.Instance.AddFloatingPoints(base.GetComponent<Collider>().gameObject.transform.localPosition, string.Empty + Constants.DesginerGlobals.HIT_STAR_POINT);
			}
			Object.Destroy(base.transform.gameObject);
		}
	}

	public void SetTimeSpan(float lifeSpan)
	{
		mTimeSpanLeft = lifeSpan;
	}
}
