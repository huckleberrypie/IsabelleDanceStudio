using UnityEngine;

public class TriggerPlayerEvent : MonoBehaviour
{
	private bool mActive = true;

	private Vector3 mLastClickedPos = Vector3.zero;

	private int mUpdateCallCount;

	public void setProperties(Vector3 pos)
	{
		mLastClickedPos = pos;
	}

	private void OnTriggerEnter(Collider collider)
	{
		TakeAction(collider);
	}

	private void OnTriggerStay(Collider collider)
	{
		TakeAction(collider);
	}

	private void TakeAction(Collider collider)
	{
		if (!mActive)
		{
			return;
		}
		mActive = false;
		if (collider.gameObject.CompareTag("SuperPoseCollider"))
		{
			PerformSuperPose();
		}
		if (collider.gameObject.CompareTag("BouncePackage"))
		{
			PerformRegularPose();
			BouncePackage bouncePackage = collider.gameObject.GetComponent("BouncePackage") as BouncePackage;
			if (bouncePackage.HiddenItem == Constants.PowerUps.FLOWER_BOUNCE_ID)
			{
				if (PlayerManager.Instance.SeenBounceUp)
				{
					SpawnManager.Instance.AddPowerBurst(collider.gameObject.transform.localPosition, collider.gameObject);
					BouncePackageTriggered();
					Object.Destroy(collider.gameObject);
					AudioManager.Sound.PlaySoundOneShot("powerup_sequinbounce");
					// KontagentManager.Instance.SendCustomEvent("Game_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Bounce_PowerUp_Collected");
				}
			}
			else if (bouncePackage.HiddenItem == Constants.PowerUps.POSE_UP_ID)
			{
				SpawnManager.Instance.AddPowerBurst(collider.gameObject.transform.localPosition, collider.gameObject);
				DanceSessionManager.CollectedPoseUp = true;
				Object.Destroy(collider.gameObject);
				SpawnManager.Instance.AddPowerNotifier(mLastClickedPos, "PoseUp");
			}
			else if (bouncePackage.HiddenItem == Constants.PowerUps.DOUBLE_STARS_ID && PlayerManager.Instance.SeenDouble)
			{
				SpawnManager.Instance.AddPowerBurst(collider.gameObject.transform.localPosition, collider.gameObject);
				DanceSessionManager.CollectedDoubleStar = true;
				Object.Destroy(collider.gameObject);
				AudioManager.Sound.PlaySoundOneShot("powerup_doublestars");
				SpawnManager.Instance.AddFloatingPoints(mLastClickedPos, "Gems 2x", false, true);
				// KontagentManager.Instance.SendCustomEvent("Game_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "DoubleSequins_PowerUp_Collected");
			}
		}
		else if (collider.gameObject.CompareTag("RandomMulitplier"))
		{
			PerformRegularPose();
			if (PlayerManager.Instance.SeenMultiplier)
			{
				RandomMultiplierController randomMultiplierController = collider.gameObject.GetComponent("RandomMultiplierController") as RandomMultiplierController;
				if (randomMultiplierController != null)
				{
					DanceSessionManager.SelectedMulitplierAmount = randomMultiplierController.MultiplierAmount;
					DanceSessionManager.SelectedMultiplierTimeLeft = GameData.RandomMultiplierLifeTime;
				}
				SpawnManager.Instance.AddPowerBurst(collider.gameObject.transform.localPosition, collider.gameObject);
				AudioManager.Sound.PlaySoundOneShot("powerup_multiplier");
				Object.Destroy(collider.gameObject);
				SpawnManager.Instance.AddFloatingPoints(mLastClickedPos, string.Empty + DanceSessionManager.SelectedMulitplierAmount + "X", true);
				// KontagentManager.Instance.SendCustomEvent("Game_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Multiplier_PowerUp_Collected");
			}
		}
		else
		{
			PerformRegularPose();
		}
	}

	private void PerformSuperPose()
	{
		AudioManager.Sound.PlaySoundOneShot("powerup_superpose");
		DanceSessionManager.Instance.SetSuperPoseMode(true);
		if ((bool)GameManager.PlayerObject.GetComponent("PlayerController"))
		{
			PlayerController playerController = GameManager.PlayerObject.GetComponent("PlayerController") as PlayerController;
			playerController.PerformMove(new Vector3(mLastClickedPos.x, mLastClickedPos.y, 0f), TouchManager.Instance.SwipeDirection);
		}
	}

	private void PerformRegularPose()
	{
		if ((bool)GameManager.PlayerObject.GetComponent("PlayerController"))
		{
			PlayerController playerController = GameManager.PlayerObject.GetComponent("PlayerController") as PlayerController;
			playerController.PerformMove(new Vector3(mLastClickedPos.x, mLastClickedPos.y, 0f), TouchManager.Instance.SwipeDirection);
		}
	}

	private void Update()
	{
		mUpdateCallCount++;
		if (mActive && mUpdateCallCount > 1)
		{
			mActive = false;
			PerformRegularPose();
		}
	}

	private void BouncePackageTriggered()
	{
		DanceSessionManager.Instance.SetFlowerBounceMode(true);
	}
}
