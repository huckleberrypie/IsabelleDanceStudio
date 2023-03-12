using UnityEngine;

public class UITextureManager
{
	private UITexture[] _uiTextures;

	private static UITextureManager _instance;

	private bool _isLoaded;

	public static UITextureManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new UITextureManager();
			}
			return _instance;
		}
	}

	public bool IsLoaded
	{
		get
		{
			return _isLoaded;
		}
	}

	public UITextureManager()
	{
		Debug.Log("calling UITextureManager()");
		_uiTextures = Object.FindObjectsOfType(typeof(UITexture)) as UITexture[];
	}

	public void LoadTextures()
	{
		if (_uiTextures == null)
		{
			return;
		}
		if (AtlasManagerInfo.IsSHD)
		{
			Debug.Log("SHD texture length " + _uiTextures.Length);
			for (int i = 0; i < _uiTextures.Length; i++)
			{
				string name = _uiTextures[i].mainTexture.name;
				Debug.Log("index is " + i + " texture name " + name);
				Texture2D mainTexture = Resources.Load("SHD/MenuPanel_UI/" + name) as Texture2D;
				_uiTextures[i].mainTexture = mainTexture;
			}
		}
		else if (AtlasManagerInfo.IsHD)
		{
			Debug.Log("HD texture length " + _uiTextures.Length);
			for (int j = 0; j < _uiTextures.Length; j++)
			{
				string name2 = _uiTextures[j].mainTexture.name;
				Debug.Log("index is " + j + " texture name " + name2);
				Texture2D mainTexture2 = Resources.Load("HD/MenuPanel_UI/" + name2) as Texture2D;
				_uiTextures[j].mainTexture = mainTexture2;
			}
		}
		else
		{
			Debug.Log("SD texture length " + _uiTextures.Length);
			for (int k = 0; k < _uiTextures.Length; k++)
			{
				string name3 = _uiTextures[k].mainTexture.name;
				Debug.Log("index is " + k + " texture name " + name3);
				Texture2D mainTexture3 = Resources.Load("SD/MenuPanel_UI/" + name3) as Texture2D;
				_uiTextures[k].mainTexture = mainTexture3;
			}
		}
		_isLoaded = true;
	}
}
