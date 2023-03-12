using UnityEngine;

public class UISoundButtonReceiver : MonoBehaviour
{
	public UISoundButton.SoundButtonType buttonType;

	private UISoundButton _uiSoundButton;

	private void OnClick()
	{
		if (_uiSoundButton != null)
		{
			_uiSoundButton.UpdateButton(buttonType);
		}
	}

	private void Awake()
	{
		_uiSoundButton = base.transform.parent.GetComponent<UISoundButton>();
	}
}
