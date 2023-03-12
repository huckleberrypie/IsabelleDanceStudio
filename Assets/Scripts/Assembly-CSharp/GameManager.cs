using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	public UIPanel mainScreenPanel;

	public UIPanel loadingScreenPanel;

	public UIPanel modeScreenPanel;

	public UIPanel goalScreenPanel;

	public UIPanel gameScreenPanel;

	public UIPanel infoScreenPanel;

	public UIPanel howScreenPanel;

	public UIPanel privacyScreenPanel;

	public UIPanel resultScreenPanel;

	public GameObject superPoseBar;

	public GameObject transitionBackground;

	public GameObject transitionGameBackground;

	private Enums.GameScreens mCurrentScreen;

	public Transform fallingContainer;

	public GameObject playerObject;

	public GameObject playerShadow;

	private string[] playerPoseAnimationPrefixes = new string[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private int[] playerPoseAnimationFPS = new int[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private string[] playerPoseAttachmentName = new string[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private Vector3[] playerPoseAttachmentPos = new Vector3[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private UIWidget.Pivot[] playerPosePivot = new UIWidget.Pivot[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private float[] playerPoseAttachmentStartRotation = new float[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private float[] playerPoseAttachmentEndRotation = new float[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private Vector2[] playerPoseHitPointOffsets = new Vector2[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	private Vector2[] playerPoseForce = new Vector2[Constants.PlayerMoves.PLAYER_NUM_MOVES];

	public GameObject poseCollider;

	public GameObject eventCollider;

	private bool mInGame;

	private bool mIsGamePaused;

	private bool mNewBestScore;

	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<GameManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public static UIPanel MainScreen
	{
		get
		{
			return Instance.mainScreenPanel;
		}
	}

	public static UIPanel LoadingScreen
	{
		get
		{
			return Instance.loadingScreenPanel;
		}
	}

	public static UIPanel ModeScreen
	{
		get
		{
			return Instance.modeScreenPanel;
		}
	}

	public static UIPanel GoalScreen
	{
		get
		{
			return Instance.goalScreenPanel;
		}
	}

	public static UIPanel GameScreen
	{
		get
		{
			return Instance.gameScreenPanel;
		}
	}

	public static UIPanel InfoScreen
	{
		get
		{
			return Instance.infoScreenPanel;
		}
	}

	public static UIPanel HowToScreen
	{
		get
		{
			return Instance.howScreenPanel;
		}
	}

	public static UIPanel PrivacyScreen
	{
		get
		{
			return Instance.privacyScreenPanel;
		}
	}

	public static UIPanel ResultScreen
	{
		get
		{
			return Instance.resultScreenPanel;
		}
	}

	public static GameObject SuperPoseBar
	{
		get
		{
			return Instance.superPoseBar;
		}
	}

	public static GameObject TransitionBackground
	{
		get
		{
			return Instance.transitionBackground;
		}
	}

	public static GameObject TransitionGameBackground
	{
		get
		{
			return Instance.transitionGameBackground;
		}
	}

	public static Enums.GameScreens CurrentScreen
	{
		get
		{
			return Instance.mCurrentScreen;
		}
		set
		{
			Instance.mCurrentScreen = value;
		}
	}

	public static Transform FallingContainer
	{
		get
		{
			return Instance.fallingContainer;
		}
	}

	public static GameObject PlayerObject
	{
		get
		{
			return Instance.playerObject;
		}
	}

	public static GameObject PlayerShadow
	{
		get
		{
			return Instance.playerShadow;
		}
	}

	public static GameObject PoseCollider
	{
		get
		{
			return Instance.poseCollider;
		}
	}

	public static GameObject EventCollider
	{
		get
		{
			return Instance.eventCollider;
		}
	}

	public bool NewBestScore
	{
		get
		{
			return mNewBestScore;
		}
		set
		{
			mNewBestScore = value;
		}
	}

	public static string GetPlayerPoseAnimationPrefix(int index)
	{
		return Instance.playerPoseAnimationPrefixes[index];
	}

	public static int GetPlayerPoseAnimationFPS(int index)
	{
		return Instance.playerPoseAnimationFPS[index];
	}

	public static string GetPlayerPoseAttachmentName(int index)
	{
		return Instance.playerPoseAttachmentName[index];
	}

	public static Vector3 GetPlayerPoseAttachmentPos(int index)
	{
		return Instance.playerPoseAttachmentPos[index];
	}

	public static UIWidget.Pivot GetPlayerPosePivot(int index)
	{
		return Instance.playerPosePivot[index];
	}

	public static float GetPlayerPoseAttachmentStartRotation(int index)
	{
		return Instance.playerPoseAttachmentStartRotation[index];
	}

	public static float GetPlayerPoseAttachmentEndRotation(int index)
	{
		return Instance.playerPoseAttachmentEndRotation[index];
	}

	public static Vector2 GetPlayerPoseHitPointOffset(int index)
	{
		return Instance.playerPoseHitPointOffsets[index];
	}

	public static Vector2 GetPlayerPoseForce(int index)
	{
		return Instance.playerPoseForce[index];
	}

	public void AddPlayerPoseAnimation(int index, string prefix, int fps, string attachmentName, Vector3 attachmentPos, UIWidget.Pivot pivotType, float startRot, float endRot, Vector2 hitPointOffset, Vector2 poseForce)
	{
		if (index < Constants.PlayerMoves.PLAYER_NUM_MOVES)
		{
			playerPoseAnimationPrefixes[index] = prefix;
			playerPoseAnimationFPS[index] = fps;
			playerPoseAttachmentName[index] = attachmentName;
			playerPoseAttachmentPos[index] = attachmentPos;
			playerPosePivot[index] = pivotType;
			playerPoseAttachmentStartRotation[index] = startRot;
			playerPoseAttachmentEndRotation[index] = endRot;
			playerPoseHitPointOffsets[index] = hitPointOffset;
			playerPoseForce[index] = poseForce;
		}
	}

	public bool IsGamePaused()
	{
		return mIsGamePaused;
	}

	public bool HasGameStarted()
	{
		return mInGame;
	}

	private void Awake()
	{
	}

	private void Start()
	{
		GameData.Instance.LoadDesignerData();
		LoadingScreenController.LoadMainScreen();
	}

	private IEnumerator WaitForLoading(float second)
	{
		while (!UITextureManager.Instance.IsLoaded)
		{
			yield return null;
		}
		yield return new WaitForSeconds(second);
	}

	private void SetupScreen()
	{
		if (SystemInfo.deviceModel == "Amazon KFAPWA" || SystemInfo.deviceModel == "Amazon KFAPWI" || SystemInfo.deviceModel == "Amazon KFTHWA" || SystemInfo.deviceModel == "Amazon KFTHWI" || SystemInfo.deviceModel == "Amazon KFSOWI")
		{
			if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
			{
				Screen.orientation = ScreenOrientation.LandscapeRight;
			}
			else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
			{
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}
		}
		else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}
		else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
		{
			Screen.orientation = ScreenOrientation.LandscapeRight;
		}
	}

	private void Update()
	{
		if (PlayerShadow != null && PlayerObject != null)
		{
			PlayerShadow.transform.localPosition = new Vector3(PlayerObject.transform.localPosition.x, PlayerShadow.transform.localPosition.y, PlayerShadow.transform.localPosition.z);
		}
		else
		{
			MonoBehaviour.print("player shadow is null");
		}
		if (!mInGame)
		{
		}
	}

	public void EnterGameMode()
	{
		if (!mInGame)
		{
			mInGame = true;
			playerObject.transform.position = Vector3.zero;
			Scoreboard.Instance.ResetScoreboard();
			GameScreenController.Instance.FadeTapToBegin();
			ResetBackgroundTransition();
		}
	}

	public void EndGameMode()
	{
		AudioManager.Music.PlayMusicLoop("music_loop_main");
		ResetBackgroundTransition();
		Instance.mInGame = false;
		Instance.ClearContainers();
		PlayerManager.Instance.isBestScore(Scoreboard.Instance.CollectedPoints);
		LoadingScreenController.LoadResultScreen();
		DanceSessionManager.CanTouch = false;
		mInGame = false;
	}

	public void QuitGameMode()
	{
		Instance.ResumeGameMode();
		Instance.mInGame = false;
		Instance.ClearContainers();
		LoadingScreenController.LoadMainScreen();
		mInGame = false;
		DanceSessionManager.CanTouch = false;
		ResetBackgroundTransition();
	}

	public void RestartGameMode()
	{
		Instance.ResumeGameMode();
		Instance.mInGame = false;
		Instance.ClearContainers();
		LoadingScreenController.LoadGameScreen();
		mInGame = false;
		DanceSessionManager.CanTouch = false;
		ResetBackgroundTransition();
	}

	public void TutorialGameMode()
	{
		Time.timeScale = 0f;
		mIsGamePaused = true;
		DanceSessionManager.CanTouch = false;
	}

	public void PauseGameMode()
	{
		Time.timeScale = 0f;
		mIsGamePaused = true;
		SuperPoseBar.SetActive(false);
		PlayerObject.SetActive(false);
		FallingContainer.gameObject.SetActive(false);
		DanceSessionManager.CanTouch = false;
	}

	public void ResumeGameMode()
	{
		Time.timeScale = 1f;
		mIsGamePaused = false;
		SuperPoseBar.SetActive(true);
		PlayerObject.SetActive(true);
		FallingContainer.gameObject.SetActive(true);
		DanceSessionManager.CanTouch = true;
	}

	public void StartBackgroundTransition(bool loadingScreen = true)
	{
		if (loadingScreen)
		{
			TweenAlpha.Begin(TransitionBackground, 0.1f, 0f);
			TransitionBackground.SetActive(true);
			TweenAlpha.Begin(TransitionBackground, Constants.DesginerGlobals.LOADING_SCREEN_FADEIN_TIME, 1f);
		}
		else
		{
			TweenAlpha.Begin(TransitionGameBackground, 0.1f, 0f);
			TransitionGameBackground.SetActive(true);
			TweenAlpha.Begin(TransitionGameBackground, Constants.DesginerGlobals.IN_GAME_FADEIN_TIME, 0.9f);
		}
	}

	public void EndBackgroundTransition()
	{
		TweenAlpha.Begin(TransitionBackground, Constants.DesginerGlobals.LOADING_SCREEN_FADEOUT_TIME, 0f);
		TweenAlpha.Begin(TransitionGameBackground, Constants.DesginerGlobals.IN_GAME_FADEOUT_TIME, 0f);
	}

	public void ResetBackgroundTransition()
	{
		TransitionBackground.SetActive(false);
		TransitionGameBackground.SetActive(false);
	}

	private void ClearContainers()
	{
		if (!(FallingContainer != null) || FallingContainer.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in FallingContainer)
		{
			if (item != null)
			{
				Object.Destroy(item.gameObject);
			}
		}
	}

	private void OnDestroy()
	{
		ClearContainers();
	}
}
