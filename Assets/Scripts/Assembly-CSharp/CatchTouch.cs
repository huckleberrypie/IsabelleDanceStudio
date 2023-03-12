using UnityEngine;

public class CatchTouch : MonoBehaviour
{
	private bool mTouchBegan;

	private Vector2 firstPos = Vector2.zero;

	private bool mTouchedMe;

	public bool MouseTouchedMe
	{
		get
		{
			return mTouchedMe;
		}
	}

	private void Update()
	{
		if (GameManager.Instance.IsGamePaused())
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			mTouchedMe = false;
			mTouchBegan = true;
			firstPos = Input.mousePosition;
		}
		if (!mTouchBegan)
		{
			return;
		}
		mTouchBegan = false;
		if (mTouchBegan || !base.transform.gameObject.GetComponent("BoxCollider"))
		{
			return;
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("PoseCollider");
		if (gameObject != null)
		{
			if (base.transform.GetComponent<Collider>().bounds.Contains(new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0f)))
			{
				mTouchedMe = true;
			}
			if (gameObject.transform.GetComponent<Collider>().bounds.Contains(new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, 0f)))
			{
				mTouchedMe = true;
			}
		}
	}

	public void ResetTouch()
	{
		mTouchedMe = false;
	}
}
