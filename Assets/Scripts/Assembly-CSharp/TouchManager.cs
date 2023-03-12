using UnityEngine;

public class TouchManager : MonoBehaviour
{
	private static TouchManager _instance;

	private bool mTouchBegan;

	private Vector2 lastDragPos = Vector2.zero;

	private Vector2 lastClickedPos = Vector2.zero;

	private bool mRecordTouch;

	private Vector2 mSwipeDirection = Vector2.zero;

	public static TouchManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(TouchManager)) as TouchManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<TouchManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public Vector2 SwipeDirection
	{
		get
		{
			return mSwipeDirection;
		}
	}

	public void StartRecording()
	{
		mRecordTouch = true;
	}

	public void EndRecording()
	{
		mRecordTouch = false;
	}

	private void Update()
	{
		if (!mRecordTouch)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0) && !mTouchBegan)
		{
			mTouchBegan = true;
			lastDragPos = Vector2.zero;
			lastClickedPos = Input.mousePosition;
			lastClickedPos.x -= Screen.width / 2;
			lastClickedPos.y -= Screen.height / 2;
			mSwipeDirection = Vector2.zero;
		}
		if (mTouchBegan)
		{
			if (Input.GetAxis("Mouse X") < 0f)
			{
				mSwipeDirection = new Vector2(-1f, 1f);
			}
			if (Input.GetAxis("Mouse X") > 0f)
			{
				mSwipeDirection = new Vector2(1f, 1f);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			mTouchBegan = false;
		}
	}
}
