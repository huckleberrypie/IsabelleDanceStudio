using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	private float mLastTime;

	private float lifeTimeLength;

	public float lifeCycle = 10f;

	private void Update()
	{
		if (mLastTime == 0f)
		{
			mLastTime = Time.time;
			lifeTimeLength = 0f;
		}
		if (!GameManager.Instance.IsGamePaused())
		{
			float num = Time.time - mLastTime;
			if (!GameManager.Instance.IsGamePaused())
			{
				lifeTimeLength += num;
			}
			if (lifeTimeLength >= lifeCycle)
			{
				Object.Destroy(base.transform.gameObject);
			}
			mLastTime = Time.time;
		}
	}
}
