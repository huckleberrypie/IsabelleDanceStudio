using System;
using UnityEngine;

public class GameScreenController : MonoBehaviour
{
	private static GameScreenController _instance;

	public GameObject pauseButton;

	public UIPanel pauseMenu;

	public GameObject resumeButton;

	public GameObject newGameButton;

	public GameObject mainMenuButton;

	public UIPanel areYouSureMenu;

	public GameObject yesButton;

	public GameObject noButton;

	public UILabel scoreLabel;

	public GameObject superPoseBG;

	public GameObject superPoseBGScaleAnimation;

	public GameObject flowerBounceUI;

	public UILabel tapToBeginUI;

	public GameObject BottomLeft;

	public GameObject TopRight;

	public GameObject gameOverPanel;

	public GameObject tipPanel;

	private string scoreTxt;

	public UILabel confirmMessage;

	public UILabel confirmYes;

	public UILabel confirmNo;

	public UILabel pausedTitle;

	public UILabel pausedResume;

	public UILabel pausedNew;

	public UILabel pausedMain;

	private bool mIsQuiting;

	private bool mIsRestarting;

	private bool mScaleSuperPose;

	public static GameScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(GameScreenController)) as GameScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<GameScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		//TODO: Add basic gamepad support at least for the menus
		//Also add INI reader to parse localisation files from
		//rather than hardcode the language stuff in them assemblies
		UIEventListener uIEventListener = UIEventListener.Get(pauseButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(ShowPauseScreen));
		UIEventListener uIEventListener2 = UIEventListener.Get(resumeButton);
		uIEventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener2.onClick, new UIEventListener.VoidDelegate(RemovePauseScreen));
		UIEventListener uIEventListener3 = UIEventListener.Get(newGameButton);
		uIEventListener3.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener3.onClick, new UIEventListener.VoidDelegate(StartNewGame));
		UIEventListener uIEventListener4 = UIEventListener.Get(mainMenuButton);
		uIEventListener4.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener4.onClick, new UIEventListener.VoidDelegate(GoToMainScreen));
		UIEventListener uIEventListener5 = UIEventListener.Get(yesButton);
		uIEventListener5.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener5.onClick, new UIEventListener.VoidDelegate(ConfirmAction));
		UIEventListener uIEventListener6 = UIEventListener.Get(noButton);
		uIEventListener6.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener6.onClick, new UIEventListener.VoidDelegate(CancelAction));
	}

	private void OnEnable()
	{
		SharedTextureManager.ShowMainIsabelle(false);
		SharedTextureManager.ShowSequinsBottomLeft(false);
		SharedTextureManager.ShowSequinsTopRight(false);
		pauseMenu.gameObject.SetActive(false);
		pauseButton.SetActive(true);
		areYouSureMenu.gameObject.SetActive(false);
		superPoseBG.SetActive(false);
		superPoseBGScaleAnimation.SetActive(false);
		ShowTapToBegin();
		Scoreboard.Instance.ResetScoreboard();
		TouchManager.Instance.StartRecording();
		DanceSessionManager.Instance.StartDanceSession();
		DanceSessionManager.CanTouch = true;
		TweenAlpha.Begin(flowerBounceUI, 1f, 0f);

		confirmMessage.text ="Are you sure you'd like to exit the game now?";
		confirmYes.text ="Yes";
		confirmNo.text ="No";
		pausedTitle.text ="";
		pausedResume.text ="Resume";
		pausedNew.text ="New";
		pausedMain.text ="Main";
		scoreTxt ="Score:";
		tapToBeginUI.text ="Tap the star.";
		HideGameOverPanel();
	}

	private void OnDisable()
	{
		TouchManager.Instance.EndRecording();
		HideFlowerBounce();
		StopSuperPoseBGAnimation();
		HideTapToBegin();
		HideGameOverPanel();
	}

	private void Start()
	{
	}

	private void Update()
	{
		scoreLabel.text = string.Empty + scoreTxt + " " + Conversion.AddCommas(Scoreboard.Instance.CollectedPoints);
		if ((!Input.GetMouseButtonDown(0) && Input.touchCount <= 0) || GameManager.Instance.IsGamePaused() || !GameManager.Instance.HasGameStarted())
		{
		}
		if (mScaleSuperPose)
		{
			float num = (Constants.DesginerGlobals.MAX_SUPERPOSE_ANIM_SCALE - 1f) / (float)(5 / Constants.PlayerMoves.SUPER_POSE_FPS) * Time.deltaTime;
			float num2 = superPoseBGScaleAnimation.transform.localScale.x + num;
			superPoseBGScaleAnimation.transform.localScale = new Vector3(num2, num2, num2);
		}
	}

	public void LeaveAppShowPause()
	{
		if (!GameManager.Instance.IsGamePaused())
		{
			SharedTextureManager.ShowMainIsabelle(false);
			SharedTextureManager.ShowSequinsBottomLeft(true);
			SharedTextureManager.ShowSequinsTopRight(true);
			GameManager.Instance.PauseGameMode();
			pauseButton.SetActive(false);
			pauseMenu.gameObject.SetActive(true);
		}
	}

	public void ShowPauseScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Pause_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Pause_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		if (!GameManager.Instance.IsGamePaused())
		{
			SharedTextureManager.ShowMainIsabelle(false);
			SharedTextureManager.ShowSequinsBottomLeft(true);
			SharedTextureManager.ShowSequinsTopRight(true);
			GameManager.Instance.PauseGameMode();
			pauseButton.SetActive(false);
			pauseMenu.gameObject.SetActive(true);
		}
	}

	public void RemovePauseScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Pause_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Resume_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		if (GameManager.Instance.IsGamePaused())
		{
			SharedTextureManager.ShowMainIsabelle(false);
			SharedTextureManager.ShowSequinsBottomLeft(false);
			SharedTextureManager.ShowSequinsTopRight(false);
			GameManager.Instance.ResumeGameMode();
			pauseButton.SetActive(true);
			pauseMenu.gameObject.SetActive(false);
		}
	}

	public void GoToMainScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Pause_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Main_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		mIsQuiting = true;
		areYouSureMenu.gameObject.SetActive(true);
		pauseMenu.gameObject.SetActive(false);
	}

	public void StartNewGame(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Pause_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "New_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		mIsRestarting = true;
		areYouSureMenu.gameObject.SetActive(true);
		pauseMenu.gameObject.SetActive(false);
	}

	public void ConfirmAction(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Are_You_Sure_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Yes_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		if (mIsQuiting)
		{
			GameManager.Instance.QuitGameMode();
		}
		if (mIsRestarting)
		{
			GameManager.Instance.RestartGameMode();
		}
		mIsQuiting = false;
		mIsRestarting = false;
	}

	public void CancelAction(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Are_You_Sure_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "No_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		mIsQuiting = false;
		mIsRestarting = false;
		pauseMenu.gameObject.SetActive(true);
		areYouSureMenu.gameObject.SetActive(false);
	}

	public void StartSuperPoseBGAnimation()
	{
		superPoseBG.SetActive(true);
		superPoseBGScaleAnimation.SetActive(true);
		mScaleSuperPose = true;
	}

	public void StopSuperPoseBGAnimation()
	{
		superPoseBGScaleAnimation.transform.localScale = new Vector3(1f, 1f, 0f);
		superPoseBG.SetActive(false);
		superPoseBGScaleAnimation.SetActive(false);
		mScaleSuperPose = false;
	}

	public void ShowFlowerBounce()
	{
		flowerBounceUI.SetActive(true);
		if (flowerBounceUI.GetComponent("FadeInFadeOut") == null)
		{
			(flowerBounceUI.AddComponent<FadeInFadeOut>() as FadeInFadeOut).SetTimeSpan(GameData.FlowerBounceLifeTime);
			(flowerBounceUI.GetComponent("FadeInFadeOut") as FadeInFadeOut).AllowStart = true;
		}
	}

	public void HideFlowerBounce()
	{
		if (flowerBounceUI.GetComponent("FadeInFadeOut") != null)
		{
			UnityEngine.Object.Destroy(flowerBounceUI.GetComponent("FadeInFadeOut"));
		}
		TweenAlpha.Begin(flowerBounceUI, 1f, 0f);
		flowerBounceUI.SetActive(false);
	}

	public void ShowTapToBegin()
	{
		NGUITools.SetActive(tapToBeginUI.gameObject, true);
		TweenAlpha.Begin(tapToBeginUI.gameObject, 0.1f, 0.6f);
	}

	public void FadeTapToBegin()
	{
		TweenAlpha.Begin(tapToBeginUI.gameObject, 1f, 0f);
	}

	public void HideTapToBegin()
	{
		NGUITools.SetActive(tapToBeginUI.gameObject, false);
	}

	public void ShowGameOverPanel()
	{
		gameOverPanel.SetActive(true);
	}

	public void HideGameOverPanel()
	{
		gameOverPanel.SetActive(false);
	}

	public void ShowTipPanel(Enums.GameTip tip)
	{
		tipPanel.SetActive(true);
		GameManager.Instance.TutorialGameMode();
	}

	public void HideTipPanel()
	{
		tipPanel.SetActive(false);
		DanceSessionManager.Instance.CurrentTip = Enums.GameTip.NONE;
		GameManager.Instance.ResumeGameMode();
	}
}
