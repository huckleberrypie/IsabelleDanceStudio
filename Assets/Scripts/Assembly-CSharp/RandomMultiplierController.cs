using UnityEngine;

public class RandomMultiplierController : MonoBehaviour
{
	private int mMultiplierAmount;

	private int mMultiplierType;

	public int MultiplierAmount
	{
		get
		{
			return mMultiplierAmount;
		}
	}

	public int MultiplierType
	{
		get
		{
			return mMultiplierType;
		}
		set
		{
			UISprite uISprite = base.transform.GetComponent("UISprite") as UISprite;
			mMultiplierType = value;
			if (mMultiplierType == Constants.PowerUps.PowerUpMulitpliers.TWO_TIMES)
			{
				mMultiplierAmount = 2;
				uISprite.spriteName = Constants.Sprites.Game.RANDOM_MULTIPLIER_POWERUP;
			}
			else if (mMultiplierType == Constants.PowerUps.PowerUpMulitpliers.THREE_TIMES)
			{
				mMultiplierAmount = 3;
				uISprite.spriteName = Constants.Sprites.Game.RANDOM_MULTIPLIER_POWERUP;
			}
			else if (mMultiplierType == Constants.PowerUps.PowerUpMulitpliers.FIVE_TIMES)
			{
				mMultiplierAmount = 5;
				uISprite.spriteName = Constants.Sprites.Game.RANDOM_MULTIPLIER_POWERUP;
			}
		}
	}
}
