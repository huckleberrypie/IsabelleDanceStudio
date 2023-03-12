using UnityEngine;

[RequireComponent(typeof(UITexture))]
public class UITextureSwitcher : MonoBehaviour
{
	public string SDTextureName = string.Empty;

	public string HDTextureName = string.Empty;

	public string SHDTextureName = string.Empty;

	private UITexture _texture;

	public void Awake()
	{
		_texture = GetComponent<UITexture>();
	}

	public void OnEnable()
	{
		if (AtlasManagerInfo.IsEnabled)
		{
			if (AtlasManagerInfo.IsSHD)
			{
				_texture.mainTexture = Resources.Load("SHD/MenuPanel_UI/" + SHDTextureName) as Texture2D;
			}
			else if (AtlasManagerInfo.IsHD)
			{
				_texture.mainTexture = Resources.Load("HD/MenuPanel_UI/" + HDTextureName) as Texture2D;
			}
			else
			{
				_texture.mainTexture = Resources.Load("SD/MenuPanel_UI/" + SDTextureName) as Texture2D;
			}
		}
	}
}
