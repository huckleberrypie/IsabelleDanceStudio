using UnityEngine;

public class BouncePackage : MonoBehaviour
{
	private int mHiddenItemID;

	public int HiddenItem
	{
		get
		{
			return mHiddenItemID;
		}
		set
		{
			mHiddenItemID = value;
		}
	}
}
