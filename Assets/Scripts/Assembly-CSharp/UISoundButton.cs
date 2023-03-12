using UnityEngine;

public class UISoundButton : MonoBehaviour
{
	public enum SoundButtonType
	{
		ON = 0,
		OFF = 1
	}

	public GameObject soundOn;

	public GameObject soundOff;

	private UIImageButton _uiSoundOn;

	private UIImageButton _uiSoundOff;

	private void Awake()
	{
		_uiSoundOn = soundOn.GetComponent<UIImageButton>();
		_uiSoundOff = soundOff.GetComponent<UIImageButton>();
		if (PlayerPrefs.HasKey("audio_key"))
		{
			bool isSoundOFF = ((PlayerPrefs.GetString("audio_key") == "true") ? true : false);
			AudioManager.isSoundOFF = isSoundOFF;
			return;
		}
		soundOn.SetActive(true);
		soundOff.SetActive(false);
		_uiSoundOn.enabled = true;
		_uiSoundOff.enabled = false;
	}

	private void Start()
	{
		if (AudioManager.isSoundOFF)
		{
			soundOn.SetActive(false);
			soundOff.SetActive(true);
			_uiSoundOn.enabled = false;
			_uiSoundOff.enabled = true;
		}
		else
		{
			soundOn.SetActive(true);
			soundOff.SetActive(false);
			_uiSoundOn.enabled = true;
			_uiSoundOff.enabled = false;
		}
		if (AudioManager.isSoundOFF)
		{
			AudioManager.Sound.Pause();
		}
		else
		{
			AudioManager.Sound.Play();
		}
		if (AudioManager.isSoundOFF)
		{
			AudioManager.Music.Pause();
		}
		else
		{
			AudioManager.Music.Play();
		}
	}

	private void UpdateSound()
	{
		MonoBehaviour.print("switching Sound from " + AudioManager.isSoundOFF + " to " + !AudioManager.isSoundOFF);
		AudioManager.isSoundOFF = !AudioManager.isSoundOFF;
		if (AudioManager.isSoundOFF)
		{
			AudioManager.Sound.Pause();
		}
		else
		{
			AudioManager.Sound.Play();
			AudioManager.Sound.PlaySoundOneShot("button_default");
		}
		if (AudioManager.isSoundOFF)
		{
			AudioManager.Music.Pause();
		}
		else
		{
			AudioManager.Music.Play();
		}
		PlayerPrefs.SetString("audio_key", AudioManager.isSoundOFF.ToString().ToLower());
		PlayerPrefs.Save();
		if (AudioManager.isSoundOFF)
		{
			if (GameManager.CurrentScreen == Enums.GameScreens.MAIN_MENU)
			{
				// KontagentManager.Instance.SendCustomEvent("Main_Menu", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Audio_Off_Selected");
			}
			else if (GameManager.CurrentScreen == Enums.GameScreens.GAME_SCREEN)
			{
				// KontagentManager.Instance.SendCustomEvent("Pause_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Audio_Off_Selected");
			}
		}
		else if (GameManager.CurrentScreen == Enums.GameScreens.MAIN_MENU)
		{
			
			// KontagentManager.Instance.SendCustomEvent("Main_Menu", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Audio_On_Selected");
		}
		else if (GameManager.CurrentScreen == Enums.GameScreens.GAME_SCREEN)
		{
			// KontagentManager.Instance.SendCustomEvent("Pause_Screen", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Audio_On_Selected");
		}
	}

	public void UpdateButton(SoundButtonType soundButtonType)
	{
		switch (soundButtonType)
		{
		case SoundButtonType.ON:
			soundOn.SetActive(false);
			soundOff.SetActive(true);
			_uiSoundOn.enabled = false;
			_uiSoundOff.enabled = true;
			break;
		case SoundButtonType.OFF:
			soundOn.SetActive(true);
			soundOff.SetActive(false);
			_uiSoundOn.enabled = true;
			_uiSoundOff.enabled = false;
			break;
		}
		UpdateSound();
	}
}
