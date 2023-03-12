using System;
using UnityEngine;

public class TipScreenController : MonoBehaviour
{
	private static TipScreenController _instance;

	public GameObject exitButton;

	public UILabel titleLabel;

	public UILabel descLabel;

	public UISprite item;

	public UISprite itemGlow;

	public GameObject sequinsRow;

	public static TipScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(TipScreenController)) as TipScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<TipScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(exitButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(GoBackToGame));
	}

	public void OnEnable()
	{
		NGUITools.SetActive(itemGlow.gameObject, false);
		NGUITools.SetActive(item.gameObject, false);
		sequinsRow.SetActive(false);
		DanceSessionManager.Instance.DeActivateDanceColliders();

		titleLabel.text = "TIP";
		if (DanceSessionManager.Instance.CurrentTip == Enums.GameTip.BLACK_PEARL)
		{
			descLabel.text = "Don’t hit the black pearl!";
			item.spriteName = Constants.Sprites.Game.BLACK_STAR;
			itemGlow.spriteName = Constants.Sprites.Game.BLACK_STAR_GLOW;
			item.MakePixelPerfect();
			itemGlow.MakePixelPerfect();
			NGUITools.SetActive(itemGlow.gameObject, true);
			NGUITools.SetActive(item.gameObject, true);
		}
		else if (DanceSessionManager.Instance.CurrentTip == Enums.GameTip.BOUNCE_UP)
		{
			descLabel.text = "Tap this megastar to make stars bounce off the floor.";
			item.spriteName = Constants.Sprites.Game.BOUNCE_POWERUP;
			itemGlow.spriteName = Constants.Sprites.Game.BOUNCE_POWERUP_GLOW;
			item.MakePixelPerfect();
			itemGlow.MakePixelPerfect();
			NGUITools.SetActive(itemGlow.gameObject, true);
			NGUITools.SetActive(item.gameObject, true);
		}
		else if (DanceSessionManager.Instance.CurrentTip == Enums.GameTip.DOUBLE)
		{
			descLabel.text = "Tap this megastar to double gems collected and fill STARBURST faster.";
			item.spriteName = Constants.Sprites.Game.DOUBLESTAR_POWERUP;
			itemGlow.spriteName = Constants.Sprites.Game.X2_POWERUP_GLOW;
			item.MakePixelPerfect();
			itemGlow.MakePixelPerfect();
			NGUITools.SetActive(itemGlow.gameObject, true);
			NGUITools.SetActive(item.gameObject, true);
		}
		else if (DanceSessionManager.Instance.CurrentTip == Enums.GameTip.MULITPLER)
		{
			descLabel.text = "Tap this megastar to multiply points for each star.";
			item.spriteName = Constants.Sprites.Game.RANDOM_MULTIPLIER_POWERUP;
			itemGlow.spriteName = Constants.Sprites.Game.RANDOM_MULITPLIER_GLOW;
			item.MakePixelPerfect();
			itemGlow.MakePixelPerfect();
			NGUITools.SetActive(itemGlow.gameObject, true);
			NGUITools.SetActive(item.gameObject, true);
		}
		else if (DanceSessionManager.Instance.CurrentTip == Enums.GameTip.SEQUINS)
		{
			descLabel.text = "Collect gems to unlock STARBURST!";
			sequinsRow.SetActive(true);
		}
		else if (DanceSessionManager.Instance.CurrentTip == Enums.GameTip.SUPERPOSE)
		{
			descLabel.text = "When STARBURST is flashing, tap it to send all stars off the screen.";
			item.spriteName = Constants.Sprites.Game.SUPER_POSE_TIP_GLOW;
			item.MakePixelPerfect();
			NGUITools.SetActive(item.gameObject, true);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void GoBackToGame(GameObject pSender)
	{
		DanceSessionManager.Instance.DeActivateDanceColliders();
		GameScreenController.Instance.HideTipPanel();
		DanceSessionManager.Instance.ShowingTip = false;
	}
}
