using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Center On Child My")]
public class UICenterOnChild_My : MonoBehaviour
{
	public SpringPanel.OnFinished onFinished;

	public float strength = 8f;

	public bool isCenterNext;

	public AudioClip moveingSound;

	public GameObject target;

	public string functionName;

	public bool includeChildren;

	private UIDraggablePanel mDrag;

	private GameObject mCenteredObject;

	private Vector3 mLastPos;

	private Vector3 previousChild = Vector3.zero;

	public GameObject centeredObject
	{
		get
		{
			return mCenteredObject;
		}
	}

	private void OnEnable()
	{
		Recenter();
	}

	private void OnDragFinished()
	{
		if (base.enabled)
		{
			Recenter();
		}
	}

	public void OnDisable()
	{
		previousChild = Vector3.zero;
	}

	public void Recenter()
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
			Vector3 vector2 = cachedTransform.InverseTransformPoint(transform.position).ClampX(previousChild.x - 430f * AtlasManagerInfo.ResolutionMultiplier, previousChild.x + 430f * AtlasManagerInfo.ResolutionMultiplier);
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
			if (!(moveingSound != null) || mDrag.gameObject.transform.localPosition != cachedTransform.localPosition - vector4)
			{
			}
			SpringPanel.Begin(mDrag.gameObject, cachedTransform.localPosition - vector4, strength).onFinished = Send;
			previousChild = vector2;
		}
		else
		{
			mCenteredObject = null;
		}
	}

	private void Send()
	{
		if (string.IsNullOrEmpty(functionName))
		{
			return;
		}
		if (target == null)
		{
			target = base.gameObject;
		}
		if (includeChildren)
		{
			Transform[] componentsInChildren = target.GetComponentsInChildren<Transform>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(functionName, -1, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			target.SendMessage(functionName, -1, SendMessageOptions.DontRequireReceiver);
		}
	}
}
