using System;
using UnityEngine;

public class ModeScreenController : MonoBehaviour
{
	private static ModeScreenController _instance;

	public GameObject danceButton;

	public GameObject trainingButton;

	public static ModeScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(ModeScreenController)) as ModeScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<ModeScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	public void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(danceButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(GoToGoalsScreen));
		UIEventListener uIEventListener2 = UIEventListener.Get(trainingButton);
		uIEventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener2.onClick, new UIEventListener.VoidDelegate(GoToTrainingScreen));
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

	public void GoToGoalsScreen(GameObject pSender)
	{
	}

	public void GoToTrainingScreen(GameObject pSender)
	{
	}
}
