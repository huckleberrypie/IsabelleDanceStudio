using UnityEngine;

public class FollowObjectCenter : MonoBehaviour
{
	private GameObject mOtherObject;

	private float mTime;

	public GameObject OtherObject
	{
		get
		{
			return mOtherObject;
		}
		set
		{
			mOtherObject = value;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		mTime += Time.deltaTime;
		if (mOtherObject != null)
		{
			base.transform.localPosition = mOtherObject.transform.localPosition;
		}
		else if (mTime > 1f)
		{
			Object.Destroy(base.transform.gameObject);
		}
	}
}
