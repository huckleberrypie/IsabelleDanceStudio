using System;
using UnityEngine;

public class ResultScreemController : MonoBehaviour
{
	private static ResultScreemController _instance;

	public GameObject mainMenuButton;

	public GameObject playAgainButton;

	public GameObject powerUpButton;

	public UILabel bestScoreLabel;

	public UILabel scoreLabel;

	public UILabel timeLabel;

	public UILabel newBestText;

	public UISprite newBestGraphics;

	public string bestScoreStr;

	public string scoreStr;

	public UILabel mainLbl;

	public UILabel playAgainLbl;

	public UILabel congratsLbl;

	public static ResultScreemController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(ResultScreemController)) as ResultScreemController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<ResultScreemController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(mainMenuButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(GoToMainScreen));
		UIEventListener uIEventListener2 = UIEventListener.Get(playAgainButton);
		uIEventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener2.onClick, new UIEventListener.VoidDelegate(GoToGameScreen));
		UIEventListener uIEventListener3 = UIEventListener.Get(powerUpButton);
		uIEventListener3.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener3.onClick, new UIEventListener.VoidDelegate(GoToHelpScreen));
	}

	public void OnEnable()
	{
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.MAIN_BACKGROUND);
		SharedTextureManager.ShowSequinsBottomLeft(true);
		SharedTextureManager.ShowSequinsTopRight(true);

		bestScoreStr = "Best Score:";
		scoreStr = "Score:";
		bestScoreLabel.text = string.Empty + bestScoreStr + " " + Conversion.AddCommas(PlayerManager.Instance.BestScore);
		scoreLabel.text = string.Empty + scoreStr + " " + Conversion.AddCommas(Scoreboard.Instance.CollectedPoints);
		timeLabel.text = Conversion.TimeSecToMinNoMilisec(Scoreboard.Instance.CurrentGameTime);
		// KontagentManager.Instance.SendCustomEvent("Results_Screen", string.Empty, Conversion.AddCommas(Scoreboard.Instance.CollectedPoints), string.Empty, string.Empty, string.Empty, "Final_Score");
		// KontagentManager.Instance.SendCustomEvent("Results_Screen", string.Empty, Conversion.AddCommas(Scoreboard.Instance.CurrentGameTime), string.Empty, string.Empty, string.Empty, "Final_Time");
		if (GameManager.Instance.NewBestScore)
		{
			NGUITools.SetActive(newBestText.gameObject, true);
			NGUITools.SetActive(newBestGraphics.gameObject, true);
			// KontagentManager.Instance.SendCustomEvent("Results_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Best_Score_Improved");
		}
		else
		{
			NGUITools.SetActive(newBestText.gameObject, false);
			NGUITools.SetActive(newBestGraphics.gameObject, false);
		}
		mainLbl.text = "Main";
		playAgainLbl.text = "Play Again";
		congratsLbl.text = "Congratulations!";
		newBestText.text = "New\nBest";
	}

	public void GoToMainScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Results_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Main_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		LoadingScreenController.LoadMainScreen();
	}

	public void GoToGameScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Results_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Play_Again_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		LoadingScreenController.LoadGameScreen();
	}

	public void GoToHelpScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Results_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Help_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		HowToPlayScreenController.IS_GOTO_MAIN = false;
		LoadingScreenController.LoadHowToScreen();
	}
}
