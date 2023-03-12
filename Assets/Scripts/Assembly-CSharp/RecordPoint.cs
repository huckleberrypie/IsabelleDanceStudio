using System.Collections.Generic;
using UnityEngine;

public class RecordPoint : MonoBehaviour
{
	private int mStarPoint;

	private int mFlowerPoint;

	private List<GameObject> mCollidingObjs = new List<GameObject>();

	public int StarPoint
	{
		get
		{
			return mStarPoint;
		}
		set
		{
			mStarPoint = value;
		}
	}

	public int FlowerPoint
	{
		get
		{
			return mFlowerPoint;
		}
		set
		{
			mFlowerPoint = value;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!(collider != null) || !(collider.gameObject != null) || (mCollidingObjs.Count != 0 && mCollidingObjs.Contains(collider.gameObject)))
		{
			return;
		}
		int num = 1 * (mCollidingObjs.Count + 1);
		float num2 = collider.transform.localPosition.y + (GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos()) / 2f;
		float num3 = num2 / (GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos());
		num = ((num3 < Constants.DesginerGlobals.LOW_KICK_SCREEN_AREA) ? (num * 30) : ((num3 < Constants.DesginerGlobals.MID_KICK_SCREEN_AREA) ? (num * 25) : ((num3 < Constants.DesginerGlobals.HIGH_KICK_SCREEN_AREA) ? (num * 20) : ((num3 < Constants.DesginerGlobals.LOW_HAND_SCREEN_AREA) ? (num * 15) : ((!(num3 < Constants.DesginerGlobals.MID_HAND_SCREEN_AREA)) ? (num * 5) : (num * 10))))));
		if (!(collider.gameObject.tag == "Flower") || DanceSessionManager.Instance.EndingGameSlowly)
		{
			return;
		}
		if (collider.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics != null && (collider.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics).BlackStar)
		{
			playHitSound(0f);
			return;
		}
		if (!DanceSessionManager.Instance.FirstKickMode)
		{
			Scoreboard.Instance.AddPoints(mFlowerPoint * num);
			float num4 = mFlowerPoint * num;
			if (DanceSessionManager.SelectedMulitplierAmount != 0)
			{
				num4 *= (float)DanceSessionManager.SelectedMulitplierAmount;
			}
			SpawnManager.Instance.AddFloatingPoints(collider.gameObject.transform.localPosition, string.Empty + num4);
		}
		mCollidingObjs.Add(collider.gameObject);
		playHitSound(num3);
	}

	private void OnTriggerStay(Collider collider)
	{
		if (!(collider != null) || !(collider.gameObject != null) || (mCollidingObjs.Count != 0 && mCollidingObjs.Contains(collider.gameObject)))
		{
			return;
		}
		int num = 1 * (mCollidingObjs.Count + 1);
		float num2 = collider.transform.localPosition.y + (GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos()) / 2f;
		float num3 = num2 / (GameData.Instance.GetScreenTopPos() - GameData.Instance.GetScreenBottomPos());
		num = ((num3 < Constants.DesginerGlobals.LOW_KICK_SCREEN_AREA) ? (num * 30) : ((num3 < Constants.DesginerGlobals.MID_KICK_SCREEN_AREA) ? (num * 25) : ((num3 < Constants.DesginerGlobals.HIGH_KICK_SCREEN_AREA) ? (num * 20) : ((num3 < Constants.DesginerGlobals.LOW_HAND_SCREEN_AREA) ? (num * 15) : ((!(num3 < Constants.DesginerGlobals.MID_HAND_SCREEN_AREA)) ? (num * 5) : (num * 10))))));
		if (!(collider.gameObject.tag == "Flower") || DanceSessionManager.Instance.EndingGameSlowly)
		{
			return;
		}
		if (collider.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics != null && (collider.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics).BlackStar)
		{
			playHitSound(0f, true);
			return;
		}
		if (!DanceSessionManager.Instance.FirstKickMode)
		{
			Scoreboard.Instance.AddPoints(mFlowerPoint * num);
			float num4 = mFlowerPoint * num;
			if (DanceSessionManager.SelectedMulitplierAmount != 0)
			{
				num4 *= (float)DanceSessionManager.SelectedMulitplierAmount;
			}
			SpawnManager.Instance.AddFloatingPoints(collider.gameObject.transform.localPosition, string.Empty + num4);
		}
		mCollidingObjs.Add(collider.gameObject);
		playHitSound(num3);
	}

	private void playHitSound(float screenPercentage, bool black = false)
	{
		string text = "hit_sequin_tone01";
		text = ((screenPercentage < Constants.DesginerGlobals.LOW_KICK_SCREEN_AREA) ? "hit_sequin_tone01" : ((screenPercentage < Constants.DesginerGlobals.MID_KICK_SCREEN_AREA) ? "hit_sequin_tone02" : ((screenPercentage < Constants.DesginerGlobals.HIGH_KICK_SCREEN_AREA) ? "hit_sequin_tone03" : ((screenPercentage < Constants.DesginerGlobals.LOW_HAND_SCREEN_AREA) ? "hit_sequin_tone04" : ((!(screenPercentage < Constants.DesginerGlobals.MID_HAND_SCREEN_AREA)) ? "hit_sequin_tone06" : "hit_sequin_tone05")))));
		if (black)
		{
			AudioManager.Sound.PlaySoundOneShot("hit_sequin_negative");
		}
		if (Scoreboard.Instance.CollectedPoints > PlayerManager.Instance.BestScore && !Scoreboard.Instance.AnnouncedNewBestScore)
		{
			MonoBehaviour.print("playing new best score sound");
			AudioManager.Sound.PlaySoundByOrder(string.Empty + text + ",achievement_newbest", false);
			Scoreboard.Instance.AnnouncedNewBestScore = true;
		}
		else if (mCollidingObjs.Count == 1)
		{
			AudioManager.Sound.PlaySoundOneShot(text);
		}
		else if (mCollidingObjs.Count > 1)
		{
			AudioManager.Sound.PlaySoundOneShot("achievement_combo");
		}
	}
}
