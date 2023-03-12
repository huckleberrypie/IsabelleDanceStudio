using System;
using UnityEngine;

public class MainScreenController : MonoBehaviour
{
	private static MainScreenController _instance;

	public GameObject startButton;

	public GameObject howToPlayButton;

	public GameObject privacyButton;

	public GameObject soundButton;

	public UILabel versionLabel;

	public UILabel gameDescription;

	public UILabel startBtnText;

	public UISprite titleSprite;

	public UISprite privacySprite;

	public static MainScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(MainScreenController)) as MainScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<MainScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(howToPlayButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(GoToHowToPlayScreen));
		UIEventListener uIEventListener2 = UIEventListener.Get(startButton);
		uIEventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener2.onClick, new UIEventListener.VoidDelegate(GoToGameScreen));
		UIEventListener uIEventListener3 = UIEventListener.Get(privacyButton);
		uIEventListener3.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener3.onClick, new UIEventListener.VoidDelegate(GoToPrivacyScreen));
	}

	public void OnEnable()
	{
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.MAIN_BACKGROUND);
		SharedTextureManager.ShowMainIsabelle(true);
		SharedTextureManager.ShowSequinsBottomLeft(true);
		SharedTextureManager.ShowSequinsTopRight(true);

		gameDescription.text =      "Leap into action with Isabelle as she practices for her next ballet recital.";
		startBtnText.text =               "Start";
		titleSprite.spriteName =          "game_name"; // The "Dance Studio" atlas sprite
		privacySprite.spriteName =      "PrivacySprite";
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void GoToHowToPlayScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Main_Menu", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Help_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		HowToPlayScreenController.IS_GOTO_MAIN = true;
		LoadingScreenController.LoadHowToScreen();
	}

	public void GoToModeScreen(GameObject pSender)
	{
		AudioManager.Sound.PlaySoundOneShot("button_default");
	}

	public void GoToPrivacyScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Main_Menu", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Privacy_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		LoadingScreenController.LoadPrivacyScreen();
	}

	public void GoToGameScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Main_Menu", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Start_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		LoadingScreenController.LoadGameScreen();
	}
}
