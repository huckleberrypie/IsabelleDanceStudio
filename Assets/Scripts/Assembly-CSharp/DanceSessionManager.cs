using System.Collections.Generic;
using UnityEngine;

public class DanceSessionManager : MonoBehaviour
{
	private static DanceSessionManager _instance;

	private int numStarsBeforeSuperPose = Constants.DesginerGlobals.NUM_STARS_FOR_SUPER_POSE;

	private float mTimeTillNextFLower;

	private float mTimeTillNextStarGroup;

	private float mTimeTillNextItem;

	private float mTimeTillBlackStar;

	private float mFloweBounceLifeTime;

	private float mCurrUpdateTime;

	private float mPrevUpdateTime;

	private bool mSuperPoseMode;

	private bool mFlowerBounceMode;

	private bool mFirstKickMode = true;

	private bool mEndingGameSlowly;

	private float mGameOverDelay = 2f;

	private bool mShowingTip;

	private Enums.GameTip mCurrentTip = Enums.GameTip.NONE;

	private bool mTouchBegan;

	private float maxDragTime = 0.15f;

	private float curDragTime;

	private Vector2 lastClickedPos = Vector2.zero;

	private Vector2 mouseScreenPos = Vector2.zero;

	private bool mRecordTouch;

	private int mCurBackgroundIndex = -1;

	private bool mBackgroundChanging;

	private float mBackgroundTransitionTimeLeft;

	private float mSelectedMultiplierTimeLeft;

	private int mSelectedMultiplierAmount;

	private int mCurrCollectedStars;

	private bool mAwardCollectAllStarsBonus;

	private bool mHasSuperCollider;

	private bool mCollectedDoubleStarPowerUp;

	private bool mCollectedPoseUpPowerUp;

	public static DanceSessionManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(DanceSessionManager)) as DanceSessionManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<DanceSessionManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public int StarsLeftBeforeSuperPose
	{
		get
		{
			return numStarsBeforeSuperPose;
		}
	}

	public bool FirstKickMode
	{
		get
		{
			return mFirstKickMode;
		}
		set
		{
			mFirstKickMode = value;
		}
	}

	public bool EndingGameSlowly
	{
		get
		{
			return mEndingGameSlowly;
		}
		set
		{
			mEndingGameSlowly = value;
		}
	}

	public float GameOverDelay
	{
		get
		{
			return mGameOverDelay;
		}
	}

	public bool ShowingTip
	{
		get
		{
			return mShowingTip;
		}
		set
		{
			mShowingTip = value;
		}
	}

	public Enums.GameTip CurrentTip
	{
		get
		{
			return mCurrentTip;
		}
		set
		{
			mCurrentTip = value;
		}
	}

	public static bool CanTouch
	{
		get
		{
			return Instance.mRecordTouch;
		}
		set
		{
			Instance.mRecordTouch = value;
		}
	}

	public static float BackgroundTransitionTimeLeft
	{
		set
		{
			Instance.mBackgroundTransitionTimeLeft = value;
		}
	}

	public static float SelectedMultiplierTimeLeft
	{
		set
		{
			Instance.mSelectedMultiplierTimeLeft = value;
		}
	}

	public static int SelectedMulitplierAmount
	{
		get
		{
			return Instance.mSelectedMultiplierAmount;
		}
		set
		{
			Instance.mSelectedMultiplierAmount = value;
		}
	}

	public static int CurrentCollectedStars
	{
		get
		{
			return Instance.mCurrCollectedStars;
		}
	}

	public static bool AwardCollectAllStarsBonus
	{
		get
		{
			return Instance.mAwardCollectAllStarsBonus;
		}
		set
		{
			Instance.mAwardCollectAllStarsBonus = value;
		}
	}

	public static bool CollectedDoubleStar
	{
		get
		{
			return Instance.mCollectedDoubleStarPowerUp;
		}
		set
		{
			Instance.mCollectedDoubleStarPowerUp = value;
		}
	}

	public static bool CollectedPoseUp
	{
		get
		{
			return Instance.mCollectedPoseUpPowerUp;
		}
		set
		{
			Instance.mCollectedPoseUpPowerUp = value;
		}
	}

	public static void ResetSuperPose()
	{
		Instance.mHasSuperCollider = false;
	}

	private void Start()
	{
	}

	private float ChooseRandomItemTime()
	{
		return Random.Range(GameData.MinTimeBetweenItems, GameData.MaxTimeBetweenItems);
	}

	private float ChooseRandomBlackStarTime()
	{
		return Random.Range(GameData.MinTimeBetweenBlackStar, GameData.MaxTimeBetweenBlackStar);
	}

	public void StartDanceSession()
	{
		mTimeTillNextFLower = GameData.TimeBetweenFlowers;
		mTimeTillNextStarGroup = GameData.TimeBetweenStars;
		mTimeTillNextItem = ChooseRandomItemTime();
		mTimeTillBlackStar = ChooseRandomBlackStarTime();
		mCurrUpdateTime = 0f;
		mPrevUpdateTime = 0f;
		mSelectedMultiplierTimeLeft = 0f;
		numStarsBeforeSuperPose = Constants.DesginerGlobals.NUM_STARS_FOR_SUPER_POSE;
		mHasSuperCollider = false;
		mSelectedMultiplierAmount = 0;
		mCurrCollectedStars = 0;
		mAwardCollectAllStarsBonus = false;
		mHasSuperCollider = false;
		mCollectedDoubleStarPowerUp = false;
		mCollectedPoseUpPowerUp = false;
		mSuperPoseMode = false;
		mFlowerBounceMode = false;
		mFloweBounceLifeTime = 0f;
		SpawnManager.Instance.AddStartFlower();
		mRecordTouch = false;
		maxDragTime = GameData.MaxFingerDragTime;
		mBackgroundChanging = false;
		mBackgroundTransitionTimeLeft = GameData.TimeBetweenBackgrounds;
		mFirstKickMode = true;
		mEndingGameSlowly = false;
		mGameOverDelay = Constants.DesginerGlobals.END_GAME_TOUCH_DELAY;
		if ((bool)GameManager.PlayerObject)
		{
			GameManager.PlayerObject.transform.localPosition = new Vector3(Constants.DesginerGlobals.PLAYER_START_X, Constants.DesginerGlobals.MIN_PLAYER_POS_Y, base.transform.localPosition.z);
			GameManager.PlayerObject.transform.localScale = new Vector3(1f, GameManager.PlayerObject.transform.localScale.y, GameManager.PlayerObject.transform.localScale.z);
		}
		if ((bool)GameManager.SuperPoseBar)
		{
			GameManager.SuperPoseBar.SetActive(true);
			if ((bool)GameManager.SuperPoseBar.GetComponent("SuperPoseBarController"))
			{
				(GameManager.SuperPoseBar.GetComponent("SuperPoseBarController") as SuperPoseBarController).ResetBar();
			}
		}
		mCurBackgroundIndex = -1;
		ShowNewBackground();
		MonoBehaviour.print("El de las recargas");
	}

	private void Update()
	{
		if (GameManager.Instance.HasGameStarted())
		{
			if (!GameManager.Instance.IsGamePaused())
			{
				if (mCurrUpdateTime == 0f)
				{
					mTimeTillNextFLower = GameData.TimeBetweenFlowers;
					mTimeTillNextStarGroup = GameData.TimeBetweenStars;
					mTimeTillNextItem = ChooseRandomItemTime();
					mTimeTillBlackStar = ChooseRandomBlackStarTime();
					mPrevUpdateTime = (mCurrUpdateTime = Time.time);
					mFloweBounceLifeTime = 0f;
					mBackgroundTransitionTimeLeft = GameData.TimeBetweenBackgrounds;
					mGameOverDelay = 2f;
				}
				mCurrUpdateTime = Time.time;
				float num = mCurrUpdateTime - mPrevUpdateTime;
				if (!EndingGameSlowly)
				{
					mTimeTillNextFLower -= num;
					mTimeTillNextStarGroup -= num;
					mTimeTillNextItem -= num;
					mTimeTillBlackStar -= num;
					mSelectedMultiplierTimeLeft -= num;
					mBackgroundTransitionTimeLeft -= num;
				}
				if (EndingGameSlowly)
				{
					mGameOverDelay -= num;
				}
				if (mTimeTillNextFLower < 0f || GameObject.FindGameObjectWithTag("Flower") == null)
				{
					SpawnManager.Instance.AddFlower();
					mTimeTillNextFLower = GameData.TimeBetweenFlowers;
				}
				if ((bool)GameObject.FindGameObjectWithTag("Star"))
				{
					mAwardCollectAllStarsBonus = true;
				}
				if (!GameObject.FindGameObjectWithTag("Star") && mAwardCollectAllStarsBonus && !EndingGameSlowly && !IsSuperPoseMode())
				{
					Scoreboard.Instance.AddPoints(100);
					SpawnManager.Instance.AddFloatingPoints(new Vector3(0f, 0f, 0f), "100");
					mAwardCollectAllStarsBonus = false;
				}
				if (GameObject.FindGameObjectWithTag("Star") != null)
				{
					mTimeTillNextStarGroup = GameData.TimeBetweenStars;
				}
				if (mTimeTillNextStarGroup < 0f && !mHasSuperCollider)
				{
					SpawnManager.Instance.AddStarsAtPos(Vector3.zero, 6);
					mTimeTillNextStarGroup = GameData.TimeBetweenStars;
				}
				if (mTimeTillNextItem < 0f)
				{
					SpawnManager.Instance.AddRandomItem();
					mTimeTillNextItem = ChooseRandomItemTime();
				}
				if (mTimeTillBlackStar < 0f)
				{
					SpawnManager.Instance.AddFlower(true);
					mTimeTillBlackStar = ChooseRandomBlackStarTime();
				}
				if (mSelectedMultiplierTimeLeft < 0f)
				{
					mSelectedMultiplierAmount = 0;
					mSelectedMultiplierTimeLeft = 0f;
				}
				if (Scoreboard.Instance.CollectedStars > mCurrCollectedStars)
				{
					mCurrCollectedStars++;
					if (numStarsBeforeSuperPose > 0)
					{
						numStarsBeforeSuperPose--;
					}
					if (numStarsBeforeSuperPose <= 0)
					{
						numStarsBeforeSuperPose = 0;
					}
				}
				if (numStarsBeforeSuperPose == 0 && !PlayerManager.Instance.SeenSuperPose && !ShowingTip)
				{
					Instance.CurrentTip = Enums.GameTip.SUPERPOSE;
					GameScreenController.Instance.ShowTipPanel(Enums.GameTip.SUPERPOSE);
					ShowingTip = true;
					PlayerManager.Instance.TipUnlocked(Enums.GameTip.SUPERPOSE);
				}
				if (numStarsBeforeSuperPose <= 0 && !mHasSuperCollider)
				{
					// KontagentManager.Instance.SendCustomEvent("Game_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Super_Pose_Meter_Full");
					mHasSuperCollider = true;
				}
				if (mFlowerBounceMode)
				{
					mFloweBounceLifeTime += num;
					if (mFloweBounceLifeTime >= GameData.FlowerBounceLifeTime)
					{
						SetFlowerBounceMode(false);
					}
				}
				if (mBackgroundTransitionTimeLeft < 0.6f && mBackgroundTransitionTimeLeft > 0.3f && !mBackgroundChanging)
				{
					mBackgroundChanging = true;
					if (GameManager.TransitionGameBackground.GetComponent("UISprite") != null)
					{
						(GameManager.TransitionGameBackground.GetComponent("UISprite") as UISprite).depth = Constants.DesginerGlobals.GAME_TRANSITION_DEPTH;
					}
					GameManager.Instance.StartBackgroundTransition(false);
				}
				else if (mBackgroundTransitionTimeLeft < 0.3f && mBackgroundChanging)
				{
					ShowNewBackground();
					// KontagentManager.Instance.SendCustomEvent("Game_Screen", string.Empty, Conversion.AddCommas(Scoreboard.Instance.CurrentGameTime), string.Empty, string.Empty, string.Empty, "Background_Changed");
					GameManager.Instance.EndBackgroundTransition();
					mBackgroundChanging = false;
				}
				else if (mBackgroundTransitionTimeLeft < 0f && !mBackgroundChanging)
				{
					GameManager.Instance.ResetBackgroundTransition();
					mBackgroundTransitionTimeLeft = GameData.TimeBetweenBackgrounds;
				}
				if (Input.GetMouseButtonDown(0))
				{
					mTouchBegan = true;
					curDragTime = 0f;
					lastClickedPos = Vector2.zero;
					lastClickedPos = Input.mousePosition;
					float num2 = lastClickedPos.x / (float)Screen.width;
					float num3 = lastClickedPos.y / (float)Screen.height;
					float num4 = GameData.Instance.GetRightMaxPos() - GameData.Instance.GetLeftMaxPos();
					float num5 = GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos();
					mouseScreenPos.x = num4 * num2;
					mouseScreenPos.y = num5 * num3;
					mouseScreenPos.x -= num4 / 2f;
					mouseScreenPos.y -= num5 / 2f;
				}
				if (mTouchBegan)
				{
					curDragTime += num;
					if (curDragTime < maxDragTime)
					{
						if (Input.GetMouseButtonUp(0))
						{
							mTouchBegan = false;
						}
					}
					else
					{
						mTouchBegan = false;
					}
					if (!mTouchBegan && (mouseScreenPos.x > GameData.Instance.GetPauseButtonMaxX() || mouseScreenPos.y > GameData.Instance.GetPauseButtonMaxY()))
					{
						SpawnManager.Instance.AddEventBoxCollider(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
					}
				}
			}
			else
			{
				mPrevUpdateTime = (mCurrUpdateTime = Time.time);
				if (!ShowingTip)
				{
				}
			}
			mPrevUpdateTime = mCurrUpdateTime;
			return;
		}
		mTimeTillNextFLower = GameData.TimeBetweenFlowers;
		mTimeTillNextStarGroup = GameData.TimeBetweenStars;
		mTimeTillNextItem = ChooseRandomItemTime();
		mTimeTillBlackStar = ChooseRandomBlackStarTime();
		if (Input.GetMouseButtonDown(0) && mRecordTouch)
		{
			lastClickedPos = Vector2.zero;
			lastClickedPos = Input.mousePosition;
			MonoBehaviour.print("mouse position was " + lastClickedPos);
			float num6 = lastClickedPos.x / (float)Screen.width;
			float num7 = lastClickedPos.y / (float)Screen.height;
			float num8 = GameData.Instance.GetRightMaxPos() - GameData.Instance.GetLeftMaxPos();
			float num9 = GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos();
			mouseScreenPos.x = num8 * num6;
			mouseScreenPos.y = num9 * num7;
			mouseScreenPos.x -= num8 / 2f;
			mouseScreenPos.y -= num9 / 2f;
			if (mouseScreenPos.x > GameData.Instance.GetPauseButtonMaxX() || mouseScreenPos.y > GameData.Instance.GetPauseButtonMaxY())
			{
				SpawnManager.Instance.AddEventBoxCollider(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
			}
		}
	}

	public void SetSuperPoseMode(bool val)
	{
		mSuperPoseMode = val;
		if (mSuperPoseMode)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Star");
			if (array.Length > 0)
			{
				GameObject[] array2 = array;
				foreach (GameObject obj in array2)
				{
					Object.Destroy(obj);
				}
			}
			mAwardCollectAllStarsBonus = false;
			SetFlowerBounceMode(false);
			GameScreenController.Instance.StartSuperPoseBGAnimation();
			return;
		}
		GameScreenController.Instance.StopSuperPoseBGAnimation();
		if ((bool)GameManager.SuperPoseBar)
		{
			GameManager.SuperPoseBar.SetActive(true);
			if ((bool)GameManager.SuperPoseBar.GetComponent("SuperPoseBarController"))
			{
				(GameManager.SuperPoseBar.GetComponent("SuperPoseBarController") as SuperPoseBarController).ResetBar();
			}
		}
		numStarsBeforeSuperPose = Constants.DesginerGlobals.NUM_STARS_FOR_SUPER_POSE;
	}

	public bool IsSuperPoseMode()
	{
		return mSuperPoseMode;
	}

	public void SetFlowerBounceMode(bool val)
	{
		mFlowerBounceMode = val;
		if (mFlowerBounceMode)
		{
			GameScreenController.Instance.ShowFlowerBounce();
			return;
		}
		GameScreenController.Instance.HideFlowerBounce();
		mFloweBounceLifeTime = 0f;
	}

	public bool IsFlowerBounceMode()
	{
		return mFlowerBounceMode;
	}

	private void ShowNewBackground()
	{
		List<int> list = new List<int>();
		if (mCurBackgroundIndex == -1)
		{
			list.Add(0);
			list.Add(1);
			list.Add(2);
			list.Add(3);
			list.Add(4);
		}
		else
		{
			if (mCurBackgroundIndex != 0)
			{
				list.Add(0);
			}
			if (mCurBackgroundIndex != 1)
			{
				list.Add(1);
			}
			if (mCurBackgroundIndex != 2)
			{
				list.Add(2);
			}
			if (mCurBackgroundIndex != 3)
			{
				list.Add(3);
			}
			if (mCurBackgroundIndex != 4)
			{
				list.Add(4);
			}
		}
		if (list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			MonoBehaviour.print("Choose new background at " + list[index]);
			mCurBackgroundIndex = list[index];
			SharedTextureManager.SwitchGameBackground(list[index]);
		}
		list.Clear();
	}

	public void FadeAwayAllFlowersAndItems()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Flower");
		if (array.Length > 0)
		{
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (!(gameObject.GetComponent("GameOverSequin") != null))
				{
					if (gameObject.GetComponent("FadeOut") == null)
					{
						gameObject.AddComponent<FadeOut>();
					}
					FadeOut fadeOut = gameObject.GetComponent("FadeOut") as FadeOut;
					if (fadeOut != null)
					{
						fadeOut.StartFadeOut(Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
					}
					Object.Destroy(gameObject, Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
				}
			}
		}
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("BouncePackage");
		if (array3.Length > 0)
		{
			GameObject[] array4 = array3;
			foreach (GameObject gameObject2 in array4)
			{
				if (gameObject2.GetComponent("FadeOut") == null)
				{
					gameObject2.AddComponent<FadeOut>();
				}
				FadeOut fadeOut2 = gameObject2.GetComponent("FadeOut") as FadeOut;
				if (fadeOut2 != null)
				{
					fadeOut2.StartFadeOut(Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
				}
				Object.Destroy(gameObject2, Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
			}
		}
		GameObject[] array5 = GameObject.FindGameObjectsWithTag("RandomMulitplier");
		if (array5.Length > 0)
		{
			GameObject[] array6 = array5;
			foreach (GameObject gameObject3 in array6)
			{
				if (gameObject3.GetComponent("FadeOut") == null)
				{
					gameObject3.AddComponent<FadeOut>();
				}
				FadeOut fadeOut3 = gameObject3.GetComponent("FadeOut") as FadeOut;
				if (fadeOut3 != null)
				{
					fadeOut3.StartFadeOut(Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
				}
				Object.Destroy(gameObject3, Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
			}
		}
		GameObject[] array7 = GameObject.FindGameObjectsWithTag("Star");
		if (array7.Length > 0)
		{
			GameObject[] array8 = array7;
			foreach (GameObject gameObject4 in array8)
			{
				if (gameObject4.GetComponent("FadeOut") == null)
				{
					gameObject4.AddComponent<FadeOut>();
				}
				FadeOut fadeOut4 = gameObject4.GetComponent("FadeOut") as FadeOut;
				if (fadeOut4 != null)
				{
					fadeOut4.StartFadeOut(Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
				}
				Object.Destroy(gameObject4, Constants.DesginerGlobals.FLOWER_FADEOUT_TIME);
			}
		}
		GameScreenController.Instance.HideFlowerBounce();
	}

	public void DestoryAllFlowers()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Flower");
		if (array.Length > 0)
		{
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (gameObject.GetComponent("FlowerPhysics") as FlowerPhysics != null && !(gameObject.GetComponent("FlowerPhysics") as FlowerPhysics).BlackStar)
				{
					Object.Destroy(gameObject);
				}
			}
		}
		GameObject gameObject2 = GameObject.FindGameObjectWithTag("SuperPoseCollider");
		if (gameObject2 != null)
		{
			Object.Destroy(gameObject2);
		}
		mHasSuperCollider = false;
	}

	public void DeActivateDanceColliders()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ClickCollider");
		if (array.Length > 0)
		{
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				gameObject.SetActive(false);
			}
		}
		array = GameObject.FindGameObjectsWithTag("PoseCollider");
		if (array.Length > 0)
		{
			GameObject[] array3 = array;
			foreach (GameObject gameObject2 in array3)
			{
				gameObject2.SetActive(false);
			}
		}
	}

	public void ActivateDanceColliders()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ClickCollider");
		if (array.Length > 0)
		{
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				gameObject.SetActive(true);
			}
		}
		array = GameObject.FindGameObjectsWithTag("PoseCollider");
		if (array.Length > 0)
		{
			GameObject[] array3 = array;
			foreach (GameObject gameObject2 in array3)
			{
				gameObject2.SetActive(true);
			}
		}
	}
}
