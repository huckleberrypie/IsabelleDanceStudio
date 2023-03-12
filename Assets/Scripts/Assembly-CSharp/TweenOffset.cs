using UnityEngine;

[AddComponentMenu("NGUI/Tween/Offset")]
public class TweenOffset : UITweener
{
	public Vector2 from;

	public Vector2 to;

	private Transform mTrans;

	private UIAnchor mAnchor;

	public Vector2 offset
	{
		get
		{
			if (mAnchor != null)
			{
				return mAnchor.relativeOffset;
			}
			return Vector2.zero;
		}
		set
		{
			if (mAnchor != null)
			{
				mAnchor.relativeOffset = value;
			}
		}
	}

	private void Awake()
	{
		mAnchor = GetComponent<UIAnchor>();
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		offset = from * (1f - factor) + to * factor;
	}

	public static TweenOffset Begin(GameObject go, float duration, Vector2 offset)
	{
		TweenOffset tweenOffset = UITweener.Begin<TweenOffset>(go, duration);
		tweenOffset.from = tweenOffset.offset;
		tweenOffset.to = offset;
		if (duration <= 0f)
		{
			tweenOffset.Sample(1f, true);
			tweenOffset.enabled = false;
		}
		return tweenOffset;
	}
}
