using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
	private bool mDesignerDataLoaded;

	private static GameData _instance;

	private float mTimeBetweenBackgrounds = 62f;

	private float mTimeBetweenFlowers = 60f;

	private float mTimeBetweenStars = 10f;

	private float mMaxTimeBetweenBlackStar = 40f;

	private float mMinTimeBetweenBlackStar = 20f;

	private float mMaxTimeBetweenItems = 40f;

	private float mMinTimeBetweenItems = 20f;

	public float mRandomMultiplierLifeTime = 10f;

	public float mPoseColliderLifeTime = 5.5f;

	public float mStarLifeTime = 8f;

	public float mSuperPoseLifeTime = 8f;

	public float mMaxFingerDragTime = 0.15f;

	private Vector2 mCeilingForce = Vector2.zero;

	private Vector2 mFloorForce = Vector2.zero;

	private Vector2 mRightWallForce = Vector2.zero;

	private Vector2 mLeftWallForce = Vector2.zero;

	private List<Vector3> mStarPositionOffsets = new List<Vector3>();

	private Vector2 mFlowerBounceForce = Vector2.zero;

	public float mFlowerBounceLifeTime = 5.5f;

	private Vector2 mSuperPosePowerUpPos = Vector2.zero;

	public bool DesignerDataLoaded
	{
		get
		{
			return mDesignerDataLoaded;
		}
	}

	public static GameData Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GameData)) as GameData;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<GameData>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
					_instance.name = "GameData";
				}
			}
			return _instance;
		}
	}

	public static float TimeBetweenBackgrounds
	{
		get
		{
			return Instance.mTimeBetweenBackgrounds;
		}
	}

	public static float TimeBetweenFlowers
	{
		get
		{
			return Instance.mTimeBetweenFlowers;
		}
	}

	public static float TimeBetweenStars
	{
		get
		{
			return Instance.mTimeBetweenStars;
		}
	}

	public static float MaxTimeBetweenBlackStar
	{
		get
		{
			return Instance.mMaxTimeBetweenBlackStar;
		}
	}

	public static float MinTimeBetweenBlackStar
	{
		get
		{
			return Instance.mMinTimeBetweenBlackStar;
		}
	}

	public static float MaxTimeBetweenItems
	{
		get
		{
			return Instance.mMaxTimeBetweenItems;
		}
	}

	public static float MinTimeBetweenItems
	{
		get
		{
			return Instance.mMinTimeBetweenItems;
		}
	}

	public static float RandomMultiplierLifeTime
	{
		get
		{
			return Instance.mRandomMultiplierLifeTime;
		}
	}

	public static float PoseColliderLifeTime
	{
		get
		{
			return Instance.mPoseColliderLifeTime;
		}
	}

	public static float StarLifeTime
	{
		get
		{
			return Instance.mStarLifeTime;
		}
	}

	public static float SuperPoseLifeTime
	{
		get
		{
			return Instance.mSuperPoseLifeTime;
		}
	}

	public static float MaxFingerDragTime
	{
		get
		{
			return Instance.mMaxFingerDragTime;
		}
	}

	public static Vector2 CeilingForce
	{
		get
		{
			return Instance.mCeilingForce;
		}
	}

	public static Vector2 FloorForce
	{
		get
		{
			return Instance.mFloorForce;
		}
	}

	public static Vector2 RightWallForce
	{
		get
		{
			return Instance.mRightWallForce;
		}
	}

	public static Vector2 LeftWallForce
	{
		get
		{
			return Instance.mLeftWallForce;
		}
	}

	public static List<Vector3> StarPositionOffsets
	{
		get
		{
			return Instance.mStarPositionOffsets;
		}
	}

	public static Vector2 FlowerBounceForce
	{
		get
		{
			return Instance.mFlowerBounceForce;
		}
	}

	public static float FlowerBounceLifeTime
	{
		get
		{
			return Instance.mFlowerBounceLifeTime;
		}
	}

	public static Vector2 SuperPosePowerUpPos
	{
		get
		{
			return Instance.mSuperPosePowerUpPos;
		}
	}

	public void LoadDesignerData()
	{
		GameManager.Instance.AddPlayerPoseAnimation(0, Constants.PlayerMoves.LOW_KICK_PREFIX, Constants.PlayerMoves.LOW_KICK_FPS, Constants.PlayerMoves.LOW_KICK_ATTACHMENTNAME, Constants.PlayerMoves.LOW_KICK_ATTACHMENTPOS, Constants.PlayerMoves.LOW_KICK_PIVOT, Constants.PlayerMoves.LOW_KICK_START_ROT, Constants.PlayerMoves.LOW_KICK_END_ROT, Constants.PlayerMoves.LOW_KICK_HITPONT_OFFSET, Constants.PlayerMoves.LOW_KICK_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(1, Constants.PlayerMoves.MID_KICK_PREFIX, Constants.PlayerMoves.MID_KICK_FPS, Constants.PlayerMoves.MID_KICK_ATTACHMENTNAME, Constants.PlayerMoves.MID_KICK_ATTACHMENTPOS, Constants.PlayerMoves.MID_KICK_PIVOT, Constants.PlayerMoves.MID_KICK_START_ROT, Constants.PlayerMoves.MID_KICK_END_ROT, Constants.PlayerMoves.MID_KICK_HITPONT_OFFSET, Constants.PlayerMoves.MID_KICK_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(2, Constants.PlayerMoves.HIGH_KICK_PREFIX, Constants.PlayerMoves.HIGH_KICK_FPS, Constants.PlayerMoves.HIGH_KICK_ATTACHMENTNAME, Constants.PlayerMoves.HIGH_KICK_ATTACHMENTPOS, Constants.PlayerMoves.HIGH_KICK_PIVOT, Constants.PlayerMoves.HIGH_KICK_START_ROT, Constants.PlayerMoves.HIGH_KICK_END_ROT, Constants.PlayerMoves.HIGH_KICK_HITPONT_OFFSET, Constants.PlayerMoves.HIGH_KICK_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(3, Constants.PlayerMoves.LOW_HAND_PREFIX, Constants.PlayerMoves.LOW_HAND_FPS, Constants.PlayerMoves.LOW_HAND_ATTACHMENTNAME, Constants.PlayerMoves.LOW_HAND_ATTACHMENTPOS, Constants.PlayerMoves.LOW_HAND_PIVOT, Constants.PlayerMoves.LOW_HAND_START_ROT, Constants.PlayerMoves.LOW_HAND_END_ROT, Constants.PlayerMoves.LOW_HAND_HITPONT_OFFSET, Constants.PlayerMoves.LOW_HAND_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(4, Constants.PlayerMoves.MID_HAND_PREFIX, Constants.PlayerMoves.MID_HAND_FPS, Constants.PlayerMoves.MID_HAND_ATTACHMENTNAME, Constants.PlayerMoves.MID_HAND_ATTACHMENTPOS, Constants.PlayerMoves.MID_HAND_PIVOT, Constants.PlayerMoves.MID_HAND_START_ROT, Constants.PlayerMoves.MID_HAND_END_ROT, Constants.PlayerMoves.MID_HAND_HITPONT_OFFSET, Constants.PlayerMoves.MID_HAND_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(5, Constants.PlayerMoves.HIGH_HAND_PREFIX, Constants.PlayerMoves.HIGH_HAND_FPS, Constants.PlayerMoves.HIGH_HAND_ATTACHMENTNAME, Constants.PlayerMoves.HIGH_HAND_ATTACHMENTPOS, Constants.PlayerMoves.HIGH_HAND_PIVOT, Constants.PlayerMoves.HIGH_HAND_START_ROT, Constants.PlayerMoves.HIGH_HAND_END_ROT, Constants.PlayerMoves.HIGH_HAND_HITPONT_OFFSET, Constants.PlayerMoves.HIGH_HAND_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(6, Constants.PlayerMoves.HIGH_SIDE_HAND_PREFIX, Constants.PlayerMoves.HIGH_SIDE_HAND_FPS, Constants.PlayerMoves.HIGH_SIDE_HAND_ATTACHMENTNAME, Constants.PlayerMoves.HIGH_SIDE_HAND_ATTACHMENTPOS, Constants.PlayerMoves.HIGH_SIDE_HAND_PIVOT, Constants.PlayerMoves.HIGH_SIDE_HAND_START_ROT, Constants.PlayerMoves.HIGH_SIDE_HAND_END_ROT, Constants.PlayerMoves.HIGH_SIDE_HAND_HITPONT_OFFSET, Constants.PlayerMoves.HIGH_SIDE_HAND_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(7, Constants.PlayerMoves.SUPER_POSE_PREFIX, Constants.PlayerMoves.SUPER_POSE_FPS, Constants.PlayerMoves.SUPER_POSE_ATTACHMENTNAME, Constants.PlayerMoves.SUPER_POSE_ATTACHMENTPOS, Constants.PlayerMoves.SUPER_POSE_PIVOT, Constants.PlayerMoves.SUPER_POSE_START_ROT, Constants.PlayerMoves.SUPER_POSE_END_ROT, Constants.PlayerMoves.SUPER_POSE_HITPONT_OFFSET, Constants.PlayerMoves.SUPER_POSE_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(8, Constants.PlayerMoves.IDLE_POSE_PREFIX, Constants.PlayerMoves.IDLE_POSE_FPS, Constants.PlayerMoves.IDLE_POSE_ATTACHMENTNAME, Constants.PlayerMoves.IDLE_POSE_ATTACHMENTPOS, Constants.PlayerMoves.IDLE_POSE_PIVOT, Constants.PlayerMoves.IDLE_POSE_START_ROT, Constants.PlayerMoves.IDLE_POSE_END_ROT, Constants.PlayerMoves.IDLE_POSE_HITPONT_OFFSET, Constants.PlayerMoves.IDLE_POSE_FORCE);
		GameManager.Instance.AddPlayerPoseAnimation(9, Constants.PlayerMoves.IDLE_LOOP_PREFIX, Constants.PlayerMoves.IDLE_LOOP_FPS, Constants.PlayerMoves.IDLE_LOOP_ATTACHMENTNAME, Constants.PlayerMoves.IDLE_LOOP_ATTACHMENTPOS, Constants.PlayerMoves.IDLE_LOOP_PIVOT, Constants.PlayerMoves.IDLE_LOOP_START_ROT, Constants.PlayerMoves.IDLE_LOOP_END_ROT, Constants.PlayerMoves.IDLE_LOOP_HITPONT_OFFSET, Constants.PlayerMoves.IDLE_LOOP_FORCE);
		mTimeBetweenFlowers = 20f;
		mTimeBetweenStars = 15f;
		mMinTimeBetweenBlackStar = 15f;
		mMaxTimeBetweenBlackStar = 30f;
		mMinTimeBetweenItems = 20f;
		mMaxTimeBetweenItems = 40f;
		mPoseColliderLifeTime = 0.5f;
		mRandomMultiplierLifeTime = 10f;
		mStarLifeTime = 20f;
		mFlowerBounceLifeTime = 10f;
		mSuperPoseLifeTime = 8f;
		mMaxFingerDragTime = 0.15f;
		mTimeBetweenBackgrounds = 60f;
		mCeilingForce = new Vector2(0f, -0.3f);
		mFloorForce = new Vector2(0f, 0f);
		mLeftWallForce = new Vector2(-0.5f, 0f);
		mRightWallForce = new Vector2(0.5f, 0f);
		mFlowerBounceForce = new Vector2(0f, 100f);
		mSuperPosePowerUpPos = new Vector2(0f, 100f);
		mStarPositionOffsets.Add(new Vector3(-100f, 50f, 0f));
		mStarPositionOffsets.Add(new Vector3(0f, 50f, 0f));
		mStarPositionOffsets.Add(new Vector3(100f, 50f, 0f));
		mStarPositionOffsets.Add(new Vector3(-100f, -50f, 0f));
		mStarPositionOffsets.Add(new Vector3(0f, -50f, 0f));
		mStarPositionOffsets.Add(new Vector3(100f, -50f, 0f));
		mDesignerDataLoaded = true;
	}

	public float GetRightMaxPos()
	{
		return GameScreenController.Instance.TopRight.transform.localPosition.x;
	}

	public float GetLeftMaxPos()
	{
		return GameScreenController.Instance.BottomLeft.transform.localPosition.x;
	}

	public float GetScreenTopPos()
	{
		return GameScreenController.Instance.TopRight.transform.localPosition.y;
	}

	public float GetScreenBottomPos()
	{
		return GameScreenController.Instance.BottomLeft.transform.localPosition.y;
	}

	public float GetBottomMaxFlowerPos()
	{
		return GameScreenController.Instance.BottomLeft.transform.localPosition.y + 10f;
	}

	public float GetFlowerBounceYPos()
	{
		return GameScreenController.Instance.BottomLeft.transform.localPosition.y + 70f;
	}

	public float GetPauseButtonMaxX()
	{
		float result = 0f;
		if (GameScreenController.Instance.pauseButton != null)
		{
			float x = GameScreenController.Instance.pauseButton.transform.localPosition.x;
			float num = 5f;
			float leftMaxPos = GetLeftMaxPos();
			if ((bool)GameScreenController.Instance.pauseButton.GetComponent("UIImageButton"))
			{
				UIImageButton uIImageButton = GameScreenController.Instance.pauseButton.GetComponent("UIImageButton") as UIImageButton;
				float x2 = uIImageButton.target.transform.localScale.x;
				return leftMaxPos + x + x2 / 2f + num;
			}
		}
		return result;
	}

	public float GetPauseButtonMaxY()
	{
		float result = 0f;
		if (GameScreenController.Instance.pauseButton != null)
		{
			float y = GameScreenController.Instance.pauseButton.transform.localPosition.y;
			float num = 5f;
			float screenBottomPos = GetScreenBottomPos();
			if ((bool)GameScreenController.Instance.pauseButton.GetComponent("UIImageButton"))
			{
				UIImageButton uIImageButton = GameScreenController.Instance.pauseButton.GetComponent("UIImageButton") as UIImageButton;
				float y2 = uIImageButton.target.transform.localScale.y;
				return screenBottomPos + y + y2 / 2f + num;
			}
		}
		return result;
	}
}
