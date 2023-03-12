using System.Collections.Generic;
using UnityEngine;

public class ApplyForceOnCollision : MonoBehaviour
{
	private float mYForce;

	private float mXForce;

	public bool mFlipForceX;

	public bool mFlipForceY;

	private bool mPlayedSound;

	private float mUpdateCallCount;

	private bool mHitFlower;

	private List<GameObject> mCollidingObjs = new List<GameObject>();

	public void SetForce(Vector2 force)
	{
		mXForce = force.x;
		mYForce = force.y;
	}

	public void FlipForceX(bool val)
	{
		mFlipForceX = val;
	}

	public void FlipForceY(bool val)
	{
		mFlipForceY = val;
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!(collider != null) || !(collider.gameObject != null) || (mCollidingObjs.Count != 0 && mCollidingObjs.Contains(collider.gameObject)) || !(collider.gameObject.tag == "Flower"))
		{
			return;
		}
		if (base.transform.gameObject.tag == "SuperPoseCollider")
		{
			DestroyCollidingObject(collider, 1f);
		}
		if (base.transform.gameObject.CompareTag("PoseCollider") && !GameManager.Instance.HasGameStarted())
		{
			GameManager.Instance.EnterGameMode();
			if ((bool)collider.GetComponent<Rigidbody>())
			{
				collider.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		mHitFlower = true;
		ApplyForceToObject(collider);
		mCollidingObjs.Add(collider.gameObject);
	}

	private void OnTriggerStay(Collider collider)
	{
		if (!(collider != null) || !(collider.gameObject != null) || (mCollidingObjs.Count != 0 && mCollidingObjs.Contains(collider.gameObject)) || !(collider.gameObject.tag == "Flower"))
		{
			return;
		}
		if (base.transform.gameObject.tag == "SuperPoseCollider")
		{
			DestroyCollidingObject(collider, 1f);
		}
		if (base.transform.gameObject.CompareTag("PoseCollider") && !GameManager.Instance.HasGameStarted())
		{
			GameManager.Instance.EnterGameMode();
			if ((bool)collider.GetComponent<Rigidbody>())
			{
				collider.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		mHitFlower = true;
		ApplyForceToObject(collider);
		mCollidingObjs.Add(collider.gameObject);
	}

	private void OnTriggernExit(Collider collider)
	{
	}

	private void ApplyForceToObject(Collider collider)
	{
		if (!(collider != null))
		{
			return;
		}
		Rigidbody rigidbody = collider.transform.GetComponent<Rigidbody>();
		if (!(rigidbody != null))
		{
			return;
		}
		Vector3 velocity = rigidbody.velocity;
		float num = 0f;
		float num2 = 0f;
		if (!mFlipForceY)
		{
			if (mYForce != 0f)
			{
				num = mYForce;
			}
		}
		else
		{
			num = -1f * velocity.y;
		}
		if (!mFlipForceX)
		{
			if (mXForce != 0f)
			{
				num2 = mXForce;
			}
		}
		else
		{
			num2 = -1f * velocity.x;
		}
		FlowerPhysics flowerPhysics = collider.transform.gameObject.GetComponent("FlowerPhysics") as FlowerPhysics;
		if (!(flowerPhysics != null))
		{
			return;
		}
		mHitFlower = true;
		if ((flowerPhysics.BlackStar && DanceSessionManager.Instance.IsSuperPoseMode()) || (flowerPhysics.BlackStar && collider.transform.localPosition.y - (float)(Constants.DesginerGlobals.SEQUIN_WIDTH / 2) >= GameData.Instance.GetScreenTopPos()) || (flowerPhysics.BlackStar && !PlayerManager.Instance.SeenBlackPearl))
		{
			return;
		}
		float num3 = Random.Range((0f - num2) / 5f, num2 / 5f);
		float num4 = Random.Range((0f - num) / 8f, num / 8f);
		flowerPhysics.ResetBounceSound();
		rigidbody.velocity = Vector3.zero;
		rigidbody.AddForce(num2 + num3, num + num4, 0f, ForceMode.Acceleration);
		flowerPhysics.setDirectionalForce((!(num2 > 0f)) ? (0f - Constants.DesginerGlobals.FLOWER_DIRECTIONAL_FORCE) : Constants.DesginerGlobals.FLOWER_DIRECTIONAL_FORCE);
		if (!DanceSessionManager.Instance.FirstKickMode)
		{
			SpawnManager.Instance.AddStarBurst(collider.transform.localPosition, flowerPhysics.BlackStar);
			if (!flowerPhysics.BlackStar)
			{
				SpawnManager.Instance.AddStarParticleSystem(collider.transform.localPosition);
			}
		}
		if (flowerPhysics.BlackStar)
		{
			flowerPhysics.PrepareEndGame();
			// KontagentManager.Instance.SendCustomEvent("Game_Ended", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Hit_Black_Pearl");
		}
		if (base.transform.CompareTag("PoseCollider"))
		{
			Object.Destroy(base.transform.gameObject);
		}
	}

	private void DestroyCollidingObject(Collider collider, float time)
	{
		DestroyAfterTime destroyAfterTime = collider.gameObject.AddComponent<DestroyAfterTime>() as DestroyAfterTime;
		if (destroyAfterTime != null)
		{
			destroyAfterTime.lifeCycle = time;
		}
	}

	private void Update()
	{
	}
}
