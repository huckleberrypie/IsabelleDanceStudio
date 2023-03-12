using System.Collections;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
	private static LoadingScreenController _instance;

	public UILabel loadingLabel;

	public GameObject loadingTransitionObj;

	public UITexture loadingBackground;

	public UITexture splashScreen;

	public UITexture splashLogo;

	public UITexture splashLegal;

	private bool firstLoad;

	public static LoadingScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(LoadingScreenController)) as LoadingScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<LoadingScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	private void OnEnable()
	{
		loadingBackground.gameObject.SetActive(true);
		TweenAlpha.Begin(base.transform.gameObject, 0.1f, 0f);
		loadingBackground.color = new Color(loadingBackground.color.r, loadingBackground.color.g, loadingBackground.color.b, 0f);
		loadingLabel.text = "Loading...";
	}

	private void OnDisable()
	{
		TweenAlpha.Begin(base.transform.gameObject, 0f, 0f);
	}

	private void FadeInScreen()
	{
		TweenAlpha.Begin(base.transform.gameObject, Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME, 1f);
		TweenAlpha.Begin(loadingBackground.gameObject, Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME * 2f, 1f);
	}

	public void Awake()
	{
		splashScreen.gameObject.SetActive(false);
		splashLogo.gameObject.SetActive(false);
		splashLegal.gameObject.SetActive(false);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.FONTS);
		AtlasManager.UnloadFont(Enums.FontOrder.FLOATING_FONT);
		AtlasManager.UnloadFont(Enums.FontOrder.NEWBEST_FONT);
		AtlasManager.UnloadFont(Enums.FontOrder.BLUE_FLOATING_FONT);
		AtlasManager.UnloadFont(Enums.FontOrder.GOLD_FLOATING_FONT);
		AtlasManager.UnloadFont(Enums.FontOrder.VIOLET_FLOATING_FONT);
		AtlasManager.LoadAtlas(Enums.AtlasOrder.FONTS);
		AtlasManager.LoadFont(Enums.FontOrder.FLOATING_FONT);
		AtlasManager.LoadFont(Enums.FontOrder.NEWBEST_FONT);
		AtlasManager.LoadFont(Enums.FontOrder.BLUE_FLOATING_FONT);
		AtlasManager.LoadFont(Enums.FontOrder.GOLD_FLOATING_FONT);
		AtlasManager.LoadFont(Enums.FontOrder.VIOLET_FLOATING_FONT);
		firstLoad = true;
	}

	public static void LoadMainScreen()
	{
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.LOADING_BACKGROUND);
		if (GameManager.TransitionBackground.GetComponent("UISprite") != null)
		{
			(GameManager.TransitionBackground.GetComponent("UISprite") as UISprite).depth = 29;
		}
		if (Instance.firstLoad)
		{
			Instance.splashScreen.gameObject.SetActive(true);
			Instance.splashLogo.gameObject.SetActive(true);
			Instance.splashLegal.gameObject.SetActive(true);
			Instance.loadingLabel.gameObject.SetActive(false);
			UIWindow.Show(GameManager.LoadingScreen);
			Instance.firstLoad = false;
			Instance.StartCoroutine(Instance.ShowMainScreen(Constants.DesginerGlobals.SPLASH_SCREEN_TIME));
		}
		else
		{
			GameManager.Instance.StartBackgroundTransition();
			UIWindow.Show(GameManager.LoadingScreen);
			Instance.FadeInScreen();
			Instance.StartCoroutine(Instance.ShowMainScreen(Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME));
		}
	}

	private IEnumerator ShowMainScreen(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.MENUONLY_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.TITLE_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.SHARED_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.INGAME_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.INGAME_UI2);
		UIRoot.Broadcast("Refresh");
		yield return Resources.UnloadUnusedAssets();
		AtlasManager.LoadAtlas(Enums.AtlasOrder.MENUONLY_UI);
		AtlasManager.LoadAtlas(Enums.AtlasOrder.TITLE_UI);
		AtlasManager.LoadAtlas(Enums.AtlasOrder.SHARED_UI);
		AtlasManager.LoadAtlas(Enums.AtlasOrder.INGAME_UI);
		if (!AudioManager.Music.IsPlaying("music_loop_main"))
		{
			AudioManager.Music.PlayMusicLoop("music_loop_main");
		}
		UIWindow.Show(GameManager.MainScreen);
		GameManager.CurrentScreen = Enums.GameScreens.MAIN_MENU;
		GameManager.Instance.EndBackgroundTransition();
		NGUITools.SetActive(loadingLabel.gameObject, false);
		NGUITools.SetActive(splashScreen.gameObject, false);
		NGUITools.SetActive(splashLogo.gameObject, false);
		NGUITools.SetActive(splashLegal.gameObject, false);
	}

	public static void LoadGameScreen()
	{
		if (GameManager.TransitionBackground.GetComponent("UISprite") != null)
		{
			(GameManager.TransitionBackground.GetComponent("UISprite") as UISprite).depth = 29;
		}
		GameManager.Instance.StartBackgroundTransition();
		UIWindow.Show(GameManager.LoadingScreen);
		Instance.FadeInScreen();
		Instance.StartCoroutine(Instance.ShowGameScreen(Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME));
	}

	private IEnumerator ShowGameScreen(float delayTime)
	{
		NGUITools.SetActive(loadingLabel.gameObject, true);
		yield return new WaitForSeconds(delayTime);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.MENUONLY_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.TITLE_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.SHARED_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.INGAME_UI2);
		UIRoot.Broadcast("Refresh");
		yield return Resources.UnloadUnusedAssets();
		AtlasManager.LoadAtlas(Enums.AtlasOrder.MENUONLY_UI);
		AtlasManager.LoadAtlas(Enums.AtlasOrder.SHARED_UI);
		AtlasManager.LoadAtlas(Enums.AtlasOrder.INGAME_UI);
		AtlasManager.LoadAtlas(Enums.AtlasOrder.INGAME_UI2);
		AudioManager.Music.PlayMusicByOrder("music_sting_gameintro,music_loop_game", true);
		UIWindow.Show(GameManager.GameScreen);
		GameManager.CurrentScreen = Enums.GameScreens.GAME_SCREEN;
		GameManager.Instance.EndBackgroundTransition();
		NGUITools.SetActive(loadingLabel.gameObject, false);
	}

	public static void LoadInfoScreen()
	{
		if (GameManager.TransitionBackground.GetComponent("UISprite") != null)
		{
			(GameManager.TransitionBackground.GetComponent("UISprite") as UISprite).depth = 29;
		}
		GameManager.Instance.StartBackgroundTransition();
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.LOADING_BACKGROUND);
		UIWindow.Show(GameManager.LoadingScreen);
		Instance.FadeInScreen();
		Instance.StartCoroutine(Instance.ShowInfoScreen(Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME));
	}

	private IEnumerator ShowInfoScreen(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		UIWindow.Show(GameManager.InfoScreen);
		GameManager.CurrentScreen = Enums.GameScreens.HELP_SCREEN;
		GameManager.Instance.EndBackgroundTransition();
	}

	public static void LoadHowToScreen()
	{
		if (GameManager.TransitionBackground.GetComponent("UISprite") != null)
		{
			(GameManager.TransitionBackground.GetComponent("UISprite") as UISprite).depth = 29;
		}
		GameManager.Instance.StartBackgroundTransition();
		UIWindow.Show(GameManager.LoadingScreen);
		Instance.FadeInScreen();
		Instance.StartCoroutine(Instance.ShowHowToScreen(Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME));
	}

	private IEnumerator ShowHowToScreen(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		if (!AudioManager.Music.IsPlaying("music_loop_help"))
		{
			AudioManager.Music.PlayMusicLoop("music_loop_help");
		}
		UIWindow.Show(GameManager.HowToScreen);
		GameManager.CurrentScreen = Enums.GameScreens.HELP_SCREEN;
		GameManager.Instance.EndBackgroundTransition();
	}

	public static void LoadPrivacyScreen()
	{
		if (GameManager.TransitionBackground.GetComponent("UISprite") != null)
		{
			(GameManager.TransitionBackground.GetComponent("UISprite") as UISprite).depth = 29;
		}
		GameManager.Instance.StartBackgroundTransition();
		UIWindow.Show(GameManager.LoadingScreen);
		Instance.FadeInScreen();
		Instance.StartCoroutine(Instance.ShowPrivacyScreen(Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME));
	}

	private IEnumerator ShowPrivacyScreen(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		UIWindow.Show(GameManager.PrivacyScreen);
		GameManager.CurrentScreen = Enums.GameScreens.PRIVACY_SCREEN;
		GameManager.Instance.EndBackgroundTransition();
	}

	public static void LoadResultScreen()
	{
		GameManager.Instance.StartBackgroundTransition();
		if (GameManager.TransitionBackground.GetComponent("UISprite") != null)
		{
			(GameManager.TransitionBackground.GetComponent("UISprite") as UISprite).depth = 29;
		}
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.LOADING_BACKGROUND);
		SharedTextureManager.ShowMainIsabelle(false);
		SharedTextureManager.ShowSequinsBottomLeft(false);
		SharedTextureManager.ShowSequinsTopRight(false);
		UIWindow.Show(GameManager.LoadingScreen);
		Instance.FadeInScreen();
		Instance.StartCoroutine(Instance.ShowResultScreen(Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME));
	}

	private IEnumerator ShowResultScreen(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.TITLE_UI);
		AtlasManager.UnloadAtlas(Enums.AtlasOrder.INGAME_UI2);
		UIRoot.Broadcast("Refresh");
		yield return Resources.UnloadUnusedAssets();
		AtlasManager.LoadAtlas(Enums.AtlasOrder.MENUONLY_UI);
		UIWindow.Show(GameManager.ResultScreen);
		GameManager.CurrentScreen = Enums.GameScreens.RESULT_SCREEN;
		GameManager.Instance.EndBackgroundTransition();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
