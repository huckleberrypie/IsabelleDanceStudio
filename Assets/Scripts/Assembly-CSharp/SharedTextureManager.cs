using System.Collections.Generic;
using UnityEngine;

public class SharedTextureManager : MonoBehaviour
{
	private static SharedTextureManager _instance;

	public UITexture backgroundTexture;

	public GameObject sequinsTopRight;

	public GameObject sequinsBottomLeft;

	public GameObject mainIsabelle;

	public List<UITexture> backgroundList = new List<UITexture>();

	private static Texture2D _mainBG;

	private static Texture2D _gameBG;

	public static SharedTextureManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(SharedTextureManager)) as SharedTextureManager;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<SharedTextureManager>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public static UITexture BackgroundTexture
	{
		get
		{
			return Instance.backgroundTexture;
		}
	}

	public static GameObject SequinsTopRight
	{
		get
		{
			return Instance.sequinsTopRight;
		}
	}

	public static GameObject SequinsBottomLeft
	{
		get
		{
			return Instance.sequinsBottomLeft;
		}
	}

	public static GameObject MainIsabelle
	{
		get
		{
			return Instance.mainIsabelle;
		}
	}

	private void Start()
	{
		for (int i = 0; i < backgroundList.Count; i++)
		{
			backgroundList[i].gameObject.SetActive(false);
		}
	}

	public static void ShowSequinsTopRight(bool show)
	{
		Instance.sequinsTopRight.SetActive(show);
	}

	public static void ShowSequinsBottomLeft(bool show)
	{
		Instance.sequinsBottomLeft.SetActive(show);
	}

	public static void ShowMainIsabelle(bool show)
	{
		Instance.mainIsabelle.SetActive(show);
	}

	public static void SwitchGameBackground(int index)
	{
		BackgroundTexture.gameObject.SetActive(false);
		if (index >= Instance.backgroundList.Count)
		{
			return;
		}
		for (int i = 0; i < Instance.backgroundList.Count; i++)
		{
			if (index != i)
			{
				Instance.backgroundList[i].gameObject.SetActive(false);
				Instance.backgroundList[i].depth = -5;
			}
			else
			{
				Instance.backgroundList[i].gameObject.SetActive(true);
				Instance.backgroundList[i].depth = -4;
			}
		}
	}

	public static void SwitchToBackgroundTexture()
	{
		BackgroundTexture.gameObject.SetActive(true);
		BackgroundTexture.depth = -4;
		for (int i = 0; i < Instance.backgroundList.Count; i++)
		{
			Instance.backgroundList[i].gameObject.SetActive(false);
			Instance.backgroundList[i].depth = -5;
		}
	}

	public static void ShowBackground(string p_background)
	{
		SwitchToBackgroundTexture();
		if (_mainBG == null && p_background == Constants.Sprites.Game.LOADING_BACKGROUND)
		{
			if (Instance.backgroundTexture.mainTexture != null)
			{
				_mainBG = Instance.backgroundTexture.mainTexture as Texture2D;
			}
			else if (AtlasManagerInfo.IsSHD)
			{
				_mainBG = Resources.Load("SHD/MenuPanel_UI/" + p_background) as Texture2D;
			}
			else if (AtlasManagerInfo.IsHD)
			{
				_mainBG = Resources.Load("HD/MenuPanel_UI/" + p_background) as Texture2D;
			}
			else
			{
				_mainBG = Resources.Load("SD/MenuPanel_UI/" + p_background) as Texture2D;
			}
		}
		if (p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND1 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND2 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND3 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND4 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND5)
		{
			if (AtlasManagerInfo.IsSHD)
			{
				_gameBG = Resources.Load("SHD/MenuPanel_UI/" + p_background) as Texture2D;
			}
			else if (AtlasManagerInfo.IsHD)
			{
				_gameBG = Resources.Load("HD/MenuPanel_UI/" + p_background) as Texture2D;
			}
			else
			{
				_gameBG = Resources.Load("SD/MenuPanel_UI/" + p_background) as Texture2D;
			}
		}
		if (p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND1 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND2 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND3 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND4 || p_background == Constants.Sprites.Game.IN_GAME_BACKGROUND5)
		{
			Instance.backgroundTexture.mainTexture = _gameBG;
		}
		else
		{
			Instance.backgroundTexture.mainTexture = _mainBG;
		}
		NGUITools.SetActive(Instance.backgroundTexture.gameObject, true);
	}

	public static void HideBackground()
	{
		NGUITools.SetActive(Instance.backgroundTexture.gameObject, false);
	}
}
