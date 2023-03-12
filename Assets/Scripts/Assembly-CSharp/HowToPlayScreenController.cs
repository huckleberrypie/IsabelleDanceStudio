using System;
using UnityEngine;

public class HowToPlayScreenController : MonoBehaviour
{
	private static HowToPlayScreenController _instance;

	public static bool IS_GOTO_MAIN;

	public GameObject backButton;

	public UICenterOnChildCustom uiCenterOnChild;

	public PageIndicator pageIndicator;

	public UIPanel clippingPanel;

	public UILabel pg1Ln1;

	public UILabel pg1Ln2;

	public UILabel pg1Ln3;

	public UILabel pg2Ln1;

	public UILabel pg3Ln1;

	public UILabel pg3Ln2;

	public UILabel pg3Ln3;

	public UILabel pg3Ln4;

	public UILabel pg4Ln1;

	public static HowToPlayScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(HowToPlayScreenController)) as HowToPlayScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<HowToPlayScreenController>();
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
		uiCenterOnChild.onUpdateCenteredObject = OnUpdateCenteredObject;
	}

	public void OnEnable()
	{
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.MAIN_BACKGROUND);
		SharedTextureManager.ShowMainIsabelle(false);
		SharedTextureManager.ShowSequinsBottomLeft(false);
		SharedTextureManager.ShowSequinsTopRight(false);
		clippingPanel.clipRange = new Vector4(clippingPanel.clipRange.x, clippingPanel.clipRange.y, Screen.width, clippingPanel.clipRange.w);

		pg1Ln1.text = "HTP.Pg1.Ln1";
		pg1Ln2.text = "HTP.Pg1.Ln2";
		pg1Ln3.text = "HTP.Pg1.Ln3";
		pg2Ln1.text = "HTP.Pg2.Ln1";
		pg3Ln1.text = "HTP.Pg3.Ln1";
		pg3Ln2.text = "HTP.Pg3.Ln2";
		pg3Ln3.text = "HTP.Pg3.Ln3";
		pg3Ln4.text = "HTP.Pg3.Ln4";
		pg4Ln1.text = "HTP.Pg4.Ln1";
	}

	public void OnDisable()
	{
		pageIndicator.UpdatePage(0);
	}

	private void OnUpdateCenteredObject()
	{
		GameObject centeredObject = uiCenterOnChild.centeredObject;
		pageIndicator.UpdatePage(Convert.ToInt32(centeredObject.name));
	}

	private void Start()
	{
		pageIndicator.UpdatePage(0);
	}

	private void Update()
	{
	}

	public void GoToMainScreen(GameObject pSender)
	{
		// KontagentManager.Instance.SendCustomEvent("Help_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Back_Selected");
		AudioManager.Sound.PlaySoundOneShot("button_default");
		if (IS_GOTO_MAIN)
		{
			LoadingScreenController.LoadMainScreen();
		}
	}
}
