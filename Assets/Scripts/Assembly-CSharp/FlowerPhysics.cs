using System.Collections;
using UnityEngine;

public class FlowerPhysics : MonoBehaviour
{
	private float mGravity = -0.53f;

	private float mSuperPoseGravity = -0.1f;

	private float mFallingCount;

	private float mDirectionalForce;

	private int mPlayedBounceSound;

	private bool mTouchedOnce;

	private bool isBlackStar;

	public bool BlackStar
	{
		get
		{
			return isBlackStar;
		}
		set
		{
			isBlackStar = value;
		}
	}

	public void ResetBounceSound()
	{
		mPlayedBounceSound = 0;
	}

	public void setDirectionalForce(float force)
	{
		mDirectionalForce = force;
		mTouchedOnce = true;
		mFallingCount = 0f;
	}

	private void FixedUpdate()
	{
		float num = mGravity;
		float num2 = 0f;
		if (DanceSessionManager.Instance.IsSuperPoseMode())
		{
			num = mSuperPoseGravity;
		}
		if (DanceSessionManager.Instance.EndingGameSlowly)
		{
			num = 0.2f;
			mFallingCount = -1f;
			base.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
			mDirectionalForce = 0f;
		}
		if (DanceSessionManager.Instance.ShowingTip)
		{
			mFallingCount = -1f;
			base.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		if (base.transform.GetComponent<Rigidbody>() != null)
		{
			if (base.transform.GetComponent<Rigidbody>().velocity.y != 0f && DanceSessionManager.Instance.FirstKickMode)
			{
				DanceSessionManager.Instance.FirstKickMode = false;
			}
			if (base.transform.GetComponent<Rigidbody>().velocity.y < 0f && mTouchedOnce)
			{
				mFallingCount += 1f;
				num += mFallingCount * Constants.DesginerGlobals.FLOWER_GRAVITY_ACCELARATION_INTERVAL;
				num2 += mDirectionalForce;
			}
			else
			{
				mFallingCount = 0f;
				num2 = mDirectionalForce;
			}
			if (num < -2f)
			{
				num = -2f;
			}
			base.transform.GetComponent<Rigidbody>().AddForce(mDirectionalForce, num, 0f, ForceMode.Acceleration);
		}
	}

	private void Update()
	{
		if (DanceSessionManager.Instance.ShowingTip)
		{
			return;
		}
		float num = -1.5f * base.transform.GetComponent<Rigidbody>().velocity.x;
		if (base.transform.localPosition.x + (float)(Constants.DesginerGlobals.SEQUIN_WIDTH / 2) >= GameData.Instance.GetRightMaxPos())
		{
			if (num > -5.5f && num < 5.5f)
			{
				num = -5.5f;
			}
			base.transform.GetComponent<Rigidbody>().AddForce(num, 0f, 0f, ForceMode.Acceleration);
			setDirectionalForce(0f - Constants.DesginerGlobals.FLOWER_DIRECTIONAL_FORCE);
			if (mPlayedBounceSound != 1)
			{
				AudioManager.Sound.PlaySoundOneShot("hit_sequinbounce");
				mPlayedBounceSound = 1;
			}
		}
		if (base.transform.localPosition.x - (float)(Constants.DesginerGlobals.SEQUIN_WIDTH / 2) <= GameData.Instance.GetLeftMaxPos())
		{
			if (num > -5.5f && num < 5.5f)
			{
				num = 5.5f;
			}
			base.transform.GetComponent<Rigidbody>().AddForce(num, 0f, 0f, ForceMode.Acceleration);
			setDirectionalForce(Constants.DesginerGlobals.FLOWER_DIRECTIONAL_FORCE);
			if (mPlayedBounceSound != 2)
			{
				AudioManager.Sound.PlaySoundOneShot("hit_sequinbounce");
				mPlayedBounceSound = 2;
			}
		}
		if (base.transform.localPosition.y - (float)(Constants.DesginerGlobals.SEQUIN_WIDTH / 2) <= GameData.Instance.GetFlowerBounceYPos() && DanceSessionManager.Instance.IsFlowerBounceMode())
		{
			if (!BlackStar && mPlayedBounceSound != 3)
			{
				base.transform.GetComponent<Rigidbody>().velocity = new Vector3(base.transform.GetComponent<Rigidbody>().velocity.x, 0f, 0f);
				float directionalForce = ((!(base.transform.GetComponent<Rigidbody>().velocity.x > 0f)) ? (0f - Constants.DesginerGlobals.FLOWER_DIRECTIONAL_FORCE) : Constants.DesginerGlobals.FLOWER_DIRECTIONAL_FORCE);
				setDirectionalForce(directionalForce);
				base.transform.GetComponent<Rigidbody>().AddForce(base.transform.GetComponent<Rigidbody>().velocity.x, GameData.FlowerBounceForce.y, 0f, ForceMode.Acceleration);
				AudioManager.Sound.PlaySoundOneShot("sequin_barrierbounce");
				mPlayedBounceSound = 3;
				mTouchedOnce = true;
			}
		}
		else if (base.transform.localPosition.y - (float)(Constants.DesginerGlobals.SEQUIN_WIDTH / 2) < GameData.Instance.GetBottomMaxFlowerPos() && !DanceSessionManager.Instance.FirstKickMode)
		{
			if (!BlackStar && !DanceSessionManager.Instance.EndingGameSlowly)
			{
				PrepareEndGame();
				// KontagentManager.Instance.SendCustomEvent("Game_Ended", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Star_Hit_Ground");
			}
			else if (base.transform.GetComponent("DestroyOffScreen") == null)
			{
				base.transform.gameObject.AddComponent<DestroyOffScreen>();
			}
		}
		else if (mPlayedBounceSound == 3)
		{
			mPlayedBounceSound = 0;
		}
	}

	public void PrepareEndGame()
	{
		if (!AudioManager.Music.IsPlaying("music_sting_gamecomplete"))
		{
			AudioManager.Music.PlayMusicOneShot("music_sting_gamecomplete");
		}
		DanceSessionManager.Instance.EndingGameSlowly = true;
		base.transform.gameObject.AddComponent<GameOverSequin>();
		AudioManager.Sound.PlaySoundOneShot("sequin_hitfloor");
		DanceSessionManager.Instance.FadeAwayAllFlowersAndItems();
		GameScreenController.Instance.ShowGameOverPanel();
		StartCoroutine(EndGame(Constants.DesginerGlobals.END_GAME_DELAY));
	}

	private IEnumerator EndGame(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		GameManager.Instance.EndGameMode();
	}
}
