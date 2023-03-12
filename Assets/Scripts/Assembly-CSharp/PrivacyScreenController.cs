using System;
using UnityEngine;

public class PrivacyScreenController : MonoBehaviour
{
	private static PrivacyScreenController _instance;

	public GameObject backButton;

	public GameObject link1;

	public GameObject link2;

	public UIDraggablePanel _uiDraggablePanel;

	private bool mAgePanelVisible;

	public GameObject agePanel;

	public GameObject ageExitButton;

	public GameObject button0;

	public GameObject button1;

	public GameObject button2;

	public GameObject button3;

	public GameObject button4;

	public GameObject button5;

	public GameObject button6;

	public GameObject button7;

	public GameObject button8;

	public GameObject button9;

	public GameObject backspaceButton;

	public GameObject submitButton;

	public UILabel day1;

	public UILabel day2;

	public UILabel month1;

	public UILabel month2;

	public UILabel year1;

	public UILabel year2;

	public UILabel year3;

	public UILabel year4;

	public UILabel enterDesc;

	public UILabel submitLbl;

	public UILabel continueLbl;

	public UILabel sorryMessaage;

	private UILabel mCurrentLabel;

	private bool mSorryPanelVisible;

	public GameObject sorryPanel;

	public GameObject sorryExitButton;

	public static PrivacyScreenController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType(typeof(PrivacyScreenController)) as PrivacyScreenController;
				if (_instance == null)
				{
					_instance = new GameObject().AddComponent<PrivacyScreenController>();
					_instance.hideFlags = HideFlags.HideAndDontSave;
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(backButton);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(GoToMainScreen));
		UIEventListener uIEventListener2 = UIEventListener.Get(link1);
		uIEventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener2.onClick, new UIEventListener.VoidDelegate(ShowAgePanel));
		UIEventListener uIEventListener3 = UIEventListener.Get(link2);
		uIEventListener3.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener3.onClick, new UIEventListener.VoidDelegate(ShowAgePanel));
		UIEventListener uIEventListener4 = UIEventListener.Get(button0);
		uIEventListener4.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener4.onClick, new UIEventListener.VoidDelegate(Enter0));
		UIEventListener uIEventListener5 = UIEventListener.Get(button1);
		uIEventListener5.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener5.onClick, new UIEventListener.VoidDelegate(Enter1));
		UIEventListener uIEventListener6 = UIEventListener.Get(button2);
		uIEventListener6.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener6.onClick, new UIEventListener.VoidDelegate(Enter2));
		UIEventListener uIEventListener7 = UIEventListener.Get(button3);
		uIEventListener7.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener7.onClick, new UIEventListener.VoidDelegate(Enter3));
		UIEventListener uIEventListener8 = UIEventListener.Get(button4);
		uIEventListener8.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener8.onClick, new UIEventListener.VoidDelegate(Enter4));
		UIEventListener uIEventListener9 = UIEventListener.Get(button5);
		uIEventListener9.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener9.onClick, new UIEventListener.VoidDelegate(Enter5));
		UIEventListener uIEventListener10 = UIEventListener.Get(button6);
		uIEventListener10.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener10.onClick, new UIEventListener.VoidDelegate(Enter6));
		UIEventListener uIEventListener11 = UIEventListener.Get(button7);
		uIEventListener11.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener11.onClick, new UIEventListener.VoidDelegate(Enter7));
		UIEventListener uIEventListener12 = UIEventListener.Get(button8);
		uIEventListener12.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener12.onClick, new UIEventListener.VoidDelegate(Enter8));
		UIEventListener uIEventListener13 = UIEventListener.Get(button9);
		uIEventListener13.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener13.onClick, new UIEventListener.VoidDelegate(Enter9));
		UIEventListener uIEventListener14 = UIEventListener.Get(backspaceButton);
		uIEventListener14.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener14.onClick, new UIEventListener.VoidDelegate(BackSpace));
		UIEventListener uIEventListener15 = UIEventListener.Get(ageExitButton);
		uIEventListener15.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener15.onClick, new UIEventListener.VoidDelegate(HideAgePanel));
		UIEventListener uIEventListener16 = UIEventListener.Get(submitButton);
		uIEventListener16.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener16.onClick, new UIEventListener.VoidDelegate(Submit));
		UIEventListener uIEventListener17 = UIEventListener.Get(sorryExitButton);
		uIEventListener17.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener17.onClick, new UIEventListener.VoidDelegate(HideSorryPanel));
	}

	private void OnEnable()
	{
		SharedTextureManager.ShowBackground(Constants.Sprites.Game.MAIN_BACKGROUND);
		SharedTextureManager.ShowMainIsabelle(false);
		SharedTextureManager.ShowSequinsBottomLeft(false);
		SharedTextureManager.ShowSequinsTopRight(false);
		if (_uiDraggablePanel != null)
		{
			_uiDraggablePanel.MoveRelative(new Vector3(0f, 0f - _uiDraggablePanel.transform.localPosition.y, 0f));
		}
		(submitButton.GetComponent("UIImageButton") as UIImageButton).isEnabled = false;
		agePanel.SetActive(false);
		sorryPanel.SetActive(false);

		enterDesc.text =    "AgeCheckDesc";
		submitLbl.text =    "AgeSubmitBtn";
		continueLbl.text =  "AgeContinueBtn";
		sorryMessaage.text = "AgeSorryDesc";
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void GoToMainScreen(GameObject pSender)
	{
		if (!mAgePanelVisible && !mSorryPanelVisible)
		{
			// KontagentManager.Instance.SendCustomEvent("Privacy_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Back_Selected");
			AudioManager.Sound.PlaySoundOneShot("button_default");
			LoadingScreenController.LoadMainScreen();
		}
	}

	public void OpenURL(GameObject pSender)
	{
		AudioManager.Sound.PlaySoundOneShot("button_default");
		Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
	}

	public void HideAgePanel(GameObject pSender)
	{
		day1.text = "D";
		day1.color = new Color(0.6196f, 0.6196f, 0.6196f);
		day2.text = "D";
		day2.color = new Color(0.6196f, 0.6196f, 0.6196f);
		month1.text = "M";
		month1.color = new Color(0.6196f, 0.6196f, 0.6196f);
		month2.text = "M";
		month2.color = new Color(0.6196f, 0.6196f, 0.6196f);
		year1.text = "Y";
		year1.color = new Color(0.6196f, 0.6196f, 0.6196f);
		year2.text = "Y";
		year2.color = new Color(0.6196f, 0.6196f, 0.6196f);
		year3.text = "Y";
		year3.color = new Color(0.6196f, 0.6196f, 0.6196f);
		year4.text = "Y";
		year4.color = new Color(0.6196f, 0.6196f, 0.6196f);
		agePanel.SetActive(false);
		mAgePanelVisible = false;
		AudioManager.Sound.PlaySoundOneShot("button_default");
	}

	public void ShowAgePanel(GameObject pSender)
	{
		if (!mAgePanelVisible && !mSorryPanelVisible)
		{
			agePanel.SetActive(true);
			mAgePanelVisible = true;
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void HideSorryPanel(GameObject pSender)
	{
		sorryPanel.SetActive(false);
		mSorryPanelVisible = false;
	}

	public void ShowSorryPanel(GameObject pSender)
	{
		sorryPanel.SetActive(true);
		mSorryPanelVisible = true;
	}

	public void Enter0(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "0";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter1(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "1";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter2(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "2";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter3(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "3";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter4(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "4";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter5(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "5";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter6(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "6";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter7(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "7";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter8(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "8";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	public void Enter9(GameObject pSender)
	{
		if (!(mCurrentLabel == null))
		{
			mCurrentLabel.text = "9";
			mCurrentLabel.color = new Color(0.49f, 0.227f, 0.592f);
			AudioManager.Sound.PlaySoundOneShot("button_default");
			NextLabel();
		}
	}

	private void NextLabel()
	{
		(submitButton.GetComponent("UIImageButton") as UIImageButton).isEnabled = false;
		if (day1.text.CompareTo("D") == 0)
		{
			mCurrentLabel = day1;
			return;
		}
		if (day2.text.CompareTo("D") == 0)
		{
			mCurrentLabel = day2;
			return;
		}
		if (month1.text.CompareTo("M") == 0)
		{
			mCurrentLabel = month1;
			return;
		}
		if (month2.text.CompareTo("M") == 0)
		{
			mCurrentLabel = month2;
			return;
		}
		if (year1.text.CompareTo("Y") == 0)
		{
			mCurrentLabel = year1;
			return;
		}
		if (year2.text.CompareTo("Y") == 0)
		{
			mCurrentLabel = year2;
			return;
		}
		if (year3.text.CompareTo("Y") == 0)
		{
			mCurrentLabel = year3;
			return;
		}
		if (year4.text.CompareTo("Y") == 0)
		{
			mCurrentLabel = year4;
			return;
		}
		mCurrentLabel = null;
		(submitButton.GetComponent("UIImageButton") as UIImageButton).isEnabled = true;
	}

	public void BackSpace(GameObject pSender)
	{
		AudioManager.Sound.PlaySoundOneShot("button_default");
		if (mCurrentLabel == null)
		{
			mCurrentLabel = year4;
			year4.text = "Y";
			year4.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
		else if (mCurrentLabel == year4)
		{
			mCurrentLabel = year3;
			year3.text = "Y";
			year3.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
		else if (mCurrentLabel == year3)
		{
			mCurrentLabel = year2;
			year2.text = "Y";
			year2.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
		else if (mCurrentLabel == year2)
		{
			mCurrentLabel = year1;
			year1.text = "Y";
			year1.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
		else if (mCurrentLabel == year1)
		{
			mCurrentLabel = month2;
			month2.text = "M";
			month2.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
		else if (mCurrentLabel == month2)
		{
			mCurrentLabel = month1;
			month1.text = "M";
			month1.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
		else if (mCurrentLabel == month1)
		{
			mCurrentLabel = day2;
			day2.text = "D";
			day2.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
		else if (mCurrentLabel == day2)
		{
			mCurrentLabel = day1;
			day1.text = "D";
			day1.color = new Color(0.6196f, 0.6196f, 0.6196f);
		}
	}

	public void Submit(GameObject pSender)
	{
		bool flag = false;
		string s = string.Empty + year1.text.ToString() + string.Empty + year2.text.ToString() + string.Empty + year3.text.ToString() + string.Empty + year4.text.ToString() + string.Empty;
		int result = DateTime.Now.Year;
		int.TryParse(s, out result);
		if (DateTime.Now.Year - result < 18)
		{
			flag = true;
		}
		int num = DateTime.Now.Year - result;
		if (num >= 18)
		{
			string s2 = string.Empty + month1.text.ToString() + string.Empty + month2.text.ToString() + string.Empty;
			int result2 = DateTime.Now.Month;
			int.TryParse(s2, out result2);
			MonoBehaviour.print("YEAR" + (DateTime.Now.Year - result));
			if ((num == 18 && result2 < DateTime.Now.Month) || result2 > 12)
			{
				flag = true;
			}
			else if (result2 >= DateTime.Now.Month)
			{
				string s3 = string.Empty + day1.text.ToString() + string.Empty + day2.text.ToString() + string.Empty;
				int result3 = DateTime.Now.Day;
				int.TryParse(s3, out result3);
				if ((num == 18 && result2 == DateTime.Now.Month && result3 < DateTime.Now.Day) || result3 > 31)
				{
					flag = true;
				}
			}
		}
		AudioManager.Sound.PlaySoundOneShot("button_default ");
		if (!flag)
		{
			HideAgePanel(null);
			Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
		}
		else
		{
			HideAgePanel(null);
			ShowSorryPanel(null);
		}
	}
}
