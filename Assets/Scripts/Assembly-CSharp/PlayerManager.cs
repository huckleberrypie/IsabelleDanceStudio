using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	private int mNumStars;

	private int mPoints;

	private int mBestScore;

	private bool mSeenGems;

	private bool mSeenBlackPearl;

	private bool mSeenSuperPose;

	private bool mSeenBounceUp;

	private bool mSeenMultiplier;

	private bool mSeenDouble;

	private bool mWillNeedToSave;

	private static PlayerManager _instance;

	public int Stars
	{
		get
		{
			return mNumStars;
		}
	}

	public int Points
	{
		get
		{
			return mPoints;
		}
	}

	public int BestScore
	{
		get
		{
			return mBestScore;
		}
	}

	public bool SeenGems
	{
		get
		{
			return mSeenGems;
		}
		set
		{
			mSeenGems = value;
		}
	}

	public bool SeenBlackPearl
	{
		get
		{
			return mSeenBlackPearl;
		}
		set
		{
			mSeenBlackPearl = value;
		}
	}

	public bool SeenSuperPose
	{
		get
		{
			return mSeenSuperPose;
		}
		set
		{
			mSeenSuperPose = value;
		}
	}

	public bool SeenBounceUp
	{
		get
		{
			return mSeenBounceUp;
		}
		set
		{
			mSeenBounceUp = value;
		}
	}

	public bool SeenMultiplier
	{
		get
		{
			return mSeenMultiplier;
		}
		set
		{
			mSeenMultiplier = value;
		}
	}

	public bool SeenDouble
	{
		get
		{
			return mSeenDouble;
		}
		set
		{
			mSeenDouble = value;
		}
	}

	public static PlayerManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<PlayerManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (PlayerPrefs.HasKey(Constants.SaveLoad.BEST_SCORE_KEY))
		{
			mBestScore = PlayerPrefs.GetInt(Constants.SaveLoad.BEST_SCORE_KEY);
		}
		if (PlayerPrefs.HasKey(Constants.SaveLoad.GEM_INTRO_KEY))
		{
			mSeenGems = ((PlayerPrefs.GetInt(Constants.SaveLoad.GEM_INTRO_KEY) != 0) ? true : false);
		}
		if (PlayerPrefs.HasKey(Constants.SaveLoad.BLACK_PEARL_TUT_KEY))
		{
			mSeenBlackPearl = ((PlayerPrefs.GetInt(Constants.SaveLoad.BLACK_PEARL_TUT_KEY) != 0) ? true : false);
		}
		if (PlayerPrefs.HasKey(Constants.SaveLoad.SUPERPOSE_TUT_KEY))
		{
			mSeenSuperPose = ((PlayerPrefs.GetInt(Constants.SaveLoad.SUPERPOSE_TUT_KEY) != 0) ? true : false);
		}
		if (PlayerPrefs.HasKey(Constants.SaveLoad.BOUNCEUP_TUT_KEY))
		{
			mSeenBounceUp = ((PlayerPrefs.GetInt(Constants.SaveLoad.BOUNCEUP_TUT_KEY) != 0) ? true : false);
		}
		if (PlayerPrefs.HasKey(Constants.SaveLoad.MULTIPLIER_TUT_KEY))
		{
			mSeenMultiplier = ((PlayerPrefs.GetInt(Constants.SaveLoad.MULTIPLIER_TUT_KEY) != 0) ? true : false);
		}
		if (PlayerPrefs.HasKey(Constants.SaveLoad.DOUBLE_TUT_KEY))
		{
			mSeenDouble = ((PlayerPrefs.GetInt(Constants.SaveLoad.DOUBLE_TUT_KEY) != 0) ? true : false);
		}
		if (!mSeenGems || !mSeenBlackPearl || !mSeenSuperPose || !mSeenBounceUp || !mSeenMultiplier || !mSeenDouble)
		{
			mWillNeedToSave = true;
		}
	}

	public bool isFLowerUnlocked(int flowerID)
	{
		return true;
	}

	public bool isBestScore(int points)
	{
		if (mBestScore >= points)
		{
			return false;
		}
		mBestScore = points;
		GameManager.Instance.NewBestScore = true;
		PlayerPrefs.SetInt(Constants.SaveLoad.BEST_SCORE_KEY, mBestScore);
		PlayerPrefs.Save();
		return true;
	}

	public void TipUnlocked(Enums.GameTip tip)
	{
		switch (tip)
		{
		case Enums.GameTip.BLACK_PEARL:
			mSeenBlackPearl = true;
			break;
		case Enums.GameTip.BOUNCE_UP:
			mSeenBounceUp = true;
			break;
		case Enums.GameTip.DOUBLE:
			mSeenDouble = true;
			break;
		case Enums.GameTip.MULITPLER:
			mSeenMultiplier = true;
			break;
		case Enums.GameTip.SEQUINS:
			mSeenGems = true;
			break;
		case Enums.GameTip.SUPERPOSE:
			mSeenSuperPose = true;
			break;
		}
		SaveGameSession();
	}

	private bool SaveGameSession()
	{
		if (!mWillNeedToSave)
		{
			return true;
		}
		PlayerPrefs.SetInt(Constants.SaveLoad.BEST_SCORE_KEY, mBestScore);
		PlayerPrefs.SetInt(Constants.SaveLoad.GEM_INTRO_KEY, mSeenGems ? 1 : 0);
		PlayerPrefs.SetInt(Constants.SaveLoad.BLACK_PEARL_TUT_KEY, mSeenBlackPearl ? 1 : 0);
		PlayerPrefs.SetInt(Constants.SaveLoad.SUPERPOSE_TUT_KEY, mSeenSuperPose ? 1 : 0);
		PlayerPrefs.SetInt(Constants.SaveLoad.BOUNCEUP_TUT_KEY, mSeenBounceUp ? 1 : 0);
		PlayerPrefs.SetInt(Constants.SaveLoad.MULTIPLIER_TUT_KEY, mSeenMultiplier ? 1 : 0);
		PlayerPrefs.SetInt(Constants.SaveLoad.DOUBLE_TUT_KEY, mSeenDouble ? 1 : 0);
		PlayerPrefs.Save();
		if (mSeenGems && mSeenBlackPearl && mSeenSuperPose && mSeenBounceUp && mSeenMultiplier && mSeenDouble)
		{
			mWillNeedToSave = false;
		}
		return true;
	}
}
