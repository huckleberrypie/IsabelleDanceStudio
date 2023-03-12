using System;
using UnityEngine;

public class GoalScreenController : MonoBehaviour
{
	private static GoalScreenController _instance;

	public GameObject playButton;

	public static GoalScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(GoalScreenController)) as GoalScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<GoalScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(playButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(GoToGameScreen));
	}

	public void OnEnable()
	{
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.MAIN_BACKGROUND);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void GoToGameScreen(GameObject pSender)
	{
		LoadingScreenController.LoadGameScreen();
	}
}
