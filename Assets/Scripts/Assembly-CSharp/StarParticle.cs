using UnityEngine;

public class StarParticle : MonoBehaviour
{
	private float mTimeSpanLeft;

	private float mCurrUpdateTime;

	private float mPrevUpdateTime;

	private bool initialized;

	public void InitializeEffect(Vector3 position, float fadeOutTime, float destroyTime)
	{
		base.transform.localPosition = position;
		mTimeSpanLeft = destroyTime;
		int num = Random.Range(-30, 30);
		int num2 = Random.Range(-30, 30);
		if (base.transform.GetComponent<Rigidbody>() == null)
		{
			base.transform.gameObject.AddComponent<Rigidbody>();
		}
		if (base.transform.GetComponent<Rigidbody>() != null)
		{
			base.transform.GetComponent<Rigidbody>().useGravity = false;
			base.transform.GetComponent<Rigidbody>().AddForce(new Vector3(num2, num, 0f), ForceMode.Acceleration);
		}
		mPrevUpdateTime = (mCurrUpdateTime = Time.time);
		initialized = true;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (initialized)
		{
			mCurrUpdateTime = Time.time;
			float num = mCurrUpdateTime - mPrevUpdateTime;
			mPrevUpdateTime = mCurrUpdateTime;
			if (!GameManager.Instance.IsGamePaused())
			{
				mTimeSpanLeft -= num;
			}
			if (mTimeSpanLeft <= 0f)
			{
				Object.Destroy(base.transform.gameObject);
			}
		}
	}
}
