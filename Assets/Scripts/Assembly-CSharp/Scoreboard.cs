using UnityEngine;

public class Scoreboard : MonoBehaviour
{
	private int mCollectedStars;

	private int mCollectedPoints;

	private int mFlowersHit;

	private float mCurrentGameTime;

	private float mLastUpdateTime;

	private bool mAnnouncedNewBestScore;

	private static Scoreboard _instance;

	public int CollectedStars
	{
		get
		{
			return mCollectedStars;
		}
	}

	public int CollectedPoints
	{
		get
		{
			return mCollectedPoints;
		}
	}

	public int FlowersHit
	{
		get
		{
			return mFlowersHit;
		}
	}

	public float CurrentGameTime
	{
		get
		{
			return mCurrentGameTime;
		}
	}

	public bool AnnouncedNewBestScore
	{
		get
		{
			return mAnnouncedNewBestScore;
		}
		set
		{
			mAnnouncedNewBestScore = value;
		}
	}

	public static Scoreboard Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(Scoreboard)) as Scoreboard;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<Scoreboard>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
					_instance.name = "Scoreboard";
				}
			}
			return _instance;
		}
	}

	public void ResetScoreboard()
	{
		mCollectedStars = 0;
		mCollectedPoints = 0;
		mFlowersHit = 0;
		mCurrentGameTime = 0f;
		mLastUpdateTime = Time.time;
		GameManager.Instance.NewBestScore = false;
		mAnnouncedNewBestScore = false;
	}

	private void Update()
	{
		if (!GameManager.Instance.HasGameStarted() || GameManager.Instance.IsGamePaused())
		{
			mLastUpdateTime = Time.time;
			return;
		}
		float num = Time.time - mLastUpdateTime;
		mCurrentGameTime += num;
		mLastUpdateTime = Time.time;
	}

	public void AddStars(int numStars)
	{
		if (DanceSessionManager.CollectedDoubleStar)
		{
			numStars *= 2;
		}
		mCollectedStars += numStars;
	}

	public void AddPoints(int points)
	{
		if (DanceSessionManager.SelectedMulitplierAmount != 0)
		{
			points *= DanceSessionManager.SelectedMulitplierAmount;
		}
		mCollectedPoints += points;
		if (mCollectedPoints > PlayerManager.Instance.BestScore && !mAnnouncedNewBestScore)
		{
			AudioManager.Sound.PlaySoundOneShot("achievement_newbest");
			mAnnouncedNewBestScore = true;
		}
	}

	public void AddFlowersHit(int hit)
	{
		mFlowersHit += hit;
	}
}
