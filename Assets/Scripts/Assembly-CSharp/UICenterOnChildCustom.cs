using UnityEngine;

public class UICenterOnChildCustom : MonoBehaviour
{
	public delegate void OnUpdateCenteredObject();

	public float springStrength = 8f;

	public SpringPanel.OnFinished onFinished;

	public OnUpdateCenteredObject onUpdateCenteredObject;

	private UIDraggablePanel mDrag;

	private GameObject mCenteredObject;

	public bool isCenterNext = true;

	public GameObject centeredObject
	{
		get
		{
			return mCenteredObject;
		}
	}

	private void OnEnable()
	{
		Recenter(true);
	}

	private void OnDragFinished()
	{
		if (base.enabled)
		{
			Recenter(false);
		}
	}

	private void Awake()
	{
		int i = 0;
		for (int childCount = base.transform.childCount; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			child.name = i.ToString();
		}
	}

	private void OnDisable()
	{
	}

	public void Recenter(bool firstCall)
	{
		if (mDrag == null)
		{
			mDrag = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
			if (mDrag == null)
			{
				Debug.LogWarning(string.Concat(GetType(), " requires ", typeof(UIDraggablePanel), " on a parent object in order to work"), this);
				base.enabled = false;
				return;
			}
			mDrag.onDragFinished = OnDragFinished;
			if (mDrag.horizontalScrollBar != null)
			{
				mDrag.horizontalScrollBar.onDragFinished = OnDragFinished;
			}
			if (mDrag.verticalScrollBar != null)
			{
				mDrag.verticalScrollBar.onDragFinished = OnDragFinished;
			}
		}
		if (mDrag.panel == null)
		{
			return;
		}
		if (firstCall)
		{
			mDrag.MoveRelative(new Vector3(0f - mDrag.transform.localPosition.x, 0f, 0f));
		}
		Vector4 clipRange = mDrag.panel.clipRange;
		Transform cachedTransform = mDrag.panel.cachedTransform;
		Vector3 localPosition = cachedTransform.localPosition;
		localPosition.x += clipRange.x;
		localPosition.y += clipRange.y;
		localPosition = cachedTransform.parent.TransformPoint(localPosition);
		Vector3 vector = localPosition - mDrag.currentMomentum * (mDrag.momentumAmount * 0.1f);
		mDrag.currentMomentum = Vector3.zero;
		float num = float.MaxValue;
		Transform transform = null;
		Transform transform2 = base.transform;
		int num2 = -1;
		int i = 0;
		for (int childCount = transform2.childCount; i < childCount; i++)
		{
			Transform child = transform2.GetChild(i);
			float num3 = Vector3.SqrMagnitude(child.position - vector);
			if (num3 < num)
			{
				num = num3;
				transform = child;
				num2 = i;
			}
		}
		if (isCenterNext && UICamera.currentTouch != null)
		{
			if (UICamera.currentTouch.totalDelta.x > 50f)
			{
				if (num2 - 1 >= 0)
				{
					transform = transform2.GetChild(num2 - 1);
				}
			}
			else if (UICamera.currentTouch.totalDelta.x < -50f && num2 + 1 < transform2.childCount)
			{
				transform = transform2.GetChild(num2 + 1);
			}
		}
		if (transform != null)
		{
			mCenteredObject = transform.gameObject;
			if (onUpdateCenteredObject != null)
			{
				onUpdateCenteredObject();
			}
			Vector3 vector2 = cachedTransform.InverseTransformPoint(transform.position);
			Vector3 vector3 = cachedTransform.InverseTransformPoint(localPosition);
			Vector3 vector4 = vector2 - vector3;
			if (mDrag.scale.x == 0f)
			{
				vector4.x = 0f;
			}
			if (mDrag.scale.y == 0f)
			{
				vector4.y = 0f;
			}
			if (mDrag.scale.z == 0f)
			{
				vector4.z = 0f;
			}
			SpringPanel.Begin(mDrag.gameObject, cachedTransform.localPosition - vector4, springStrength).onFinished = onFinished;
		}
		else
		{
			mCenteredObject = null;
		}
	}
}
