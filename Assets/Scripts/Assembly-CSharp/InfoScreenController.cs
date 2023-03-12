using System;
using UnityEngine;

public class InfoScreenController : MonoBehaviour
{
	private static InfoScreenController _instance;

	public GameObject backButton;

	public static InfoScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(InfoScreenController)) as InfoScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<InfoScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(backButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(GoToMainScreen));
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

	public void GoToMainScreen(GameObject pSender)
	{
		LoadingScreenController.LoadMainScreen();
	}
}
