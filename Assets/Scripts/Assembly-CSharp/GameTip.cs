using UnityEngine;

public class GameTip : MonoBehaviour
{
	private Enums.GameTip mTipIndex = Enums.GameTip.NONE;

	public void SetTip(Enums.GameTip tip)
	{
		mTipIndex = tip;
	}

	private void Update()
	{
		bool flag = false;
		switch (mTipIndex)
		{
		case Enums.GameTip.BLACK_PEARL:
			if (!PlayerManager.Instance.SeenBlackPearl)
			{
				flag = true;
			}
			break;
		case Enums.GameTip.BOUNCE_UP:
			if (!PlayerManager.Instance.SeenBounceUp)
			{
				flag = true;
			}
			break;
		case Enums.GameTip.DOUBLE:
			if (!PlayerManager.Instance.SeenDouble)
			{
				flag = true;
			}
			break;
		case Enums.GameTip.MULITPLER:
			if (!PlayerManager.Instance.SeenMultiplier)
			{
				flag = true;
			}
			break;
		case Enums.GameTip.SEQUINS:
			if (!PlayerManager.Instance.SeenGems)
			{
				flag = true;
			}
			break;
		case Enums.GameTip.SUPERPOSE:
			if (!PlayerManager.Instance.SeenSuperPose)
			{
				flag = true;
			}
			break;
		}
		if (base.transform.localPosition.x > GameData.Instance.GetLeftMaxPos() + (float)Constants.DesginerGlobals.SEQUIN_WIDTH && base.transform.localPosition.y < GameData.Instance.GetScreenTopPos() - (float)Constants.DesginerGlobals.SEQUIN_WIDTH && flag && !DanceSessionManager.Instance.ShowingTip)
		{
			DanceSessionManager.Instance.CurrentTip = mTipIndex;
			GameScreenController.Instance.ShowTipPanel(mTipIndex);
			DanceSessionManager.Instance.ShowingTip = true;
			PlayerManager.Instance.TipUnlocked(mTipIndex);
		}
	}
}
