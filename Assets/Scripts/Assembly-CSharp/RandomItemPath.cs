using UnityEngine;

public class RandomItemPath : MonoBehaviour
{
	private float mGravity;

	private float mDirectionalForce;

	private bool mGoAcross;

	private bool mFallDown;

	private bool mModePresent;

	public void StartPath()
	{
		int num = Random.Range(1, 30);
		if (num % 2 == 0)
		{
			mGoAcross = true;
		}
		else
		{
			mFallDown = true;
		}
		if (mGoAcross)
		{
			base.transform.localPosition = new Vector3(GameData.Instance.GetLeftMaxPos(), Random.Range(Constants.DesginerGlobals.ITEM_MIN_Y, Constants.DesginerGlobals.ITEM_MAX_Y), 0f);
			mDirectionalForce = Random.Range(Constants.DesginerGlobals.ITEM_MIN_SPEED, Constants.DesginerGlobals.ITEM_MAX_SPEED);
		}
		if (mFallDown)
		{
			base.transform.localPosition = new Vector3(Random.Range(Constants.DesginerGlobals.ITEM_MIN_X, Constants.DesginerGlobals.ITEM_MAX_X), GameData.Instance.GetScreenTopPos(), 0f);
			mGravity = 0f - Random.Range(Constants.DesginerGlobals.ITEM_MIN_SPEED, Constants.DesginerGlobals.ITEM_MAX_SPEED);
		}
		if (base.transform.GetComponent("DestroyOffScreen") == null)
		{
			base.transform.gameObject.AddComponent<DestroyOffScreen>();
		}
		base.transform.GetComponent<Rigidbody>().AddForce(mDirectionalForce, mGravity, 0f, ForceMode.VelocityChange);
	}

	private void FixedUpdate()
	{
		float num = 0f;
		float y = 0f;
		if (DanceSessionManager.Instance.EndingGameSlowly)
		{
			if (mFallDown)
			{
				y = -0.2f;
			}
			if (mGoAcross)
			{
				num *= 0.1f;
			}
			if (base.transform.GetComponent<Rigidbody>() != null)
			{
				base.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
				base.transform.GetComponent<Rigidbody>().AddForce(num, y, 0f, ForceMode.VelocityChange);
			}
		}
		if (DanceSessionManager.Instance.IsSuperPoseMode())
		{
			mModePresent = true;
			if (mFallDown)
			{
				y = -0.1f;
			}
			if (mGoAcross)
			{
				num *= 0.1f;
			}
			if (base.transform.GetComponent<Rigidbody>() != null)
			{
				base.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
				base.transform.GetComponent<Rigidbody>().AddForce(num, y, 0f, ForceMode.VelocityChange);
			}
		}
		else if (mModePresent)
		{
			base.transform.GetComponent<Rigidbody>().AddForce(mDirectionalForce, mGravity, 0f, ForceMode.VelocityChange);
			mModePresent = false;
		}
	}
}
