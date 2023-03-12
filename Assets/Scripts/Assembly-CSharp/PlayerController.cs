using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private int mCurrentMoveIndex = -1;

	private int mPreviousMoveIndex = -1;

	private GameObject mCurrentPlayerAnimation;

	private bool mPlayerFlip;

	public GameObject playerDefaultAnimations;

	public GameObject playerLowLegAnimation;

	public GameObject playerMidLegAnimation;

	public GameObject playerHighLegAnimation;

	public GameObject playerLowArmAnimation;

	public GameObject playerMidArmAnimation;

	public GameObject playerHighArmAnimation;

	public GameObject playerHighSideArmAnimation;

	public GameObject playerSuperPoseAnimation;

	public List<UIAtlas> poseAtlas;

	private float mWinkTimeLeft = Constants.DesginerGlobals.WINK_FRAME_TIME;

	private float mTimeBeforeWink = Constants.DesginerGlobals.MAX_WINK_TIME;

	private bool mWinking;

	private void Start()
	{
	}

	private void MoveToPos(Vector3 flowerPos)
	{
		float num = GameManager.GetPlayerPoseHitPointOffset(mCurrentMoveIndex).x;
		float y = GameManager.GetPlayerPoseHitPointOffset(mCurrentMoveIndex).y;
		if (flowerPos.x <= base.transform.localPosition.x)
		{
			mPlayerFlip = false;
		}
		else
		{
			mPlayerFlip = true;
		}
		if (flowerPos.x < GameData.Instance.GetLeftMaxPos() + 100f)
		{
			mPlayerFlip = false;
		}
		else if (flowerPos.x > GameData.Instance.GetRightMaxPos() - 100f)
		{
			mPlayerFlip = true;
		}
		if (mPlayerFlip)
		{
			base.transform.localScale = new Vector3(-1f, base.transform.localScale.y, base.transform.localScale.z);
			num *= -1f;
		}
		else
		{
			base.transform.localScale = new Vector3(1f, base.transform.localScale.y, base.transform.localScale.z);
		}
		float num2 = flowerPos.x - num;
		float num3 = flowerPos.y - y;
		Vector3 vector = new Vector3(base.transform.position.x + num2, base.transform.position.y + num3, 0f);
		if (vector.y < Constants.DesginerGlobals.MIN_PLAYER_POS_Y)
		{
			vector.y = Constants.DesginerGlobals.MIN_PLAYER_POS_Y;
		}
		if (vector.y > Constants.DesginerGlobals.MAX_PLAYER_POS_Y)
		{
			vector.y = Constants.DesginerGlobals.MAX_PLAYER_POS_Y;
		}
		if (vector.x < GameData.Instance.GetLeftMaxPos())
		{
			vector.x = GameData.Instance.GetLeftMaxPos();
		}
		if (vector.x > GameData.Instance.GetRightMaxPos())
		{
			vector.x = GameData.Instance.GetRightMaxPos();
		}
		DisplayMagic(false, vector);
		if (mCurrentMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_SUPER_POSE)
		{
			vector.x = 0f;
		}
		base.transform.localPosition = vector;
	}

	private void DisplayMagic(bool starMagic, Vector3 newPos)
	{
		float num = newPos.x - base.transform.localPosition.x;
		float num2 = newPos.y - base.transform.localPosition.y;
		float num3 = 5f / (float)GameManager.GetPlayerPoseAnimationFPS(mCurrentMoveIndex);
		List<int> list = new List<int>();
		Vector3 vector = Vector3.zero;
		Vector3 zero = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		if (num > 190f || num < -190f)
		{
			vector = new Vector3(num / 5f * 4f, num2 / 5f * 4f, 0f);
			zero = new Vector3(num / 5f * 3f, num2 / 5f * 3f, 0f);
			vector2 = new Vector3(num / 5f * 2f, num2 / 5f * 2f, 0f);
		}
		else if (num > 140f || num < -140f)
		{
			vector = new Vector3(num / 4f * 3f, num2 / 4f * 3f, 0f);
			zero = new Vector3(num / 4f * 2f, num2 / 4f * 2f, 0f);
		}
		else
		{
			zero = new Vector3(num / 2f, num2 / 2f, 0f);
		}
		if (vector != Vector3.zero)
		{
			Vector3 position = base.transform.localPosition + vector;
			SpawnManager.Instance.AddPoseAnimationEffect(string.Empty + GameManager.GetPlayerPoseAnimationPrefix(mCurrentMoveIndex) + "005", poseAtlas[mCurrentMoveIndex], 0.3f, position, mPlayerFlip, num3, num3, false);
		}
		if (zero != Vector3.zero)
		{
			Vector3 position2 = base.transform.localPosition + zero;
			SpawnManager.Instance.AddPoseAnimationEffect(string.Empty + GameManager.GetPlayerPoseAnimationPrefix(mCurrentMoveIndex) + "005", poseAtlas[mCurrentMoveIndex], 0.2f, position2, mPlayerFlip, num3 / 2f, num3 / 2f, false);
		}
		if (vector2 != Vector3.zero)
		{
			Vector3 position3 = base.transform.localPosition + vector2;
			SpawnManager.Instance.AddPoseAnimationEffect(string.Empty + GameManager.GetPlayerPoseAnimationPrefix(mCurrentMoveIndex) + "005", poseAtlas[mCurrentMoveIndex], 0.1f, position3, mPlayerFlip, num3 / 4f, num3 / 4f, false);
		}
		list.Clear();
	}

	private void CreateCollider(Vector3 choosenPos, Vector2 force)
	{
		SpawnManager.Instance.AddPoseCollider(mCurrentMoveIndex, choosenPos, force);
	}

	private int ChooseMove(Vector3 flowerPos)
	{
		int num = 0;
		float num2 = flowerPos.y + (GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos()) / 2f;
		float num3 = num2 / (GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos());
		if (num3 < Constants.DesginerGlobals.LOW_KICK_SCREEN_AREA)
		{
			num = Constants.PlayerMoves.PLAYER_MOVE_LOW_KICK;
		}
		else if (num3 < Constants.DesginerGlobals.MID_KICK_SCREEN_AREA)
		{
			num = Constants.PlayerMoves.PLAYER_MOVE_MID_KICK;
		}
		else if (num3 < Constants.DesginerGlobals.HIGH_KICK_SCREEN_AREA)
		{
			num = Constants.PlayerMoves.PLAYER_MOVE_HIGH_KICK;
		}
		else if (num3 < Constants.DesginerGlobals.LOW_HAND_SCREEN_AREA)
		{
			num = Constants.PlayerMoves.PLAYER_MOVE_LOW_HAND;
		}
		else if (num3 < Constants.DesginerGlobals.MID_HAND_SCREEN_AREA)
		{
			num = Constants.PlayerMoves.PLAYER_MOVE_MID_HAND;
		}
		else
		{
			float x = flowerPos.x;
			num = ((!(x < base.transform.localPosition.x - 40f) && !(x > base.transform.localPosition.x + 40f)) ? Constants.PlayerMoves.PLAYER_MOVE_HIGH_HAND : Constants.PlayerMoves.PLAYER_MOVE_HIGH_SIDE_HAND);
		}
		if (DanceSessionManager.Instance.IsSuperPoseMode())
		{
			num = Constants.PlayerMoves.PLAYER_MOVE_SUPER_POSE;
		}
		return num;
	}

	public void SwitchAnimation()
	{
		switch (mCurrentMoveIndex)
		{
		case 0:
			if (mPreviousMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_LOW_KICK)
			{
				return;
			}
			if (mCurrentPlayerAnimation != null)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerLowLegAnimation;
			break;
		case 1:
			if (mPreviousMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_MID_KICK)
			{
				return;
			}
			if (mCurrentPlayerAnimation != null)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerMidLegAnimation;
			break;
		case 2:
			if (mPreviousMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_HIGH_KICK)
			{
				return;
			}
			if (mCurrentPlayerAnimation != null)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerHighLegAnimation;
			break;
		case 3:
			if (mPreviousMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_LOW_HAND)
			{
				return;
			}
			if (mCurrentPlayerAnimation != null)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerLowArmAnimation;
			break;
		case 4:
			if (mPreviousMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_MID_HAND)
			{
				return;
			}
			if (mCurrentPlayerAnimation != null)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerMidArmAnimation;
			break;
		case 5:
			if (mPreviousMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_HIGH_HAND)
			{
				return;
			}
			if (mCurrentPlayerAnimation != null)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerHighArmAnimation;
			break;
		case 6:
			if (mPreviousMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_HIGH_SIDE_HAND)
			{
				return;
			}
			if (mCurrentPlayerAnimation != null)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerHighSideArmAnimation;
			break;
		case 7:
			mCurrentPlayerAnimation.SetActive(false);
			mCurrentPlayerAnimation = playerSuperPoseAnimation;
			break;
		case 8:
		case 9:
			if (mCurrentPlayerAnimation != null && mCurrentPlayerAnimation != playerDefaultAnimations)
			{
				mCurrentPlayerAnimation.SetActive(false);
			}
			mCurrentPlayerAnimation = playerDefaultAnimations;
			break;
		}
		if (mCurrentPlayerAnimation != null)
		{
			mCurrentPlayerAnimation.SetActive(true);
			UISprite uISprite = mCurrentPlayerAnimation.GetComponent("UISprite") as UISprite;
			if (uISprite == null)
			{
				uISprite = mCurrentPlayerAnimation.AddComponent<UISprite>() as UISprite;
			}
			uISprite.spriteName = GameManager.GetPlayerPoseAnimationPrefix(mCurrentMoveIndex) + "001";
			uISprite.MakePixelPerfect();
			uISprite.depth = Constants.DesginerGlobals.PLAYERSPRITE_DEPTH;
			mCurrentPlayerAnimation.transform.localPosition = GameManager.GetPlayerPoseAttachmentPos(mCurrentMoveIndex);
		}
		else
		{
			MonoBehaviour.print("ERROR: animation is null");
		}
	}

	public void PlayMoveAnimation()
	{
		if (mCurrentPlayerAnimation != null)
		{
			BalletMoveAnimation balletMoveAnimation = mCurrentPlayerAnimation.GetComponent("BalletMoveAnimation") as BalletMoveAnimation;
			if (balletMoveAnimation == null)
			{
				balletMoveAnimation = mCurrentPlayerAnimation.AddComponent<BalletMoveAnimation>() as BalletMoveAnimation;
			}
			balletMoveAnimation.framesPerSecond = GameManager.GetPlayerPoseAnimationFPS(mCurrentMoveIndex);
			balletMoveAnimation.SetAnimationByPrefix(GameManager.GetPlayerPoseAnimationPrefix(mCurrentMoveIndex));
			balletMoveAnimation.Reset();
		}
	}

	public void PerformMove(Vector3 flowerPos, Vector2 direction)
	{
		mPreviousMoveIndex = mCurrentMoveIndex;
		if (mCurrentMoveIndex != Constants.PlayerMoves.PLAYER_MOVE_SUPER_POSE)
		{
			int num = ChooseMove(flowerPos);
			if (mPreviousMoveIndex != num)
			{
				mCurrentMoveIndex = num;
				SwitchAnimation();
			}
			PlayMoveAnimation();
			MoveToPos(flowerPos);
			Vector2 force = DetermineColliderForce(direction);
			CreateCollider(flowerPos, force);
			AudioManager.Sound.PlaySoundOneShot("hit_nothing");
		}
	}

	private Vector2 DetermineColliderForce(Vector2 direction)
	{
		Vector2 zero = Vector2.zero;
		if (direction.x < 0f)
		{
			zero.x = 0f - GameManager.GetPlayerPoseForce(mCurrentMoveIndex).x;
		}
		else if (direction.x > 0f)
		{
			zero.x = GameManager.GetPlayerPoseForce(mCurrentMoveIndex).x;
		}
		else
		{
			zero.x = GameManager.GetPlayerPoseForce(mCurrentMoveIndex).x;
		}
		zero.y = GameManager.GetPlayerPoseForce(mCurrentMoveIndex).y;
		return zero;
	}

	private void Update()
	{
		if (mCurrentPlayerAnimation != null)
		{
			if (mCurrentMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_IDLE_LOOP)
			{
				mTimeBeforeWink -= Time.deltaTime;
			}
			BalletMoveAnimation balletMoveAnimation = mCurrentPlayerAnimation.GetComponent("BalletMoveAnimation") as BalletMoveAnimation;
			if (balletMoveAnimation != null)
			{
				if (!balletMoveAnimation.FinishedPlaying())
				{
					return;
				}
				if (mCurrentMoveIndex == Constants.PlayerMoves.PLAYER_MOVE_SUPER_POSE)
				{
					DanceSessionManager.ResetSuperPose();
					DanceSessionManager.Instance.SetSuperPoseMode(false);
					DanceSessionManager.Instance.DestoryAllFlowers();
				}
				if (mCurrentMoveIndex != Constants.PlayerMoves.PLAYER_MOVE_IDLE_LAND && mCurrentMoveIndex != Constants.PlayerMoves.PLAYER_MOVE_IDLE_LOOP && mCurrentMoveIndex != Constants.PlayerMoves.PLAYER_WINK_INDEX)
				{
					mCurrentMoveIndex = Constants.PlayerMoves.PLAYER_MOVE_IDLE_LAND;
					SwitchAnimation();
					PlayMoveAnimation();
					mPreviousMoveIndex = mCurrentMoveIndex;
					DisplayMagic(false, new Vector3(base.transform.localPosition.x, Constants.DesginerGlobals.MIN_PLAYER_POS_Y, base.transform.localPosition.z));
					base.transform.localPosition = new Vector3(base.transform.localPosition.x, Constants.DesginerGlobals.MIN_PLAYER_POS_Y, base.transform.localPosition.z);
				}
				else
				{
					if (mCurrentMoveIndex != Constants.PlayerMoves.PLAYER_MOVE_IDLE_LAND && mCurrentMoveIndex != Constants.PlayerMoves.PLAYER_MOVE_IDLE_LOOP && mCurrentMoveIndex != Constants.PlayerMoves.PLAYER_WINK_INDEX)
					{
						return;
					}
					if (mTimeBeforeWink <= 0f)
					{
						mTimeBeforeWink = Constants.DesginerGlobals.MAX_WINK_TIME;
						UISprite uISprite = mCurrentPlayerAnimation.GetComponent("UISprite") as UISprite;
						if (uISprite == null)
						{
							uISprite = mCurrentPlayerAnimation.AddComponent<UISprite>() as UISprite;
						}
						uISprite.spriteName = "Isabelle_pose_Idle_blink";
						uISprite.MakePixelPerfect();
						uISprite.depth = Constants.DesginerGlobals.PLAYERSPRITE_DEPTH;
						mCurrentMoveIndex = Constants.PlayerMoves.PLAYER_WINK_INDEX;
						mWinking = true;
						mWinkTimeLeft = Constants.DesginerGlobals.WINK_FRAME_TIME;
					}
					else if (mWinkTimeLeft > 0f)
					{
						mWinkTimeLeft -= Time.deltaTime;
					}
					else if (mWinkTimeLeft <= 0f)
					{
						mCurrentMoveIndex = Constants.PlayerMoves.PLAYER_MOVE_IDLE_LOOP;
						SwitchAnimation();
						PlayMoveAnimation();
						mPreviousMoveIndex = mCurrentMoveIndex;
					}
				}
			}
			else
			{
				MonoBehaviour.print("anim is null");
			}
		}
		else
		{
			MonoBehaviour.print("mCurrentAnimation is null");
			mCurrentMoveIndex = Constants.PlayerMoves.PLAYER_MOVE_IDLE_LAND;
			SwitchAnimation();
			PlayMoveAnimation();
			DisplayMagic(false, new Vector3(base.transform.localPosition.x, Constants.DesginerGlobals.MIN_PLAYER_POS_Y, base.transform.localPosition.z));
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, Constants.DesginerGlobals.MIN_PLAYER_POS_Y, base.transform.localPosition.z);
		}
	}
}
